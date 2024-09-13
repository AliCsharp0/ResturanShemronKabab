using FrameWork.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.User
{
	public class UserSearchModel:PageModel2
	{
		public string? UserName { get; set; }

		public string? Mobile { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }
	}
}
