using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Data
{
    public class OperationData
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public DateTime OperationDate { get; set; }
        public int UserId { get; set; }
        public UserData User { get; set; }
        public PetData Pet { get; set; }
    }
}
