using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ApplicationServiceContract.Services
{
    public interface IOrderApplication
    {
        OrderAddAndEditModel Get(int OrderID);

        OperationResult Register(OrderAddAndEditModel order);

        OperationResult Update(OrderAddAndEditModel order);

        OperationResult Remove(int OrderID);

        List<OrderListItem> Search(OrderSearchModel searchModel, out int RecordCount);
    }
}
