using elcubo9.data;
using elcubo9.data.binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.bll
{
    public class AdditionalBLL : BaseBLL
    {
        public List<AdditionalBinding> Get(int CustomerID)
        {
            using (var _c = db)
            {
                return _c.Additionals.Where(m => m.CustomerID == CustomerID).Select(m => new AdditionalBinding
                {
                    AdditionalID = m.AdditionalID,
                    CustomerID = m.CustomerID,
                    AdditionalName = m.AdditionalName,
                    Items = m.Items.Select(m2 => new AdditionalItemBinding
                    {
                        AdditionalID = m2.AdditionalID,
                        AdditionalItemID = m2.AdditionalItemID,
                        AdditionalItemName = m2.AdditionalItemName,
                        Price = m2.Price
                    })
                }).ToList();
            }
        }
        public void Save(AdditionalBinding Model, string UserID)
        {
            using (var _c = db)
            {
                if (MenuBLL.Can(Model.CustomerID, UserID, _c))
                {
                    _c.Additionals.Add(new Additional
                    {
                        AdditionalName = Model.AdditionalName,
                        CustomerID = Model.CustomerID
                    });
                    _c.SaveChanges();
                }
            }
        }
        public void Update(AdditionalBinding Model, string UserID)
        {
            using (var _c = db)
            {
                var _MT = _c.Additionals.Where(m => m.AdditionalID == Model.AdditionalID).SingleOrDefault();
                if (MenuBLL.Can(_MT.CustomerID, UserID, _c))
                {
                    _MT.AdditionalName = Model.AdditionalName;
                    _c.SaveChanges();
                }
            }
        }
        public void Delete(int AdditionalID, string UserID)
        {
            using (var _c = db)
            {
                var _MT = _c.Additionals.Where(m => m.AdditionalID == AdditionalID).SingleOrDefault();
                if (MenuBLL.Can(_MT.CustomerID, UserID, _c))
                {
                    _c.Additionals.Remove(_MT);
                    _c.SaveChanges();
                }
            }
        }

        #region Items
        public int AddItem(AdditionalItemBinding Model, string UserID)
        {
            using (var _c = db)
            {
                MenuBLL.Can(Model.CustomerID, UserID, _c);
                var _Model = new AdditionalItem
                {
                    AdditionalID = Model.AdditionalID,
                    AdditionalItemName = Model.AdditionalItemName,
                    Price=Model.Price
                };
                _c.AdditionalItems.Add(_Model);
                _c.SaveChanges();
                return _Model.AdditionalID;
            }
        }
        public void UpdateItem(AdditionalItemBinding Model, string UserID)
        {
            using (var _c = db)
            {
                MenuBLL.Can(Model.CustomerID, UserID, _c);
                var _Model=_c.AdditionalItems.Where(m => m.AdditionalItemID == Model.AdditionalItemID).SingleOrDefault();
                _Model.AdditionalItemName = Model.AdditionalItemName;
                _Model.Price = Model.Price;
                _c.SaveChanges();
            }
        }
        public void DeleteItem(int AdditionalItemID, string UserID)
        {
            using (var _c = db)
            {
                var _MT = _c.AdditionalItems.Where(m => m.AdditionalItemID == AdditionalItemID).SingleOrDefault();
                if (MenuBLL.Can(_MT.Additional.CustomerID, UserID, _c))
                {
                    _c.AdditionalItems.Remove(_MT);
                    _c.SaveChanges();
                }
            }
        }
        #endregion
    }
}
