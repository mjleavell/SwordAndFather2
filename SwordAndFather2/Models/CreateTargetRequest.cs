using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather2.Models
{
    public class CreateTargetRequest
    {
        public string Location { get; set; }
        public string Name { get; set; }
        public FitnessLevel FitnessLevel { get; set; }
        public int UserId { get; set; }
    }

    public enum FitnessLevel
    {
        Bad, //0
        Good, //1
        Awesome, //2
        Ovaltine  //3
    }
}
