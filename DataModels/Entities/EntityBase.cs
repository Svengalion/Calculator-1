using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Entities
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            LastTime = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        
        public DateTime LastTime { get; set; }

        public override string ToString()
        {
            return Id.ToString()+ ";"+ LastTime.ToString()+";";
        }
    }


}
