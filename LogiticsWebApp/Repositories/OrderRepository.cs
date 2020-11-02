using LogiticsWebApp.Areas.Vendor.Models;
using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;
using System;
using System.Linq;

namespace LogiticsWebApp.Repositories
{
    public class OrderRepository : BaseRepository<Orders>, IOrderRepository
    {
        private readonly LogisticDbContext _context;
        public OrderRepository(LogisticDbContext context) : base(context)
        {
            _context = context;
        }

        public GridModel<OrderViewModel> GetOrders(string vendorId,
            int currentPage,
            string search,
            int statusId,
            int paymentDetailId,
            int shippingDetailId,
            string orderNumber,
            string trackingNumber,
            int rows)
        {
            int maxRows = rows;
            var orderGridModel = new GridModel<OrderViewModel>();

            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(a => search.Trim().ToLower().Contains(a.Location));

            if (statusId > 0)
                query = query.Where(a => a.StatusId == statusId);

            if (shippingDetailId > 0)
                query = query.Where(a => a.ShippingDetailId == shippingDetailId);

            if (paymentDetailId > 0)
                query = query.Where(a => a.PaymentDetailId == paymentDetailId);

            if (!string.IsNullOrWhiteSpace(orderNumber))
                query = query.Where(a => a.OrderNumber.ToUpper().Contains(orderNumber));

            if (!string.IsNullOrWhiteSpace(trackingNumber))
                query = query.Where(a => a.TrackingNumber.ToUpper().Contains(trackingNumber));

            var data = query
                        .OrderBy(customer => customer.Email)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).Select(a => new OrderViewModel
                        {
                            OrderNumber = a.OrderNumber,
                            StatusId = a.StatusId,
                            TrackingNumber = a.TrackingNumber,
                            Name = a.Name,
                            Location = a.Location,
                            Email = a.Email,
                            Phone = a.Phone,
                            ShippingDetailId = a.ShippingDetailId,
                            PaymentDetailId = a.PaymentDetailId,
                            Id = a.Id
                        }).ToList();

            orderGridModel.TotalCount = query.Count();
            double pageCount = (double)((decimal)query.Count() / Convert.ToDecimal(maxRows));
            orderGridModel.PageCount = (int)Math.Ceiling(pageCount);
            orderGridModel.CurrentPageIndex = currentPage;
            orderGridModel.SelectedRow = rows.ToString();
            orderGridModel.List = data;
            return orderGridModel;
        }

    }
}
