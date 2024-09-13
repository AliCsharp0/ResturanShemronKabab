using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Order;
using Restaurant.DomainModel.ApplicationModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ApplicationServiceContract.Services
{
	public interface IUserApplication
	{
		List<UserListItem> GetAllListItem();

		List<UserListItem> Search(UserSearchModel sm, out int RecordCount);

		OperationResult Register(UserAddAndEditModel model);

		OperationResult Delete(int userID);

		OperationResult Update(UserAddAndEditModel model);

		UserAddAndEditModel Get(int UserID);
	}
}
