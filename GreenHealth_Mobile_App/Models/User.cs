using System;
using System.Collections.Generic;
using System.Text;

namespace GreenHealth_Mobile_App.Models
{
    public class User
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Password { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsOwner { get; set; }
#nullable enable
		public int? OrganisationId { get; set; }
		public Organisation? Organisation { get; set; }
#nullable disable
		public User()
		{
			Id = 0;
			FirstName = "";
			LastName = "";
			Email = "";
			Address = "";
			Password = "";
			IsAdmin = false;
			IsOwner = false;
		}
	}
}
