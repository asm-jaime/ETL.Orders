using ETL.Orders.BLL.DTOs;
using ETL.Orders.DAL;

namespace ETL.Orders.BLL.Services;

public class PurchaseService(IPurchaseRepository purchaseRepository, IPurchaseItemRepository purchaseItemRepository)
{
    IPurchaseRepository _purchaseRepository = purchaseRepository;
    IPurchaseItemRepository _purchaseItemRepository = purchaseItemRepository;

    public async Task AddPurchase(PurchaseDTO purchase)
    {
    }
}
