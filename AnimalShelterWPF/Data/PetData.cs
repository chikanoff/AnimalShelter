using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Data
{
    public class PetData
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int KindId { get; set; }
        public int BreedId { get; set; }
        public string Kind { get; set; }
        public string Breed { get; set; }
        public string Conditions { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Color { get; set; }
        public string HealthStatus { get; set; }
        public List<PhotoData> Photos { get; set; }
        public UserData User { get; set; }
    }
}
