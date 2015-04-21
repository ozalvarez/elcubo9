using elcubo9.data.binding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.data
{
    public class Menu : MenuBase
    {
        [ForeignKey("MenuTagID")]
        public virtual MenuTag MenuTag { get; set; }
        public virtual List<MenuAdditional> MenuAdditionals { get; set; }

    }
    public class MenuTag
    {
        public int MenuTagID { get; set; }
        public string MenuTagName { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        public virtual List<Menu> Menus { get; set; }
    }
    public class MenuAdditional : MenuAdditionalBase
    {
        

        [ForeignKey("MenuID")]
        public virtual Menu Menu { get; set; }
        [ForeignKey("AdditionalID")]
        public virtual Additional Additional { get; set; }
    }
}
