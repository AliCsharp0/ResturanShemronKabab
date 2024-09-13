
using FrameWork.DTOS;
using Security.Domain;
using Security.Domain.DTO.User;


namespace Security.ApplicationServiceContract.Services
{
    public interface IAuthHelper
    {
        void Signin(UserInfo account);
        void Signout();
        UserInfo GetCurrentUserInfo();
    }
}