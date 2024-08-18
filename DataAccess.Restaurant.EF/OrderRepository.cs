using FrameWork.BaseRepository;
using FrameWork.DTOS;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Order;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Restaurant.EF
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantShemronKababContext db;

        public OrderRepository(RestaurantShemronKababContext db)
        {
            this.db = db;
        }

        public Order Get(int ID)
        {
            return db.Orders.FirstOrDefault(x => x.OrderID == ID);
        }

        public List<Order> GetAll()
        {
            return db.Orders.ToList();
        }

        public List<OrderListItem> GetAllListItem()
        {
            throw new NotImplementedException();
        }

        public OperationResult Register(Order Current)
        {
            OperationResult op = new OperationResult("Register Order");
            try
            {
                db.Orders.Add(Current);
                db.SaveChanges();
                return op.ToSuccess("Register Order Success Fully");
            }
            catch (Exception ex)
            {
                return op.ToFail("Register Order Failed");
            }
        }

        public OperationResult Remove(int ID)
        {
            OperationResult op = new OperationResult("Remove Order");
            try
            {
                var or = Get(ID);
                db.Orders.Remove(or);
                db.SaveChanges();
                return op.ToSuccess("Remove Order Success Fullly");
            }
            catch (Exception ex)
            {
                return op.ToFail("Remove Order Failed");
            }
        }

        public List<OrderListItem> Search(OrderSearchModel searchModel, out int RecordCount)
        {
            if (searchModel.PageSize == 0)
            {
                searchModel.PageSize = 5;
            }
            var q = from or in db.Orders select or;
            if (searchModel.OrderID != null)
            {
                q = q.Where(x => x.OrderID == searchModel.OrderID);
            }
            if (searchModel.OrderDate != null)
            {
                q = q.Where(x => x.OrderDate == searchModel.OrderDate);
            }
            RecordCount = q.Count();
            q = q.OrderByDescending(x => x.OrderID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);

            var q2 = from or in q
                     select new OrderListItem
                     {
                         EmployeeID = or.EmployeeID,
                         CustomerID = or.CustomerID,
                         AddressID = or.AddressID,
                         OrderDate = or.OrderDate,
                         OrderID = or.OrderID,
                         SmallDescription = or.SmallDescription,
                     };
            return q2.ToList();
        }

        public OperationResult Update(Order Current)
        {
            OperationResult op = new OperationResult("Update Order");
            try
            {
                db.Orders.Attach(Current);
                db.Entry<Order>(Current).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return op.ToSuccess("Update Order Success Fully");
            }
            catch(Exception ex)
            {
                return op.ToFail("Update Order Failed");
            }
        }
    }
}
