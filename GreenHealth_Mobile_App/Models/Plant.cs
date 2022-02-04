using System;
using System.Collections.Generic;
using System.Text;

namespace GreenHealth_Mobile_App.Models
{
    public class Plant
    {
        public Plant() {}
        public Plant(int plotId)
        {
            PlotId = plotId;
        }

        public int Id { get; set; }
		public int PlotId{ get; set; }
#nullable enable
        public int? ResultId { get; set; } = null;
        public Result? Result { get; set; } = null;
        public int? SeasonId { get; set; } = null;
        public Season? Season { get; set; } = null;
        public string? Location { get; set; } = null;
		public string? Timestamp { get; set; } = null;
#nullable disable
        public string ImagePath { get; set; } = null;
        public Plot Plot { get; set; } = null;
	}

    /* public class PlantResponse
    {
        public List<Plant> Plants { get; set; }
    }*/
}
