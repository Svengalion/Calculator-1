using CalculationsModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Entities
{
    public class History : EntityBase
    {
        public Calculations Calculations { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
