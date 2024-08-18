using FrameWork.BaseRepository;
using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Order;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccessServiceContract.Repositories
{
    public interface IOrderRepository : IBaseRepositorySearchable<Order, int, OrderSearchModel, OrderListItem>
    {
    }
}
