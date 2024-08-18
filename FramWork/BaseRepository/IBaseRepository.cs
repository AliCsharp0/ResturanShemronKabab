using FrameWork.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.BaseRepository
{
    public interface IBaseRepository<TModel , TKey>
    {
        TModel Get(TKey ID);

        OperationResult Remove(TKey ID);

        OperationResult Register(TModel Current);

        OperationResult Update(TModel Current);

        List<TModel> GetAll();
    }
}
