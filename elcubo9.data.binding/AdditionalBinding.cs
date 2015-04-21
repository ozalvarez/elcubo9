using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.data.binding
{
    public class AdditionalBase
    {
        public int AdditionalID { get; set; }
        public int CustomerID { get; set; }
        public string AdditionalName { get; set; }
    }
    public class AdditionalBinding : AdditionalBase
    {
        public IEnumerable<AdditionalItemBinding> Items { get; set; }
    }
    public class AdditionalItemBase
    {
        public int AdditionalItemID { get; set; }
        public int AdditionalID { get; set; }
        public string AdditionalItemName { get; set; }
        public float Price { get; set; }
    }
    public class AdditionalItemBinding : AdditionalItemBase
    {
        public int CustomerID { get; set; }
    }
}
