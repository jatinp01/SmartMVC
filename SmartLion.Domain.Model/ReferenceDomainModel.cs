using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLion.Domain.Model
{
    public class ReferenceDomainModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
    }

    public class ReferenceTypeDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
