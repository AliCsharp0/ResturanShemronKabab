using FrameWork.DTOS;
using Security.Domain;
using Security.Domain.DTO.User;

namespace Security.DataAccessServiceContract
{
    public interface IAcountRepository
    {
        UserInfo GetUserInf(string UserName);
        User GetFullInfo(string UserName);
        OperationResult RegisterNewUser(User u);
        bool CheckIfUserHasaccess(CheckPermission per);

		bool ExistName(string FirstName, string LastName);

		bool ExistUserName(string userName);

		bool ExistMobile(string mobile);

		bool ExistEmail(string email);
	}
}
