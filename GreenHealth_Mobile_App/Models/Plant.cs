using System;
using System.Collections.Generic;
using System.Text;

namespace GreenHealth_Mobile_App.Models
{
    public class Plant
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ResultId { get; set; }
		public string Location { get; set; }
		public string Timestamp { get; set; }
		public string ImagePath { get; set; }
	}
}
