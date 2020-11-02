using System;
using System.Collections.Generic;

namespace LogiticsWebApp.Entities
{
    public partial class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
        public int NumericIsoCode { get; set; }
        public int DisplayOrder { get; set; }
    }
}
