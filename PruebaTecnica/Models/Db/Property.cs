using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models.Db
{
    public partial class Property
    {
        public int Id { get; set; }
        public int? PropertyTypeId { get; set; }
        public int? OwnerId { get; set; }
        public string Number { get; set; } = null!;
        public string Address { get; set; } = null!;
        public double Area { get; set; }
        public double? ConstructionArea { get; set; }

        public virtual Owner? Owner { get; set; }
        public virtual PropertyType? PropertyType { get; set; }
    }
}
