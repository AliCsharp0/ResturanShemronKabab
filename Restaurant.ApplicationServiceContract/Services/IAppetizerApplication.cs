using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ApplicationServiceContract.Services
{
    public interface IAppetizerApplication
    {
        AppetizerAddAndEditModel Get(int AppetizerID);

        OperationResult Update(AppetizerAddAndEditModel appetizer);

        OperationResult Register(AppetizerAddAndEditModel appetizer);

        OperationResult Remove(int AppetizerID);

        List<AppetizerListItem> Search(AppetizerSearchModel searchModel , out int RecordCount);

        List<AppetizerListItem> GetAllListItem();


	}
}
