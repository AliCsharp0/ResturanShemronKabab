using FrameWork.DTOS;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class AppetizerApplication : IAppetizerApplication
    {
        private readonly IAppetizerRepository AppetizerRepo;

        public AppetizerApplication(IAppetizerRepository AppetizerRepo)
        {
            this.AppetizerRepo = AppetizerRepo;
        }

        private Appetizer ToModel(AppetizerAddAndEditModel appetizerAddAndEditModel)
        {
            Appetizer appetizer = new Appetizer
            {
                AppetizerID = appetizerAddAndEditModel.AppetizerID,
                CategoryID=appetizerAddAndEditModel.CategoryID,
                ImageURL = appetizerAddAndEditModel.ImageURL,
                SmallDescription=appetizerAddAndEditModel.SmallDescription,
                AppetizerName = appetizerAddAndEditModel.AppetizerName,
                UnitPrice = appetizerAddAndEditModel.UnitPrice,
            };
            return appetizer;
        }

        private AppetizerAddAndEditModel ToAddEditModel(Appetizer appetizer)
        {
            AppetizerAddAndEditModel appetizerAddAndEdit = new AppetizerAddAndEditModel
            {
                AppetizerID = appetizer.AppetizerID,
                AppetizerName=appetizer.AppetizerName,
                CategoryID = appetizer.CategoryID,
                ImageURL = appetizer.ImageURL,
                SmallDescription = appetizer.SmallDescription,
                UnitPrice=appetizer.UnitPrice,
            };
            return appetizerAddAndEdit;
        }

        public AppetizerAddAndEditModel Get(int AppetizerID)
        {
            return ToAddEditModel(AppetizerRepo.Get(AppetizerID));
        }

        public OperationResult Register(AppetizerAddAndEditModel appetizer)
        {
            if (AppetizerRepo.ExistAppetizerName(appetizer.AppetizerName))
            {
                return new OperationResult("Register Appetizer ").ToFail("Duplicate Appetizer Name");
            }
            if(AppetizerRepo.ExistImage(appetizer.ImageURL))
            {
				return new OperationResult("Register Appetizer ").ToFail("Duplicate Appetizer Image");
			}
            if(appetizer.CategoryID < 0)
            {
                return new OperationResult("Register Appetizer").ToFail("Duplicate Category Name");
            }
            var appe = ToModel(appetizer);
            var OperationAppetizer = AppetizerRepo.Register(appe);
            return OperationAppetizer;
        }

        public OperationResult Remove(int AppetizerID)
        {
            if (AppetizerRepo.HasRelatedOrders(AppetizerID))
            {
                return new OperationResult("Remove Appetizer ").ToFail("Appetizer Has Relate Order");
            }
            return AppetizerRepo.Remove(AppetizerID);
        }

        public List<AppetizerListItem> Search(AppetizerSearchModel searchModel, out int RecordCount)
        {
            return AppetizerRepo.Search(searchModel, out RecordCount);
        }

        public OperationResult Update(AppetizerAddAndEditModel appetizer)
        {
            if(AppetizerRepo.ExistNameInUpdate(appetizer.AppetizerID , appetizer.AppetizerName))
            {
                return new OperationResult("Register Appetizer").ToFail("Duplicate Appetizer Name");
            }
            if(AppetizerRepo.ExistImageInUpdate(appetizer.AppetizerID , appetizer.ImageURL))
            {
                return new OperationResult("Register Appetizer").ToFail("Duplicate Image");
            }
			if (appetizer.CategoryID < 0)
			{
				return new OperationResult("Register Appetizer").ToFail("Duplicate Category Name");
			}
			var appe = ToModel(appetizer);
            var OperationAppetizer = AppetizerRepo.Update(appe);
            return OperationAppetizer;
        }

		public List<AppetizerListItem> GetAllListItem()
        {
            return AppetizerRepo.GetAllListItem();
        }

		public List<AppetizerListItemUI> GetAllListItemInUI()
		{
			return AppetizerRepo.GetAllListItemInUI();
		}

		public void RemoveImage(int appetizerID)
		{
			AppetizerRepo.RemoveImage(appetizerID); 
		}
	}
}
