using elcubo9.data.binding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.data
{
    public class Additional:AdditionalBase
    {
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        public virtual List<AdditionalItem> Items { get; set; }
    }
    public class AdditionalItem : AdditionalItemBase 
    {
        
        [ForeignKey("AdditionalID")]
        public virtual Additional Additional { get; set; }
    }
}
