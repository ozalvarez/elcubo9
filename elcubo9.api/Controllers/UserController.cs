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
    [RoutePrefix("api/user")]
    [Authorize(Roles = "admin")]
    public class UserController : ApiController
    {
        private UserBLL UserService = new UserBLL();
        public IHttpActionResult Get(string Text = null, int Paging = 1)
        {
            return Ok(UserService.Get(40, Paging, Text));
        }
        [Route("getNumUsers")]
        public IHttpActionResult GetNumUsers()
        {
            return Ok(UserService.GetNumUsers());
        }
        [Route("getNumUsersByDay")]
        public IHttpActionResult GetNumUsersByDay(string UTCDate)
        {
            DateTime _Date = DateTime.Parse(UTCDate);
            return Ok(UserService.GetNumUsersByDay(_Date));
        }
        [Route("getNumUsersByPeriod")]
        public IHttpActionResult GetNumUsersByPeriod(string Start, string End)
        {
            DateTime _DateStart = DateTime.Parse(Start);
            DateTime _DateEnd = DateTime.Parse(End);
            return Ok(UserService.GetNumUsersByPeriod(_DateStart, _DateEnd));
        }
        [Route("block")]
        public IHttpActionResult PostBlock(UserBlockBinding UserBlock)
        {
            UserService.Block(UserBlock);
            return Ok();
        }
    }
}
