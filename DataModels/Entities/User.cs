using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Entities
{
    public class User : EntityBase
    {
        public string Nick { get; set; } = null!;
        public Guid? AuthorizationId { get; set; }
        public Authorization? Authorization {get;set;}
        public bool RememberMe { get; set; }

        public IList<History> Histories { get; set; } = new List<History>();

        public static readonly Guid GustGuid = new Guid("28A81A21-FABA-4C0F-827E-5B3E567A5523");

        public static readonly User Gust = new User()
        {
            Nick = "Гость",
            Id = GustGuid
        };
    }
}
