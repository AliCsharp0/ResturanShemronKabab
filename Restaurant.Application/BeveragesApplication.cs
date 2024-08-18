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
                Image=model.Image,
                UnitPrice = model.UnitPrice,
            };
            return beverages;
        }
        private BeveragesAddAndEditModel ToAddEditModel(Beverages model)
        {
            BeveragesAddAndEditModel addAndEditModel = new BeveragesAddAndEditModel
            {
                CategoryID = model.CategoryID,
                Image = model.Image,
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
                return new OperationResult("Register Beverages").ToFail("Duplicate Beverages");
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
            if (BeveragesRepo.ExistBeveragesName(beverages.BeveragesName))
            {
                return new OperationResult("Update Beverages").ToFail("Duplicate Beverages");
            }
            var bever = ToModel(beverages);
            return BeveragesRepo.Update(bever);
        }

        public List<BeveragesListItem> GetAllListItem()
        {
            return BeveragesRepo.GetAllListItem();
        }
    }
}
