using System;
using System.Collections.Generic;
using System.Text;

namespace GreenHealth_Mobile_App.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int GrowthStage { get; set; }
        public double Accuracy { get; set; }
        public int CorrectedGrowthStage { get; set; }
        public string Species { get; set; }
        public string CorrectedSpecies { get; set; }
    }
}
