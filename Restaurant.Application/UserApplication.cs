using FrameWork.DTOS;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.ApplicationModel.Order;
using Restaurant.DomainModel.ApplicationModel.User;
using Restaurant.DomainModel.Models;
using Security.DataAccessServiceContract;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
	public class UserApplication : IUserApplication
	{
		private readonly IUserRepository UserRepo;
		private readonly IAcountRepository _accountRepository;
		public UserApplication(IUserRepository UserRepo , IAcountRepository _accountRepository)
        {
            this.UserRepo = UserRepo;
			this._accountRepository = _accountRepository;
        }

		public User  ToModel(UserAddAndEditModel model)
		{
			User user = new User
			{
				UserID = model.UserID,
				UserName = model.UserName,
				Password = model.Password,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				IsEmailActivated = model.IsEmailActivated,
				Mobile = model.Mobile,
			};
			return user;
		}

		public UserAddAndEditModel ToAddEditModel(User user)
		{
			UserAddAndEditModel userAddAndEditModel = new UserAddAndEditModel
			{
				UserID=user.UserID,
				UserName=user.UserName,
				Password=user.Password,
				FirstName=user.FirstName,
				LastName=user.LastName,
				Email=user.Email,
				IsEmailActivated=user.IsEmailActivated,
				Mobile=user.Mobile,
			};
			return userAddAndEditModel;
		}

        public List<UserListItem> GetAllListItem()
		{
			return UserRepo.GetAllListItem();
		}

		public OperationResult Register(UserAddAndEditModel model)
		{
			if(UserRepo.ExistName(model.FirstName , model.LastName))
			{
				return new OperationResult("Register User").ToFail("Duplicate  First Name And Last Name");
			}
			if (UserRepo.ExistUserName(model.UserName))
			{
				return new OperationResult("Register User").ToFail("Duplicate User Name");
			}
			if (UserRepo.ExistMobile(model.Mobile))
			{
				return new OperationResult("Register User").ToFail("Duplicate Mobile");
			}
			if(UserRepo.ExistEmail(model.Email))
			{
				return new OperationResult("Register User").ToFail("Duplicate Email");
			}
			User user = ToModel(model);
			var OperationUserAdd = UserRepo.Register(user);
			return OperationUserAdd;
		}

		public List<UserListItem> Search(UserSearchModel sm, out int RecordCount)
		{
			return UserRepo.Search(sm, out RecordCount);
		}

		public OperationResult Delete(int userID)
		{
			return UserRepo.Remove(userID);
		}

		public OperationResult Update(UserAddAndEditModel model)
		{
			if (UserRepo.ExistNameInUpdate(model.UserID, model.FirstName, model.LastName))
			{
				return new OperationResult("Update User").ToFail("Duplicate  First Name And Last Name");
			}
			if (UserRepo.ExistUserNameInUpdate(model.UserID, model.UserName))
			{
				return new OperationResult("Update User").ToFail("Duplicate User Name");
			}
			if (UserRepo.ExistMobileInUpdate(model.UserID, model.Mobile))
			{
				return new OperationResult("Update User").ToFail("Duplicate Mobile");
			}
			if (UserRepo.ExistEmailInUpdate(model.UserID, model.Email))
			{
				return new OperationResult("Update User").ToFail("Duplicate Email");
			}
			User user = ToModel(model);
			return UserRepo.Update(user);
		}

		public UserAddAndEditModel Get(int UserID)
		{
			return ToAddEditModel(UserRepo.Get(UserID));
		}
	}
}
