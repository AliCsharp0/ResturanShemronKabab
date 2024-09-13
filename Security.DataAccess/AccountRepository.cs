using System;
using System.Linq;


using FrameWork.DTOS;
using Security.DataAccessServiceContract;
using Security.Domain;
using Security.Domain.DTO.User;

namespace Security.DataAccess
{
    public class AccountRepository : IAcountRepository
    {
        private readonly Security.Domain.SecurityContext db;
        public AccountRepository(SecurityContext db)
        {
            this.db = db;
        }

        public User GetFullInfo(string UserName)
        {
            return db.Users.FirstOrDefault(x => x.UserName == UserName || x.Email == UserName);
        }

        public UserInfo GetUserInf(string UserName)
        {


            var q = from r in db.Roles
                    join u in db.Users
 on r.RoleID equals u.RoleID
                    where u.UserName == UserName || u.Email == UserName
                    select new UserInfo
                    {
                        FullName = u.LastName + u.FirstName
                ,
                        RoleID = u.RoleID
                ,
                        RoleName = r.RoleName
                ,
                        UserName = u.UserName
                ,
                        UserID = u.UserID,
                        Mobile = u.Mobile,
                        Email = u.Email,
                    };
            return q.FirstOrDefault();
        }

        public OperationResult RegisterNewUser(User u)
        {
            OperationResult op = new OperationResult("Register New User");
            try
            {
                if (u.RoleID == 0)
                {
                    u.RoleID = 2;
                }
                db.Users.Add(u);
                db.SaveChanges();
                //op.ToSuccess( u.UserID, "User Registered Successfully");
                op.ToSuccess("User Registered Successfully");
            }
            catch (Exception ex)
            {
                op.ToFail("Registeration Failed   " + ex.Message);
            }
            return op;
        }

        public bool CheckIfUserHasaccess(CheckPermission per)
        {
            var q = from u in db.Users
                    join r in db.Roles on u.RoleID equals r.RoleID
                    join ra in db.RoleActions on r.RoleID equals ra.RoleID
                    join ac in db.projectActions on ra.ProjectActionID equals ac.ProjectActionID
                    join co in db.projectControllers on ac.ProjectController equals co

                    select new
                    {
                        co.ProjectControllerName,
                        ac.ProjectActionName
                        ,
                        u.UserName
                        ,
                        ra.HasPermission
                    };
            var result = q.First(x =>
                x.UserName == per.UserName && x.ProjectActionName == per.ActionName &&
                x.ProjectControllerName == per.Controller);
            if (result == null)
            {
                return false;
            }

            return result.HasPermission;
        }

		public bool ExistEmail(string email)
		{
			return db.Users.Any(x => x.Email == email);
		}

		public bool ExistMobile(string mobile)
		{
			return db.Users.Any(x => x.Mobile == mobile);
		}

		public bool ExistName(string FirstName, string LastName)
		{
			return db.Users.Any(x => x.FirstName == FirstName && x.LastName == LastName);
		}

		public bool ExistUserName(string userName)
		{
			return db.Users.Any(x => x.UserName == userName);
		}

	}
}
