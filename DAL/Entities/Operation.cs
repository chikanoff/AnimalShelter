using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public DateTime OperationDate { get; set; }
        public int UserId { get; set; }

    }
}
