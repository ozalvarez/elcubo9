using elcubo9.data.Models;
using elcubo9.bll.Exceptions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elcubo9.bll.Utils;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using elcubo9.data.binding;
using System.Data.Entity;

namespace elcubo9.bll
{
    public enum Role
    {
        ADMIN
    }
    public class UserBLL : BaseBLL
    {
        public UserManager<ApplicationUser> UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        #region ADMIN
        public List<UserModel> Get(int Take, int Paging, string Text)
        {
            using (var context = db)
            {
                return (from u in context.Users
                        where (string.IsNullOrEmpty(Text) ? true : (u.Name.Contains(Text) || u.UserName.Contains(Text) || u.Email.Contains(Text)))
                        select new UserModel
                        {
                            UserID = u.Id,
                            Name = u.Name,
                            Username = u.UserName,
                            DateCreated = u.DateCreated,
                            Email=u.Email,
                            NumOrders=u.Orders.Count,
                            Block=u.Block
                        }).Distinct().OrderByDescending(m => m.DateCreated).Skip(Take * (Paging - 1)).Take(Take).ToList();
            }
        }
        public int GetNumUsers()
        {
            using (var context = db)
            {
                return context.Users.Count();
            }
        }
        public int GetNumUsersByDay(DateTime Date)
        {
            using (var context = db)
            {
                Date = Date.AddHours(-5);
                var DateDay = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0);
                var DateDayTomorrow = DateDay.AddDays(1);
                return context.Users.Where(m => DbFunctions.AddHours(m.DateCreated, -5) >= DateDay && DbFunctions.AddHours(m.DateCreated, -5) < DateDayTomorrow).Count();
            }
        }
        public int GetNumUsersByPeriod(DateTime Start, DateTime End)
        {
            using (var context = db)
            {
                End = End.AddDays(1);
                return context.Users.Where(m => DbFunctions.AddHours(m.DateCreated, -5) >= Start && DbFunctions.AddHours(m.DateCreated, -5) < End).Count();
            }
        }
        public void Block(UserBlockBinding UserBlock)
        {
            var _U = UserManager.FindById(UserBlock.UserID);
            _U.Block = UserBlock.Block;
            UserManager.Update(_U);
        }
        #endregion

        public Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var _AU = UserManager.FindByEmail(user.Email);
            if (_AU == null)
            {
                return UserManager.CreateAsync(user);
            }
            throw new RuleException("Ya existe un usuario con ese email");
        }
        public Task<IdentityResult> CreateAsync(RegisterBindingModel model)
        {

            var _AU = UserManager.FindByEmail(model.Email);
            if (_AU == null)
            {
                var user = new ApplicationUser(model.Email,model.Name);
                return UserManager.CreateAsync(user, model.Password);
            }
            throw new RuleException("Ya existe un usuario con ese email");
        }
        public ApplicationUser Create(string Email, string Password, string Name)
        {
            var _AU = UserManager.FindByEmail(Email);
            if (_AU == null)
            {
                _AU = new ApplicationUser(Email,Name);
                var _U = UserManager.Create(_AU, Password);
                if (_U.Succeeded)
                    return _AU;
                throw new RuleException("Error creando usuarios");

            }
            throw new RuleException("Ya existe un usuario con ese email");
        }
        public ApplicationUser Create(string Email, string Password)
        {
            return Create(Email, Password, Email);
        }
        

        public ApplicationUser Find(string Email)
        {
            return UserManager.FindByEmail(Email);
        }
        public ApplicationUser FindByName(string UserName)
        {
            return UserManager.FindByName(UserName);
        }


        public bool IsInRole(string UserID, Role Role)
        {
            switch (Role)
            {
                case Role.ADMIN:
                    return UserManager.IsInRole(UserID, "Admin");
            }
            return false;
        }
        
        public string VerifyAccessToken(string provider, string accessToken)
        {
            string verifyTokenEndPoint;
            string _UserIDProvider;
            if (provider == "Facebook")
                verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, FacebookAPPToken);
            else if (provider == "Google")
                verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
            else throw new RuleException("Verify Access Token - INVALID");
            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;

                dynamic jObj = (JObject)JsonConvert.DeserializeObject(content);

                if (provider == "Facebook")
                {
                    _UserIDProvider = jObj["data"]["user_id"];
                    string app_id = jObj["data"]["app_id"];

                    if (!string.Equals(FacebookAPPID, app_id, StringComparison.OrdinalIgnoreCase))
                        throw new RuleException("Verify Access Token - INVALID");
                    return _UserIDProvider;
                }
                else if (provider == "Google")
                {
                    _UserIDProvider = jObj["user_id"];
                    string app_id = jObj["audience"];

                    if (!string.Equals(GoogleAPPID, app_id, StringComparison.OrdinalIgnoreCase))
                        throw new RuleException("Verify Access Token - INVALID");
                    return _UserIDProvider;
                }
            }
            throw new RuleException("Verify Access Token - INVALID");
        }
    }
}
