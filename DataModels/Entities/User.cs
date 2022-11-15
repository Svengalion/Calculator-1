using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Entities
{
    public class User : EntityBase
    {
        public string Nick { get; set; } = null!;
        public Guid AuthorizationId { get; set; }
        public Authorization Authorization {get;set;}

        public IList<History> Histories { get; set; } = new List<History>();

        public static readonly Guid GuidOne = new Guid("1-1-1-1-1");

        public static readonly User Gust = new User()
        {
            Nick = "Гость",
            Id = GuidOne
        };
    }
}
