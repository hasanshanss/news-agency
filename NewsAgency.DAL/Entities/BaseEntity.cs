using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgency.DAL.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public byte[]? Timestamp { get; set; }

        //public bool IsDeleted { get; set; }

    }
}
