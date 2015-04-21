using elcubo9.bll.Exceptions;
using elcubo9.data;
using elcubo9.data.binding;
using elcubo9.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.bll
{
    public class MenuBLL : BaseBLL
    {
        public IEnumerable<MenuListBinding> Get(int CustomerID, bool ShowDisabled, string UserID)
        {
            using (var _c = db)
            {
                if (ShowDisabled)
                    Can(CustomerID, UserID, _c);
                return _c.MenuTags.Where(m => m.CustomerID == CustomerID).Select(m => new MenuListBinding
                {
                    MenuTagID = m.MenuTagID,
                    MenuTagName = m.MenuTagName,
                    CustomerID = CustomerID,
                    Symbol=m.Customer.Symbol,
                    Menus = m.Menus.Where(m2 => ShowDisabled ? true : !m2.Disabled).OrderBy(m2 => m2.Title).Select(m2 => new MenuBinding
                    {
                        MenuID = m2.MenuID,
                        MenuTagID = m2.MenuTagID,
                        Subtitle = m2.Subtitle,
                        Title = m2.Title,
                        Price = m2.Price,
                        Tag = m.MenuTagName,
                        CustomerID = CustomerID,
                        Disabled = m2.Disabled,
                        Items = m2.MenuAdditionals.Select(m3 => new MenuAdditionalBinding
                        {
                            AdditionalID = m3.AdditionalID,
                            MenuAdditionalID = m3.MenuAdditionalID,
                            MenuAdditionalName = m3.MenuAdditionalName,
                            MenuID = m3.MenuID,
                            Required = m3.Required,
                            Items = m3.Additional.Items.Select(m4 => new AdditionalItemBinding
                            {
                                AdditionalItemID = m4.AdditionalItemID,
                                AdditionalItemName = m4.AdditionalItemName,
                                AdditionalID = m4.AdditionalID,
                                Price = m4.Price
                            })
                        })
                    })
                }).ToList();
            }
        }
        public IEnumerable<MenuListBinding> Get(int CustomerID)
        {
            return Get(CustomerID, false, null);
        }
        public void Create(MenuBinding Model, string UserID)
        {
            using (var _c = db)
            {
                if (Can(Model.CustomerID, UserID, _c))
                {
                    var _MT = _c.MenuTags.Where(m => m.CustomerID == Model.CustomerID && m.MenuTagName.ToLower().Equals(Model.Tag.ToLower())).SingleOrDefault();
                    if (_MT == null)
                    {
                        _MT = new MenuTag
                        {
                            CustomerID = Model.CustomerID,
                            MenuTagName = Model.Tag,
                            Menus = new List<Menu>()
                        };
                        _c.MenuTags.Add(_MT);
                    }
                    _MT.Menus.Add(new Menu
                    {
                        Title = Model.Title,
                        Subtitle = Model.Subtitle,
                        Price = Model.Price
                    });
                    _c.SaveChanges();
                }
            }
        }
        public void Update(MenuBinding Model, string UserID)
        {
            using (var _c = db)
            {
                if (Can(Model.CustomerID, UserID, _c))
                {
                    var _MT = _c.MenuTags.Where(m => m.CustomerID == Model.CustomerID && m.MenuTagName.ToLower().Equals(Model.Tag.ToLower())).SingleOrDefault();
                    if (_MT == null)
                    {
                        _MT = new MenuTag
                        {
                            CustomerID = Model.CustomerID,
                            MenuTagName = Model.Tag,
                            Menus = new List<Menu>()
                        };
                        _c.MenuTags.Add(_MT);
                    }
                    var _M = _c.Menus.Where(m => m.MenuID == Model.MenuID).SingleOrDefault();
                    _M.Title = Model.Title;
                    _M.Subtitle = Model.Subtitle;
                    _M.Price = Model.Price;
                    _M.MenuTagID = _MT.MenuTagID;
                    _c.SaveChanges();
                }
            }
        }
        public void Create(MenuTagBinding Model, string UserID)
        {
            using (var _c = db)
            {
                if (Can(Model.CustomerID, UserID, _c))
                {
                    var _MT = _c.MenuTags.Where(m => m.MenuTagID == Model.MenuTagID).SingleOrDefault();
                    _MT.MenuTagName = Model.MenuTagName;
                    _c.SaveChanges();
                }
            }
        }
        public void DeleteTag(int MenuTagID, string UserID)
        {
            using (var _c = db)
            {
                var _MT = _c.MenuTags.Where(m => m.MenuTagID == MenuTagID).SingleOrDefault();
                if (Can(_MT.CustomerID, UserID, _c))
                {
                    _c.MenuTags.Remove(_MT);
                    _c.SaveChanges();
                }
            }
        }
        public void DeleteItem(int MenuID, string UserID)
        {
            using (var _c = db)
            {
                var _MT = _c.Menus.Where(m => m.MenuID == MenuID).SingleOrDefault();
                if (Can(_MT.MenuTag.CustomerID, UserID, _c))
                {
                    _c.Menus.Remove(_MT);
                    _c.SaveChanges();
                }
            }
        }
        public void AddAdditional(MenuAdditionalBinding Model, string UserID)
        {
            using (var _c = db)
            {
                var _MT = _c.Additionals.Where(m => m.AdditionalID == Model.AdditionalID).SingleOrDefault();
                if (Can(_MT.CustomerID, UserID, _c))
                {
                    var _MN = new MenuAdditional
                    {
                        MenuID = Model.MenuID,
                        AdditionalID = Model.AdditionalID,
                        MenuAdditionalName = Model.MenuAdditionalName,
                        Required = Model.Required
                    };
                    _c.MenuAdditionals.Add(_MN);
                    _c.SaveChanges();
                }
            }
        }
        public void DeleteAdditional(int MenuAdditionalID, string UserID)
        {
            using (var _c = db)
            {
                var _MA = _c.MenuAdditionals.Where(m => m.MenuAdditionalID == MenuAdditionalID).SingleOrDefault();
                if (Can(_MA.Menu.MenuTag.CustomerID, UserID, _c))
                {
                    _c.MenuAdditionals.Remove(_MA);
                    _c.SaveChanges();
                }
            }
        }
        public void Disable(MenuDisabledBinding Model, string UserID)
        {
            using (var _c = db)
            {
                var _M = _c.Menus.Where(m => m.MenuID == Model.MenuID).FirstOrDefault();
                if (Can(_M.MenuTag.CustomerID, UserID, _c))
                {
                    _M.Disabled = Model.Disabled;
                    _c.SaveChanges();
                }
            }
        }
        public static bool Can(int CustomerID, string UserID, ApplicationDbContext _c)
        {
            var _Can = _c.CustomerUserRoles.Where(m => m.CustomerUserType == CustomerUserType.Menu
                    && m.UserID.Equals(UserID) && m.CustomerID == CustomerID).Any();
            if (_Can)
                return _Can;
            else throw new RuleException("Ese usuario no tiene permiso para manejar el menu");
        }
    }
}
