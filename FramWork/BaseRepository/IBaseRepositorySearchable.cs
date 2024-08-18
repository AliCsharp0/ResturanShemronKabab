using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.BaseRepository
{
    public interface IBaseRepositorySearchable<TModel , TKey , TSearchModel , TSearchResult>:IBaseRepository<TModel , TKey>
    {
        List<TSearchResult> Search(TSearchModel searchModel , out int RecordCount);

        List<TSearchResult> GetAllListItem();
    }
}
