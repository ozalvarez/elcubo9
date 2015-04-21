using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elcubo9.api.Hubs.BLL
{
    public enum TypeConnection
    {
        WEB, CUSTOMER, ADMIN
    }
    public class Connection
    {
        public TypeConnection Type { get; set; }
        public string ConnectionId { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public DateTime DateConnected { get; set; }
    }
    public class ConnectionHubBLL
    {
        public static List<Connection> _Connections { get; set; }
        public ConnectionHubBLL()
        {
            if(_Connections==null)
            _Connections = new List<Connection>();
        }
        public List<Connection> Get() { return _Connections.OrderByDescending(m=>m.DateConnected).ToList(); }
        public void Add(string ConnectionId, string Name, TypeConnection Type)
        {

            if (_Connections.Where(m => m.ConnectionId == ConnectionId).Any())
            {
                var _CToRemove = _Connections.Where(m => m.ConnectionId == ConnectionId).FirstOrDefault();
                _CToRemove.DateConnected = DateTime.Now;
            }
            else
            {
                _Connections.Add(new Connection
                {
                    ConnectionId = ConnectionId,
                    UserName = Name,
                    DateConnected = DateTime.Now,
                    Type = Type
                });
            }
        }
        public void Remove(string ConnectionId)
        {
            var _CToRemove = _Connections.Where(m => m.ConnectionId == ConnectionId).FirstOrDefault();
            _Connections.Remove(_CToRemove);
        }
    }
}