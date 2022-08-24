using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using WebApi.Models;
using WebApi.Services;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userservice;

        public UserController(IUserService userservice)
        {
           _userservice = userservice;
        }


        public IEnumerable<User> GetUser()
        {
            
            return _userservice.GetAllUsers().ToList(); 
        }
        //public IEnumerable<User> GetUser(int pageNumber, int pageSize)
        //{

        //    var users= _userservice.GetUsersByPageNumber(pageNumber, pageSize);
        //    //var json = JsonConvert.SerializeObject(users);
        //    return users;
        //}
        
        
        public User GetUser(int id)
        {
            
            return _userservice.GetUserById(id);
        }
        [HttpPost]
        public HttpResponseMessage AddUser(User user)
        {
            try
            {
                _userservice.AddUser(user);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch(Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateUser(int id, User user)
        {
            try
            {
                if (id == user.id)
                {
                    _userservice.UpdateUser(id, user);
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }


        }

        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            User usermodel = _userservice.GetUserById(id);
            if (usermodel != null)
            {
                _userservice.Deleteuser(usermodel);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteUser(IEnumerable<int> ids)
        {
            
            
            if (ids != null)
            {
                _userservice.DeleteUser(ids);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
        public void Pipeline_Release3()
        {

        }


    }
}
