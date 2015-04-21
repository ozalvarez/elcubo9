using elcubo9.bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using elcubo9.data.binding;

namespace elcubo9.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        OrderBLL OrderService = new OrderBLL();
        public IHttpActionResult Get(int CustomerID)
        {
            return Ok(OrderService.GetOrders(CustomerID, User.Identity.GetUserId()));
        }
        public IHttpActionResult Get(int CustomerID, int Paging)
        {
            return Ok(OrderService.GetOrders(40, Paging,CustomerID, User.Identity.GetUserId()));
        }
        [Authorize(Roles = "admin")]
        public IHttpActionResult GetAll(int Paging = 1, int CustomerID=0)
        {
            return Ok(OrderService.GetOrders(40, Paging, CustomerID));
        }
        [Route("by-id")]
        public IHttpActionResult GetOrder(int OrderID)
        {
            return Ok(OrderService.GetOrder(OrderID, User.Identity.GetUserId()));
        }
        [Route("my")]
        public IHttpActionResult GetMyOrder(int OrderID)
        {
            return Ok(OrderService.GetMyOrder(OrderID, User.Identity.GetUserId()));
        }
        [Route("my")]
        public IHttpActionResult GetMy()
        {
            return Ok(OrderService.GetMyOrders(User.Identity.GetUserId()));
        }
        public IHttpActionResult Post(OrderBinding Model)
        {
            return Ok(OrderService.CreateOrder(Model, User.Identity.GetUserId()));
        }
        [Route("change-status")]
        public IHttpActionResult ChangeStatus(ChangeStatusBinding Model)
        {
            OrderService.ChangeStatus(Model, User.Identity.GetUserId());
            return Ok();
        }

    }
}
