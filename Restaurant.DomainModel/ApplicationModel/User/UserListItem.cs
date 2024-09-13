using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.User
{
	public class UserListItem
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public bool IsEmailActivated { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
