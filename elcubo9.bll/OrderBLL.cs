using elcubo9.bll.Exceptions;
using elcubo9.bll.Utils;
using elcubo9.data;
using elcubo9.data.binding;
using elcubo9.data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.bll
{
    public class OrderBLL : BaseBLL
    {
        public OrderBinding GetOrder(int OrderID, string UserID)
        {
            using (var _c = db)
            {
                var _Order = (from o in _c.Orders
                              where o.OrderID == OrderID
                              select new OrderBinding
                              {
                                  OrderID = o.OrderID,
                                  CustomerID = o.CustomerID,
                                  TableNumber = o.TableNumber,
                                  DateOrder = o.DateOrder,
                                  OrderStatus = o.OrderStatus,
                                  NameOfUser = o.User.Name,
                                  Items = o.OrderMenus.Select(m => new OrderItemBinding
                                  {
                                      Title = m.Menu.Title,
                                      AdditionalInfo = m.AdditionalInfo,
                                      MenuID = m.MenuID,
                                      Quantity = m.Quantity,
                                      Price = m.Menu.Price,
                                      Items = m.OrderMenuAdditionals.Select(m2 => new OrderItemAdditionalBinding
                                      {
                                          AdditionalItemID = m2.AdditionalItemID,
                                          AdditionalItemName = m2.AdditionalItem.AdditionalItemName,
                                          Price = m2.AdditionalItem.Price

                                      })
                                  })
                              }).SingleOrDefault();
                Can(_Order.CustomerID, UserID, _c, CustomerUserType.Orders);
                return _Order;
            }
        }
        public List<OrderBinding> GetOrders(int CustomerID, string UserID)
        {

            using (var _c = db)
            {
                Can(CustomerID, UserID, _c, CustomerUserType.Orders);
                var _Q = (from o in _c.Orders
                          where o.CustomerID == CustomerID && o.OrderStatus != OrderStatus.DELIVERED && o.OrderStatus != OrderStatus.INVALID
                          select new
                          {
                              OB = new OrderBinding
                              {
                                  OrderID = o.OrderID,
                                  CustomerID = o.CustomerID,
                                  TableNumber = o.TableNumber,
                                  DateOrder = o.DateOrder,
                                  OrderStatus = o.OrderStatus,
                                  NameOfUser = o.User.Name,
                                  UserID = o.UserID,
                                  Items = o.OrderMenus.Select(m => new OrderItemBinding
                                  {
                                      Title = m.Menu.Title,
                                      AdditionalInfo = m.AdditionalInfo,
                                      MenuID = m.MenuID,
                                      Quantity = m.Quantity,
                                      Price = m.Menu.Price,
                                      Items = m.OrderMenuAdditionals.Select(m2 => new OrderItemAdditionalBinding
                                      {
                                          AdditionalItemID = m2.AdditionalItemID,
                                          AdditionalItemName = m2.AdditionalItem.AdditionalItemName,
                                          Price = m2.AdditionalItem.Price

                                      })
                                  })
                              },
                              O = o
                          }).OrderByDescending(m => m.O.OrderID);
                foreach (var item in _Q.Select(m => m.O).Where(m => !m.Received).ToList())
                {
                    item.Received = true;
                    item.DateReceived = DateTime.Now;
                }
                _c.SaveChanges();
                return _Q.Select(m => m.OB).ToList();
            }
        }

        public List<OrderAllBinding> GetOrders(int Take, int Paging, int CustomerID, string UserID)
        {
            using (var _c = db)
            {
                Can(CustomerID, UserID, _c, CustomerUserType.Orders);
                return GetOrders(Take, Paging, CustomerID);
            }
        }
        public List<OrderAllBinding> GetOrders(int Take, int Paging, int CustomerID)
        {
            using (var _c = db)
            {
                var _Q = (from o in _c.Orders
                          where (CustomerID == 0 ? true : o.CustomerID == CustomerID)
                          select new
                          {
                              OB = new OrderAllBinding
                              {
                                  OrderID = o.OrderID,
                                  CustomerID = o.CustomerID,
                                  TableNumber = o.TableNumber,
                                  DateOrder = o.DateOrder,
                                  OrderStatus = o.OrderStatus,
                                  NameOfUser = o.User.Name,
                                  UserID = o.UserID,
                                  TimeInReceive = DbFunctions.DiffSeconds(o.DateOrder, o.DateReceived),
                                  Received = o.Received,
                                  CustomerName = o.Customer.CustomerName,
                                  Items = o.OrderMenus.Select(m => new OrderItemBinding
                                  {
                                      Title = m.Menu.Title,
                                      AdditionalInfo = m.AdditionalInfo,
                                      MenuID = m.MenuID,
                                      Quantity = m.Quantity,
                                      Price = m.Menu.Price,
                                      Items = m.OrderMenuAdditionals.Select(m2 => new OrderItemAdditionalBinding
                                      {
                                          AdditionalItemID = m2.AdditionalItemID,
                                          AdditionalItemName = m2.AdditionalItem.AdditionalItemName,
                                          Price = m2.AdditionalItem.Price

                                      })
                                  })
                              },
                              O = o
                          }).OrderByDescending(m => m.O.OrderID);
                return _Q.Select(m => m.OB).Skip(Take * (Paging - 1)).Take(Take).ToList();
            }
        }

        public List<OrderBinding> GetMyOrders(string UserID)
        {
            using (var _c = db)
            {

                return (from o in _c.Orders
                        where o.UserID.Equals(UserID)
                        select new OrderBinding
                        {
                            OrderID = o.OrderID,
                            CustomerID = o.CustomerID,
                            TableNumber = o.TableNumber,
                            DateOrder = o.DateOrder,
                            OrderStatus = o.OrderStatus,
                            Items = o.OrderMenus.Select(m => new OrderItemBinding
                            {
                                AdditionalInfo = m.AdditionalInfo,
                                MenuID = m.MenuID,
                                Quantity = m.Quantity,
                                Items = m.OrderMenuAdditionals.Select(m2 => new OrderItemAdditionalBinding
                                {
                                    AdditionalItemID = m2.AdditionalItemID
                                })
                            })
                        }).OrderByDescending(m => m.OrderID).ToList();
            }
        }
        public OrderBinding GetMyOrder(int OrderID, string UserID)
        {
            using (var _c = db)
            {
                return (from o in _c.Orders
                        where o.OrderID == OrderID && o.UserID.Equals(UserID)
                        select new OrderBinding
                        {
                            OrderID = o.OrderID,
                            CustomerID = o.CustomerID,
                            TableNumber = o.TableNumber,
                            DateOrder = o.DateOrder,
                            OrderStatus = o.OrderStatus,
                            Items = o.OrderMenus.Select(m => new OrderItemBinding
                            {
                                AdditionalInfo = m.AdditionalInfo,
                                MenuID = m.MenuID,
                                Quantity = m.Quantity,
                                Price = m.Menu.Price,
                                Title = m.Menu.Title,
                                Subtitle = m.Menu.Subtitle,
                                Items = m.OrderMenuAdditionals.Select(m2 => new OrderItemAdditionalBinding
                                {
                                    AdditionalItemID = m2.AdditionalItemID
                                })
                            })
                        }).SingleOrDefault();
            }
        }
        public int CreateOrder(OrderBinding Model, string UserID)
        {
            if (Model == null)
                throw new RuleException("Order OBJECT is Null, please check");
            if (string.IsNullOrEmpty(Model.TableNumber))
                throw new RuleException("Debe ingresar la Mesa donde está");
            using (var _c = db)
            {
                var _Order = new Order
                {
                    DateOrder = DateTime.Now,
                    TableNumber = Model.TableNumber,
                    UserID = UserID,
                    OrderID = Model.CustomerID,
                    CustomerID = Model.CustomerID,
                    OrderStatus = data.binding.OrderStatus.SENT,
                    OrderMenus = new List<OrderMenu>(),
                    Received = false,
                    DateReceived = DateTime.Now
                };
                foreach (var item in Model.Items)
                {
                    var _OM = new OrderMenu
                    {
                        MenuID = item.MenuID,
                        Quantity = item.Quantity,
                        AdditionalInfo = item.AdditionalInfo,
                        OrderMenuAdditionals = new List<OrderMenuAdditional>()
                    };

                    foreach (var item2 in item.Items)
                    {
                        _OM.OrderMenuAdditionals.Add(new OrderMenuAdditional
                        {
                            AdditionalItemID = item2.AdditionalItemID
                        });
                    }
                    _Order.OrderMenus.Add(_OM);
                }
                _c.Orders.Add(_Order);
                _c.SaveChanges();
                // SEND EMAILS TO CUSTOMERS
                new EmailAccess().SendEmailToCustomers(_Order.OrderID);
                //Task _Task = new EmailAccess().SendEmailToCustomersAsync(_Order.OrderID);
                return _Order.OrderID;
            }
        }
        public void ChangeStatus(ChangeStatusBinding Model, string UserID)
        {
            using (var _c = db)
            {
                var _M = _c.Orders.Where(m => m.OrderID == Model.OrderID).SingleOrDefault();
                Can(_M.CustomerID, UserID, _c, CustomerUserType.Orders);
                _M.OrderStatus = Model.OrderStatus;
                _c.SaveChanges();
            }
        }
    }
}
