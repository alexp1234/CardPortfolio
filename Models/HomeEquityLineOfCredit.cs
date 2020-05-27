using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class HomeEquityLineOfCredit:LineOfCredit
    {
        public int Id { get; set; }
        public double? LTV { get; set; }



    }
}
