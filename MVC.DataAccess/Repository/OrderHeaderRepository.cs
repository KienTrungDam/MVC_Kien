using MVC.DataAccess.Data;
using MVC.DataAccess.Repository.IRepository;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db) // lay tu ham tao trong repository
        {
            _db = db;
        }
        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }
        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id); // lay orderheader
            if (orderFromDb != null) 
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus)) // neu paymentStatus khac null or 0
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId)) // paymentIntentId la it xuat hien khi thanh toan thanh cong
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
        }
    }
}
