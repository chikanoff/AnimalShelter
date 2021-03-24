using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int KindId { get; set; }
        public int BreedId { get; set; }
        public string Conditions { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Color { get; set; }
        public string HealthStatus { get; set; }

    }
}
