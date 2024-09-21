using Azure.Core;
using FrameWork.DTOS;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Beverages;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class BeveragesApplication : IBeveragesApplication
    {
        private readonly IBeveragesRepository BeveragesRepo;

        public BeveragesApplication(IBeveragesRepository BeveragesRepo)
        {
            this.BeveragesRepo = BeveragesRepo;
        }

        private Beverages ToModel(BeveragesAddAndEditModel model)
        {
            Beverages beverages = new Beverages
            {
                BeveragesID = model.BeveragesID,
                BeveragesName = model.BeveragesName,
                CategoryID = model.CategoryID,
                ImageURL = model.ImageURL,
                UnitPrice = model.UnitPrice,
            };
            return beverages;
        }
        private BeveragesAddAndEditModel ToAddEditModel(Beverages model)
        {
            BeveragesAddAndEditModel addAndEditModel = new BeveragesAddAndEditModel
            {
                CategoryID = model.CategoryID,
                ImageURL = model.ImageURL,
                UnitPrice = model.UnitPrice,
                BeveragesID = model.BeveragesID,
                BeveragesName = model.BeveragesName,
            };
            return addAndEditModel;
        }

        public BeveragesAddAndEditModel Get(int BeveragesID)
        {
            return ToAddEditModel(BeveragesRepo.Get(BeveragesID)); 
        }

        public OperationResult Register(BeveragesAddAndEditModel beverages)
        {
            if (BeveragesRepo.ExistBeveragesName(beverages.BeveragesName))
            {
                return new OperationResult("Register Beverages").ToFail("Duplicate Beverages Name");
            }
            if (BeveragesRepo.ExistImage(beverages.ImageURL))
            {
				return new OperationResult("Register Beverages").ToFail("Duplicate Beverages Image");
			}
			if (beverages.CategoryID < 0)
			{
				return new OperationResult("Register Appetizer").ToFail("Duplicate Category Name");
			}
			var beve = ToModel(beverages);
            var OperationBeverages = BeveragesRepo.Register(beve); 
            return OperationBeverages;
        }

        public OperationResult Remove(int BeveragesID)
        {
            if (BeveragesRepo.HasRelatedOrders(BeveragesID))
            {
                return new OperationResult("Remove Beverages ").ToFail("Beverages Has Related Order");
            }
            return BeveragesRepo.Remove(BeveragesID);
        }

        public List<BeveragesListItem> Search(BeveragesSearchModel searchModel, out int RecordCount)
        {
            return BeveragesRepo.Search(searchModel, out RecordCount);
        }

        public OperationResult Update(BeveragesAddAndEditModel beverages)
        {
            if (BeveragesRepo.ExistNameInUpdate(beverages.BeveragesID, beverages.BeveragesName))
            {
                return new OperationResult("Update beverages").ToFail("Duplicate beverages Name");
            }
            if (BeveragesRepo.ExistImageInUpdate(beverages.BeveragesID , beverages.BeveragesName))
            {
                return new OperationResult("Update beverages").ToFail("Duplicate Image");
            }
			if (beverages.CategoryID < 0)
			{
				return new OperationResult("Register Appetizer").ToFail("Duplicate Category Name");
			}
			var bever = ToModel(beverages);
            return BeveragesRepo.Update(bever);
        }

        public List<BeveragesListItem> GetAllListItem()
        {
            return BeveragesRepo.GetAllListItem();
        }

		public List<BeveragesListItemUI> GetAllListItemInUI()
		{
            return BeveragesRepo.GetAllListItemInUI();
		}

		public void RemoveImage(int ID)
		{
            BeveragesRepo.RemoveImage(ID);
		}
	}
}
