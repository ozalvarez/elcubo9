using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.data.binding
{

    public class MenuBase
    {
        public int MenuID { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int MenuTagID { get; set; }
        public float Price { get; set; }
        public bool Disabled { get; set; }
    }
    public class MenuBinding : MenuBase
    {
        public int CustomerID { get; set; }
        public string Tag { get; set; }
        public IEnumerable<MenuAdditionalBinding> Items { get; set; }
    }
    public class MenuTagBinding
    {
        public int CustomerID { get; set; }
        public int MenuTagID { get; set; }
        public string MenuTagName { get; set; }
        public string Symbol { get; set; }
    }
    public class MenuListBinding : MenuTagBinding
    {
        public IEnumerable<MenuBinding> Menus { get; set; }
    }

    public class ItemBinding : MenuBase
    {
        public int CustomerID { get; set; }
        public IEnumerable<MenuAdditionalBinding> MenuAdditionals { get; set; }
    }
    public class MenuAdditionalBase
    {
        public int MenuAdditionalID { get; set; }
        public int MenuID { get; set; }
        public int AdditionalID { get; set; }
        public string MenuAdditionalName { get; set; }
        public bool Required { get; set; }
    }
    public class MenuAdditionalBinding : MenuAdditionalBase
    {
        public IEnumerable<AdditionalItemBinding> Items { get; set; }
    }
    public class MenuDisabledBinding
    {
        public int MenuID { get; set; }
        public bool Disabled { get; set; }
    }
    
}
