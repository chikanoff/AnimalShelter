using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public string Path { get; set; }
        public Pet Pet { get; set; }
    }
}
