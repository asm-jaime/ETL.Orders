using ETL.Orders.BLL.DTOs;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace ETL.Orders.BLL;

public class XmlFileProcessingService(ILogger<XmlFileProcessingService> logger, PurchaseService purchaseService) : IFileProcessingService
{
    private readonly ILogger<XmlFileProcessingService> _logger = logger;
    private readonly PurchaseService _purchaseService = purchaseService;

    public async Task ProcessFile(string filePath)
    {
        try
        {
            using var reader = XmlReader.Create(filePath, new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit });
            reader.MoveToContent();

            while(reader.Read())
            {
                if(reader.NodeType != XmlNodeType.Element || reader.Name != XmlTags.Order)
                {
                    continue;
                }
                if(XElement.ReadFrom(reader) is not XElement orderElement)
                {
                    continue;
                }
                var purchase = GetPurchase(orderElement);
                await _purchaseService.AddPurchase(purchase);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error processing the XML file.");
            throw;
        }
    }

    private PurchaseDTO GetPurchase(XElement orderElement)
    {
        var result = new PurchaseDTO()
        {
            NumberOrder = default,
            PurchaseDate = default,
            TotalAmount = default,
            PurchaseItems = []
        };

        try
        {
            var orderNo = orderElement.Element(XmlTags.No)?.Value ?? throw new FormatException("The order number is in an invalid format.");
            var regDate = orderElement.Element(XmlTags.RegDate)?.Value;
            var sum = orderElement.Element(XmlTags.Sum)?.Value;


            if(!DateTime.TryParse(regDate, out var registrationDate))
            {
                throw new FormatException("The registration date is in an invalid format.");
            }
            if(!decimal.TryParse(sum, NumberStyles.Number, CultureInfo.InvariantCulture, out var orderSum))
            {
                throw new FormatException("The order sum is in an invalid format.");
            }

            result.NumberOrder = int.Parse(orderNo);
            result.PurchaseDate = registrationDate;
            result.TotalAmount = orderSum;

            result.PurchaseItems = GetPurchaseItemsFromOrderElementsProduct(orderElement.Elements(XmlTags.Product));

            var userElement = orderElement.Element(XmlTags.User) ?? throw new FormatException("The user is in an invalid format.");
            var fioElement = userElement.Element(XmlTags.Fio)?.Value ?? throw new FormatException("The FIO is in an invalid format.");
            var emailElement = userElement.Element(XmlTags.Email)?.Value ?? throw new FormatException("The email is in an invalid format.");
            var names = fioElement.Split(" ");
            if(names.Length != 3)
            {
                throw new FormatException("The FIO quantity is in an invalid format.");
            }

            var user = new UserDTO
            {
                FirstName = names.First(),
                LastName = names.Last(),
                Email = emailElement
            };

            result.User = user;

            _logger.LogInformation($"Order {orderNo} has been processed and saved.");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"Error processing order: {orderElement.Element(XmlTags.No)?.Value}");
            throw;
        }

        return result;
    }

    private ICollection<PurchaseItemDTO> GetPurchaseItemsFromOrderElementsProduct(IEnumerable<XElement> orderElements)
    {
        var result = new List<PurchaseItemDTO>();

        foreach(var productElement in orderElements)
        {
            var productName = productElement.Element(XmlTags.Name)?.Value ?? throw new FormatException("The product name is in an invalid format.");
            var productPrice = productElement.Element(XmlTags.Price)?.Value;
            if(!decimal.TryParse(productPrice, NumberStyles.Number, CultureInfo.InvariantCulture, out var productPriceDecimal))
            {
                throw new FormatException("The product price is in an invalid format.");
            }
            var quantity = productElement.Element(XmlTags.Quantity)?.Value;
            if(!int.TryParse(quantity, out var quantityInt))
            {
                throw new FormatException("The product quantity is in an invalid format.");
            }

            var purchaseItem = new PurchaseItemDTO
            {
                ProductName = productName,
                Price = productPriceDecimal,
                StockQuantity = quantityInt,
            };

            result.Add(purchaseItem);
        }

        return result;
    }
}