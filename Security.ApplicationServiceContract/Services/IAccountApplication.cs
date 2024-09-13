
using FrameWork.DTOS;
using Security.Domain;
using Security.Domain.ApplicationModel;
using Security.Domain.DTO.User;

namespace Security.ApplicationServiceContract.Services
{
    public interface IAccountApplication
    {
        OperationResult Login(Login login);
        OperationResult Register(UserAddModelCustomer command);
        void LogoutUser();
        UserInfo GetAccountInfo();
        bool CheckIfUserHasaccess(CheckPermission per);
    }
}
