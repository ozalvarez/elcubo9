using elcubo9.bll;
using elcubo9.bll.Exceptions;
using elcubo9.data.binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace elcubo9.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/menu")]
    public class MenuController : ApiController
    {
        MenuBLL MenuService = new MenuBLL();
        OrderBLL OrderService = new OrderBLL();

        public IHttpActionResult Get(int CustomerID)
        {
            return Ok(MenuService.Get(CustomerID));
        }
        [Route("get-all")]
        public IHttpActionResult GetAll(int CustomerID)
        {
            return Ok(MenuService.Get(CustomerID, true, User.Identity.GetUserId()));
        }
        public IHttpActionResult PostSave(MenuBinding Model)
        {
            MenuService.Create(Model, User.Identity.GetUserId());
            return Ok();
        }
        public IHttpActionResult PutUpdate(MenuBinding Model)
        {
            MenuService.Update(Model, User.Identity.GetUserId());
            return Ok();
        }
        public IHttpActionResult DeleteItem(int MenuID)
        {
            MenuService.DeleteItem(MenuID, User.Identity.GetUserId());
            return Ok();
        }
        [Route("update-tag")]
        public IHttpActionResult PutUpdateTag(MenuTagBinding Model)
        {
            MenuService.Create(Model, User.Identity.GetUserId());
            return Ok();
        }
        [Route("tag")]
        public IHttpActionResult DeleteTag(int MenuTagID)
        {
            MenuService.DeleteTag(MenuTagID, User.Identity.GetUserId());
            return Ok();
        }

        [Route("add-additional")]
        public IHttpActionResult PostAddAdditional(MenuAdditionalBinding Model)
        {
            MenuService.AddAdditional(Model, User.Identity.GetUserId());
            return Ok();
        }
        [Route("delete-additional")]
        public IHttpActionResult DeleteAdditional(int MenuAdditionalID)
        {
            MenuService.DeleteAdditional(MenuAdditionalID, User.Identity.GetUserId());
            return Ok();
        }
        [Route("disable")]
        public IHttpActionResult PostDisbale(MenuDisabledBinding Model)
        {
            MenuService.Disable(Model, User.Identity.GetUserId());
            return Ok();
        }
    }
}
