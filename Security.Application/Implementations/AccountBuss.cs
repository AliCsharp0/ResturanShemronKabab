using Framework.Services;
using FrameWork.DTOS;
using Security.ApplicationServiceContract.Services;
using Security.DataAccessServiceContract;
using Security.Domain;
using Security.Domain.ApplicationModel;
using Security.Domain.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.Implementations
{
	public class AccountBuss : IAccountApplication
	{
		private readonly IAuthHelper _authHelper;
		private readonly IAcountRepository _accountRepository;
		private readonly IPasswordHasher _passwordHasher;

		public AccountBuss(IAcountRepository accountRepository, IAuthHelper authHelper, IPasswordHasher passwordHasher)
		{
			_accountRepository = accountRepository;
			_authHelper = authHelper;
			_passwordHasher = passwordHasher;
		}

		public User ToModel(UserAddModelCustomer model)
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

		public UserAddModelCustomer ToAddEditModel(User user)
		{
			UserAddModelCustomer userAddAndEditModel = new UserAddModelCustomer
			{
				UserID = user.UserID,
				UserName = user.UserName,
				Password = user.Password,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				IsEmailActivated = user.IsEmailActivated,
				Mobile = user.Mobile,
			};
			return userAddAndEditModel;
		}

		public OperationResult Login(Login login)
		{
			var result = new OperationResult("Login");
			var u = _accountRepository.GetFullInfo(login.Username);
			if (u == null)
			{
				return result.ToFail("شما هنوز ثبت نام نکرده اید");
			}

			var (verified, needsUpgrade) = _passwordHasher.Check(u.Password, login.Password);
			if (!verified)
			{
				return result.ToFail("Invalid Credential");
			}
			var userInfo = _accountRepository.GetUserInf(login.Username);


			_authHelper.Signin(userInfo);


			return result.ToSuccess("Login Successfully");
		}

		public OperationResult Register(UserAddModelCustomer command)
		{
			if (_accountRepository.ExistName(command.FirstName, command.LastName))
			{
				return new OperationResult("Register User").ToFail("Duplicate  First Name And Last Name");
			}
			if (_accountRepository.ExistUserName(command.UserName))
			{
				return new OperationResult("Register User").ToFail("Duplicate User Name");
			}
			if (_accountRepository.ExistMobile(command.Mobile))
			{
				return new OperationResult("Register User").ToFail("Duplicate Mobile");
			}
			if (_accountRepository.ExistEmail(command.Email))
			{
				return new OperationResult("Register User").ToFail("Duplicate Email");
			}
			User user = ToModel(command);

			var OperationRegister = _accountRepository.RegisterNewUser(user);
			return OperationRegister;
		}

		public void LogoutUser()
		{
			_authHelper.Signout();
		}

		public UserInfo GetAccountInfo()
		{
			return _authHelper.GetCurrentUserInfo();
		}

		public bool CheckIfUserHasaccess(CheckPermission per)
		{
			return _accountRepository.CheckIfUserHasaccess(per);
		}
	}
}
