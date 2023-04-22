using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    public class SortAdsRequest
    {
        public bool ByCreatedDate { get; set; }

        public bool ByAscPrice { get; set; }

        public bool ByDesPrice { get; set; }

        public bool ByPopular { get;}
    }
}
