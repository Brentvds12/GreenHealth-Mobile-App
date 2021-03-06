using System;
using System.Collections.Generic;
using System.Text;

namespace GreenHealth_Mobile_App.Models
{
    public class Plot
    {
		public int Id { get; set; }
		public int OrganisationId { get; set; }
		public string Location { get; set; }
		public Organisation Organisation { get; set; }
		public ICollection<Plant> Plants { get; set; }
    }
}
