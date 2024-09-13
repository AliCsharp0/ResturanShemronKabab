using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Beverages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ApplicationServiceContract.Services
{
    public interface IBeveragesApplication
    {
        BeveragesAddAndEditModel Get(int BeveragesID);

        OperationResult Update(BeveragesAddAndEditModel beverages);

        OperationResult Register(BeveragesAddAndEditModel beverages);

        OperationResult Remove(int BeveragesID);

        List<BeveragesListItem> Search(BeveragesSearchModel searchModel, out int RecordCount);

        List<BeveragesListItem> GetAllListItem();

		List<BeveragesListItemUI> GetAllListItemInUI();


	}
}
