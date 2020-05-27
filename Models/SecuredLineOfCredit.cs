using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class SecuredLineOfCredit:LineOfCredit
    {
        public int Id { get; set; }
        public double? CollateralToLocRatio { get; set; }


    }
}
