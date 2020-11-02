using LogiticsWebApp.Areas.Vendor.Models;

namespace LogiticsWebApp.Repositories
{
    public interface IOrderRepository
    {
        GridModel<OrderViewModel> GetOrders(string vendorId,
            int currentPage,
            string search,
            int statusId,
            int shippingDetailId,
            int paymentDetailId,
            string orderNumber,
            string trackingNumber,
            int rows);
    }
}
