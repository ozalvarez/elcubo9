using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using elcubo9.data.binding;
using elcubo9.bll.Exceptions;
using elcubo9.bll;
using elcubo9.api.ActionFilter;
using System.Security.Claims;
using Microsoft.AspNet.SignalR;
using elcubo9.api.Hubs.BLL;

namespace elcubo9.api.Hubs
{
    public class OrderHub : Hub
    {
        private OrderBLL OrderService = new OrderBLL();
        public ConnectionHubBLL ConnectionHubService { get; set; }
        public ClaimsPrincipal _User { get { return Context.Request.Environment["server.User"] as ClaimsPrincipal; } }
        public TypeConnection _Type
        {
            get
            {
                if (Context.QueryString["Type"] != null)
                {
                    if (Context.QueryString["Type"].Equals("WEB"))
                        return TypeConnection.WEB;
                    if (Context.QueryString["Type"].Equals("CUSTOMER"))
                        return TypeConnection.CUSTOMER;
                }
                return TypeConnection.ADMIN;
            }
        }
        public int _CustomerID { get { return Convert.ToInt32(Context.QueryString["CustomerID"]); } }
        public UserBLL UserService { get; set; }

        public OrderHub()
        {
            ConnectionHubService = new ConnectionHubBLL();
            UserService = new UserBLL();
        }

        public override Task OnConnected()
        {
            ConnectionHubService.Add(Context.ConnectionId, _User.Identity.Name, _Type);
            switch (_Type)
            {
                case TypeConnection.WEB:
                     Groups.Add(Context.ConnectionId, _User.Identity.GetUserId());
                    break;
                case TypeConnection.CUSTOMER:
                    Groups.Add(Context.ConnectionId, "C-" +_CustomerID);
                    break;
                case TypeConnection.ADMIN:
                    if (UserService.IsInRole(_User.Identity.GetUserId(), Role.ADMIN))
                    Groups.Add(Context.ConnectionId, "Admin");
                    break;
                default:
                    break;
            }
            RefreshConnections();
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ConnectionHubService.Remove(Context.ConnectionId);
            RefreshConnections();
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            if (_User != null)
            {
                ConnectionHubService.Add(Context.ConnectionId, _User.Identity.Name, _Type);
                switch (_Type)
                {
                    case TypeConnection.WEB:
                        Groups.Add(Context.ConnectionId, _User.Identity.GetUserId());
                        break;
                    case TypeConnection.CUSTOMER:
                        Groups.Add(Context.ConnectionId, "C-" + _CustomerID);
                        break;
                    case TypeConnection.ADMIN:
                        if (UserService.IsInRole(_User.Identity.GetUserId(), Role.ADMIN))
                            Groups.Add(Context.ConnectionId, "Admin");
                        break;
                    default:
                        break;
                }
                RefreshConnections();
            }
            return base.OnReconnected();
        }
        public void RefreshConnections()
        {
            Clients.Group("Admin").getConnections(ConnectionHubService.Get());
        }
        public void Send(string CustomerID, string OrderID)
        {
            if (!string.IsNullOrEmpty(CustomerID))
                Clients.Group("C-" + CustomerID).getPendingOrders(OrderID);
        }
        public void GetPendingOrders(string CustomerID)
        {
            if (!string.IsNullOrEmpty(CustomerID))
                Clients.Group("C-" + CustomerID).getPendingOrders();
        }
        public void ChangeStatus(int OrderID, string UserID, int CustomerID, OrderStatus Status)
        {
            Clients.Group(UserID).changeStatus(OrderID, Status);
            GetPendingOrders(CustomerID.ToString());
        }
    }
}