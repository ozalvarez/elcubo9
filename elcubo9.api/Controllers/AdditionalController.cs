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
    [RoutePrefix("api/additional")]
    public class AdditionalController : ApiController
    {
        AdditionalBLL AdditionalService = new AdditionalBLL();
        public IHttpActionResult Get(int CustomerID)
        {
            return Ok(AdditionalService.Get(CustomerID));
        }
        public IHttpActionResult PostSave(AdditionalBinding Model)
        {
                AdditionalService.Save(Model, User.Identity.GetUserId());
                return Ok();
        }
        public IHttpActionResult PutUpdate(AdditionalBinding Model)
        {

                AdditionalService.Update(Model, User.Identity.GetUserId());
                return Ok();
        }
        public IHttpActionResult Delete(int AdditionalID)
        {
                AdditionalService.Delete(AdditionalID, User.Identity.GetUserId());
                return Ok();
        }
        [Route("add-item")]
        public IHttpActionResult AddItem(AdditionalItemBinding Model)
        {
                AdditionalService.AddItem(Model, User.Identity.GetUserId());
                return Ok();
        }
        [Route("update-item")]
        public IHttpActionResult PutUpdateItem(AdditionalItemBinding Model)
        {
                AdditionalService.UpdateItem(Model, User.Identity.GetUserId());
                return Ok();

        }
        [Route("delete-item")]
        public IHttpActionResult DeleteItem(int AdditionalItemID)
        {
                AdditionalService.DeleteItem(AdditionalItemID, User.Identity.GetUserId());
                return Ok();
        }
    }
}
