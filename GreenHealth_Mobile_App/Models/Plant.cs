using System;
using System.Collections.Generic;
using System.Text;

namespace GreenHealth_Mobile_App.Models
{
    public class Plant
    {
        public Plant(int userId)
        {
            UserId = userId;
        }

        public int Id { get; set; } = 0;
		public int UserId { get; set; }
		public int? ResultId { get; set; } = null;
		public string Location { get; set; } = null;
		public string Timestamp { get; set; } = null;
		public string ImagePath { get; set; } = null;
	}

	public class PlantResponse
    {
		public IList<Plant> plants { get; set; }
    }
}
