using ETL.Orders.BLL.DTOs;
using ETL.Orders.DAL;
using ETL.Orders.DAL.Models;

namespace ETL.Orders.BLL;

public class PurchaseService(
    IPurchaseRepository purchaseRepository,
    IPurchaseItemRepository purchaseItemRepository,
    IUserRepository userRepository,
    IProductRepository productRepository)
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    private readonly IPurchaseItemRepository _purchaseItemRepository = purchaseItemRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task AddPurchase(PurchaseDTO purchaseDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(purchaseDto.User.Email) ?? throw new KeyNotFoundException("User not found.");

        var purchase = new Purchase
        {
            NumberOrder = purchaseDto.NumberOrder,
            PurchaseDate = purchaseDto.PurchaseDate,
            TotalAmount = purchaseDto.TotalAmount,
            UserId = user.UserId,
            PaymentMethod = string.Empty,
            ShippingAddress = string.Empty,
            BillingAddress = string.Empty,
            PurchaseItems = []
        };

        foreach(var itemDto in purchaseDto.PurchaseItems)
        {
            var product = await _productRepository.GetProductByNameAsync(itemDto.ProductName) ?? throw new KeyNotFoundException($"Product '{itemDto.ProductName}' not found.");
            var purchaseItem = new PurchaseItem
            {
                NumberOrder = purchaseDto.NumberOrder,
                ProductId = product.ProductId,
                Quantity = itemDto.StockQuantity,
                UnitPrice = itemDto.Price,
            };

            purchase.PurchaseItems.Add(purchaseItem);
        }

        await _purchaseRepository.AddAsync(purchase);
    }
}
