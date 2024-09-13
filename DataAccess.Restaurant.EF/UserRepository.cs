using FrameWork.DTOS;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.ApplicationModel.User;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Restaurant.EF
{
	public class UserRepository : IUserRepository
	{
		private readonly SecurityContext db;
        public UserRepository(SecurityContext db)
        {
            this.db = db;
        }

		public bool ExistEmail(string email)
		{
			return db.Users.Any(x => x.Email == email);
		}

		public bool ExistEmailInUpdate(int ID, string email)
		{
			return db.Users.Any(x => x.UserID != ID && x.Email == email);
		}

		public bool ExistMobile(string mobile)
		{
			return db.Users.Any(x => x.Mobile == mobile);
		}

		public bool ExistMobileInUpdate(int ID, string mobile)
		{
			return db.Users.Any(x=>x.UserID != ID &&  x.Mobile == mobile);
		}

		public bool ExistName(string FirstName, string LastName)
		{
			return db.Users.Any(x=>x.FirstName == FirstName && x.LastName == LastName);
		}

		public bool ExistNameInUpdate(int ID, string FirstName, string LastName)
		{
			return db.Users.Any(x=>x.UserID != ID && x.FirstName ==FirstName && x.LastName == LastName);	
		}

		public bool ExistUserName(string userName)
		{
			return db.Users.Any(x=> x.UserName == userName);
		}

		public bool ExistUserNameInUpdate(int ID, string userName)
		{
			return db.Users.Any(x=>x.UserID != ID && x.UserName == userName);
		}

		public User Get(int ID)
		{
			return db.Users.FirstOrDefault(x => x.UserID == ID);
		}

		public List<User> GetAll()
		{
			throw new NotImplementedException();
		}

		public List<UserListItem> GetAllListItem()
		{
			var user = db.Users.Select(x => new UserListItem
			{
				UserID = x.UserID,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Email = x.Email,
				IsEmailActivated = x.IsEmailActivated,
				Mobile = x.Mobile,
				UserName = x.UserName,
			}).ToList();
			return user;
		}

		public OperationResult Register(User Current)
		{
			var op = new OperationResult("Register User ");
			try
			{
				if (Current.RoleID == 0)
				{
					Current.RoleID = 2;
				}
				db.Users.Add(Current);
				db.SaveChanges();
				return op.ToSuccess("Register User Success Fully");
			}
			catch (Exception ex)
			{
				return op.ToFail("Register User Failed"+ex.Message);
			}
		}

		public OperationResult Remove(int ID)
		{
			OperationResult op = new OperationResult("Remove User ");
			try
			{
				var user = db.Users.FirstOrDefault(x => x.UserID == ID);
				db.Users.Remove(user);
				db.SaveChanges();
				return op.ToSuccess("Remove User Success Fully");
			}
			catch(Exception ex)
			{
				return op.ToFail("Remove User Failed"+ex.Message);
			}
		}

		public List<UserListItem> Search(UserSearchModel searchModel, out int RecordCount)
		{
			if(searchModel.PageSize == 0)
			{
				searchModel.PageSize = 10;
			}
			var q = from user in db.Users select user;

			if (!string.IsNullOrEmpty(searchModel.FirstName))
			{
				q = q.Where(x => x.FirstName.StartsWith(searchModel.FirstName));
			}
			if (!string.IsNullOrEmpty(searchModel.LastName))
			{
				q = q.Where(x => x.LastName.StartsWith(searchModel.LastName));
			}
			if (!string.IsNullOrEmpty(searchModel.UserName))
			{
				q = q.Where(x => x.UserName.StartsWith(searchModel.UserName));
			}
			if (!string.IsNullOrEmpty(searchModel.Mobile))
			{
				q = q.Where(x => x.Mobile.StartsWith(searchModel.Mobile));
			}
			RecordCount = q.Count();

			q = q.OrderByDescending(x => x.UserID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);

			var q2 = from user in q
					 select new UserListItem
					 {
						FirstName  = user.FirstName,
						LastName = user.LastName,
						UserName = user.UserName,
						Mobile = user.Mobile,
						Email = user.Email,
						IsEmailActivated = user.IsEmailActivated,
						UserID = user.UserID,
					 };
			return q2.ToList();
		}

		public OperationResult Update(User Current)
		{
			OperationResult op = new OperationResult("Update User");
			try
			{
				if (Current.RoleID == 0)
				{
					Current.RoleID = 2;
				}
				db.Users.Attach(Current);
				db.Entry<User>(Current).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				db.SaveChanges();
				return op.ToSuccess("Update User Success Fully");
			}
			catch(Exception ex)
			{
				return op.ToFail("Update User Failed" + ex.Message);
			}
		}
	}
}
