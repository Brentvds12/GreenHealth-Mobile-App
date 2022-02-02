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
		public ICollection<Plant> Plants { get; set; }
	}
}
