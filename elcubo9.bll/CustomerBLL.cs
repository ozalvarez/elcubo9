using elcubo9.bll.Exceptions;
using elcubo9.bll.Utils;
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
    public class CustomerBLL : BaseBLL
    {
        public IEnumerable<CustomerBinding> GetAll()
        {
            using (var _c = db)
            {
                return _c.Customers.Select(m => new CustomerBinding
                {
                    CustomerID = m.CustomerID,
                    CustomerImg = m.CustomerImg,
                    CustomerName = m.CustomerName,
                    DateCreated = m.DateCreated,
                    Enabled = m.Enabled,
                    Symbol = m.Symbol
                }).ToList();
            }
        }
        public IEnumerable<CustomerBinding> Get()
        {
            using (var _c = db)
            {
                return _c.Customers.Where(m => m.Enabled).Select(m => new CustomerBinding
                {
                    CustomerID = m.CustomerID,
                    CustomerImg = m.CustomerImg,
                    CustomerName = m.CustomerName,
                    DateCreated = m.DateCreated,
                    Enabled = m.Enabled,
                    Symbol=m.Symbol
                }).ToList();
            }
        }
        public List<CustomerByUserBinding> Get(string UserID)
        {
            using (var _c = db)
            {
                return _c.CustomerUsers.Where(m => m.UserID.Equals(UserID)).Select(m => new CustomerByUserBinding
                {
                    CustomerID = m.CustomerID,
                    CustomerImg = m.Customer.CustomerImg,
                    CustomerName = m.Customer.CustomerName,
                    DateCreated = m.Customer.DateCreated,
                    Enabled = m.Customer.Enabled,
                    Roles = m.CustomerUserRoles.Select(m2 => m2.CustomerUserType),
                    Symbol=m.Customer.Symbol
                }).ToList();
            }
        }
        public Customer Save(CustomerBinding Model)
        {
            using (var _c = db)
            {
                var _Model = new Customer
                    {
                        CustomerName = Model.CustomerName,
                        DateCreated = DateTime.Now,
                        Enabled = false,
                        Symbol=Model.Symbol
                    };
                if (Model.CustomerID == 0)
                    _c.Customers.Add(_Model);
                else
                {
                    _Model = _c.Customers.Where(m => m.CustomerID == Model.CustomerID).SingleOrDefault();
                    _Model.CustomerName = Model.CustomerName;
                    _Model.Symbol = Model.Symbol;
                }
                _c.SaveChanges();
                return _Model;
            }
        }
        public void Delete(int CustomerID)
        {
            using (var _c = db)
            {
                _c.MenuTags.RemoveRange(_c.MenuTags.Where(m => m.CustomerID == CustomerID).ToList());
                _c.SaveChanges();
                _c.Customers.Remove(_c.Customers.Where(m => m.CustomerID == CustomerID).SingleOrDefault());
                _c.SaveChanges();
            }
        }
        public void Enable(CustomerEnabledBinding Model)
        {
            using (var _c = db)
            {
                var _Model = _c.Customers.Where(m => m.CustomerID == Model.CustomerID).SingleOrDefault();
                _Model.Enabled = Model.Enabled;
                _c.SaveChanges();
            }
        }
        public void AddUser(AddUserInCustomerBinding Model)
        {
            var _PWD = AddUserToAllRoles(Model);
            new EmailAccess().AddCustomerUser(Model.Email, _PWD);
        }
        public string AddUserToAllRoles(AddUserInCustomerBinding Model)
        {
            using (var _c = db)
            {
                var _AU = new UserBLL().Find(Model.Email);
                var _Pwd = String.Empty;
                if (_AU == null)
                {
                    _Pwd = Util.RandomString(7);
                    _AU = new UserBLL().Create(Model.Email, _Pwd);
                }
                var _CU = _c.CustomerUsers.Where(m => m.UserID == _AU.Id && m.CustomerID == Model.CustomerID).SingleOrDefault();
                if (_CU == null)
                {
                    _CU = new CustomerUser
                    {
                        CustomerID = Model.CustomerID,
                        UserID = _AU.Id,
                        CustomerUserRoles = new List<CustomerUserRol>()
                    };
                    _c.CustomerUsers.Add(_CU);
                }
                _c.CustomerUserRoles.RemoveRange(_c.CustomerUserRoles.Where(m => m.UserID == _AU.Id && m.CustomerID == Model.CustomerID));
                var _CR1 = new CustomerUserRol
                {
                    CustomerID = Model.CustomerID,
                    UserID = _AU.Id,
                    CustomerUserType = CustomerUserType.Menu
                };
                var _CR2 = new CustomerUserRol
               {
                   CustomerID = Model.CustomerID,
                   UserID = _AU.Id,
                   CustomerUserType = CustomerUserType.Orders
               };
                var _CR4 = new CustomerUserRol
                {
                    CustomerID = Model.CustomerID,
                    UserID = _AU.Id,
                    CustomerUserType = CustomerUserType.Configuration
                };
                _c.CustomerUserRoles.Add(_CR1);
                _c.CustomerUserRoles.Add(_CR2);
                _c.CustomerUserRoles.Add(_CR4);
                _c.SaveChanges();
                return _Pwd;
            }
        }
        public void AddUserToRol(CustomerAddRol Model)
        {
            using (var _c = db)
            {
                var _Model = _c.CustomerUserRoles.Where(m => m.UserID == Model.UserID && m.CustomerID == Model.CustomerID
                    && m.CustomerUserType == Model.CustomerUserType).SingleOrDefault();
                if (_Model == null)
                {
                    if (Model.Enabled)
                    {
                        _Model = new CustomerUserRol
                        {
                            CustomerID = Model.CustomerID,
                            CustomerUserType = Model.CustomerUserType,
                            UserID = Model.UserID
                        };
                        _c.CustomerUserRoles.Add(_Model);
                    }
                }
                else
                {
                    if (!Model.Enabled)
                    {
                        _c.CustomerUserRoles.Remove(_Model);
                    }
                }
                _c.SaveChanges();
            }
        }
        public List<CustomerListUsers> GetUsers(int CustomerID)
        {
            using (var _c = db)
            {
                return (from c in _c.CustomerUsers
                        where c.CustomerID == CustomerID
                        select new CustomerListUsers
                        {
                            UserID = c.UserID,
                            Email = c.User.Email,
                            Name = c.User.Name,
                            Roles = c.CustomerUserRoles.Select(m => m.CustomerUserType)
                        }).ToList();
            }
        }
        public void DeleteUser(string UserID, int CustomerID)
        {
            using (var _c = db)
            {
                _c.CustomerUsers.Remove(_c.CustomerUsers.Where(m => m.CustomerID == CustomerID && m.UserID == UserID).SingleOrDefault());
                _c.SaveChanges();
            }
        }
        public void AddEmail(CustomerEmailBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.CustomerID, UserID, _c, CustomerUserType.Configuration);
                if (_c.CustomerEmails.Where(m => m.Email.ToLower().Equals(Model.Email.ToLower()) && m.CustomerID==Model.CustomerID).Any())
                    throw new RuleException("Email " + Model.Email + " ya existe en el cliente");
                _c.CustomerEmails.Add(new CustomerEmail
                {
                    CustomerID = Model.CustomerID,
                    Email = Model.Email
                });
                _c.SaveChanges();
            }
        }
        public void DeleteEmail(int CustomerEmailID, string UserID)
        {
            using (var _c = db)
            {
                var model = _c.CustomerEmails.Where(m => m.CustomerEmailID == CustomerEmailID).SingleOrDefault();
                Can(model.CustomerID, UserID, _c, CustomerUserType.Configuration);
                _c.CustomerEmails.Remove(model);
                _c.SaveChanges();
            }
        }
        public List<CustomerEmailBinding> GetEmails(int CustomerID, string UserID)
        {
            using (var _c = db)
            {
                Can(CustomerID, UserID, _c, CustomerUserType.Configuration);
                return _c.CustomerEmails.Where(m => m.CustomerID == CustomerID).Select(m => new CustomerEmailBinding
                {
                    CustomerEmailID = m.CustomerEmailID,
                    CustomerID = m.CustomerID,
                    Email = m.Email
                }).ToList();
            }
        }

        #region TEST
        public Customer GetCustomer(String CustomerName)
        {
            using (var _c = db)
            {
                var _Customers = _c.Customers.Where(m => m.CustomerName.ToLower().Equals(CustomerName.ToLower())).ToList();
                if (_Customers.Any())
                    return _Customers[0];
                else return null;
            }
        }
        #endregion
    }
}
