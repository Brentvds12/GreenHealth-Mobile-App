using System;
using System.Collections.Generic;
using System.Text;

namespace GreenHealth_Mobile_App.Models
{
    public class Organisation
    {
		public int Id { get; set; }
		public string Name { get; set; }		
		public ICollection<User> Users { get; set; }
		public ICollection<Plot> Plots { get; set; }
	}
}
