using FrameWork.BaseRepository;
using Restaurant.DomainModel.ApplicationModel.User;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccessServiceContract.Repositories
{
	public interface IUserRepository:IBaseRepositorySearchable<User , int , UserSearchModel , UserListItem>
	{
		bool ExistName(string FirstName , string LastName);

		bool ExistUserName(string userName);

		bool ExistNameInUpdate( int ID ,string FirstName , string LastName);

		bool ExistUserNameInUpdate(int ID, string userName);

		bool ExistMobile(string mobile);

		bool ExistMobileInUpdate(int ID ,string mobile);

		bool ExistEmail(string email);

		bool ExistEmailInUpdate(int ID , string email);
	}
}
