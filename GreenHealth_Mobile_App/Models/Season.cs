using System;
using System.Collections.Generic;
using System.Text;

namespace GreenHealth_Mobile_App.Models
{
	public class Season
	{
		public int Id { get; set; }
		public string Name { get; set; }
#nullable enable
		public string? StartDate { get; set; }
		public string? EndDate { get; set; }
#nullable disable
		public ICollection<Plant> Plants { get; set; }
	}
}
