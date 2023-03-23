using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UsersBLLProj;
using UsersDalProj;

namespace DBWA3TL.Controllers
{
    public class UsersController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                UserRole ur = UserRole.ADMIN;
                User[] users = UsersBLL.GetAllUsersFromDB(ur);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [Route("{id:int}", Name = "GetById")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                User user = UsersBLL.GetUserById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Post(int id)
        {
            try
            {
                return Ok(UsersBLL.GetUsersGreaterThenId(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.Conflict, e);
            }
        }

        public IHttpActionResult Put(int id, [FromBody] User u)
        {
            try
            {
                if (UsersBLL.UpdateUser(id, u))
                    return Ok();
                else
                    return Content(HttpStatusCode.NotFound,$"user with id={id} was not found for UPDATE!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (UsersBLL.DeleteUser(id))
                    return Ok();
                else
                    return Content(HttpStatusCode.NotFound, $"user with id={id} was not found for DELETE!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult InsertUser([FromBody] User u)
        {
            try
            {
                User nu = UsersBLL.InsertNewUser(u);
                if (nu != null)
                {
                    //return Created(new Uri(Url.Link("GetById", new { id = nu.Id })), nu);
                    
                    //opt1
                    //string req = Request.RequestUri.AbsoluteUri;
                    //int len = req.Length;
                    //return Created(new Uri(req + (req[len - 1] == '/' ? "" : "/") + u.Id), nu);

                    //opt2
                    return Content(HttpStatusCode.Created, nu);
                }
                return Content(HttpStatusCode.InternalServerError, $"user with name={u.Name} was not inserted 2 DB! ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
