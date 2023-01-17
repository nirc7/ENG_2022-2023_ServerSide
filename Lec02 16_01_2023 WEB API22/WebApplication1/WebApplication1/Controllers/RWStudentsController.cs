using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/RWStudents")]
    public class RWStudentsController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                //throw new Exception("what!");
                return Ok(StudentsDBMock.studnets);
            }
            catch (Exception e)
            {
                return BadRequest("the get was not done! " + e.Message);
            }
        }

        [Route("{isAvi:bool}")]
        public IHttpActionResult Get(bool isAvi)
        {
            try
            {
                Student stu2Get = StudentsDBMock.studnets.FirstOrDefault(stu => stu.Name == "avi");
                if ((stu2Get != null && isAvi) || (stu2Get == null && !isAvi))
                {
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound, 
                    $"student with name=avi was not found when searched " +
                    $"or found when not searched!in get !");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Student stu2Get = StudentsDBMock.studnets.FirstOrDefault(stu => stu.Id == id);
                if (stu2Get != null)
                {
                    return Ok(stu2Get);
                }
                return Content(HttpStatusCode.NotFound, $"student with id = {id} was not found in get!");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("{id}/grade")]
        [Route("~/sg/{id}")]
        public IHttpActionResult GetGrade(int id)
        {
            try
            {
                Student stu2Get = StudentsDBMock.studnets.FirstOrDefault(stu => stu.Id == id);
                if (stu2Get != null)
                {
                    return Ok(stu2Get.Grade);
                }
                return Content(HttpStatusCode.NotFound, $"student with id = {id} was not found in get grade!");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
            }
        }




        public IHttpActionResult Post([FromBody] Student value)
        {
            try
            {
                StudentsDBMock.studnets.Add(value);
                return Created(new Uri(Request.RequestUri.AbsoluteUri + value.Id), value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Put(int id, [FromBody] Student value)
        {
            try
            {
                Student stu2Update = StudentsDBMock.studnets.FirstOrDefault(stu => stu.Id == id);
                if (stu2Update != null)
                {
                    stu2Update.Name = value.Name;
                    stu2Update.Grade = value.Grade;
                    return Ok(stu2Update);
                }
                return Content(HttpStatusCode.NotFound, $"student with id = {id} was not found in PUT!");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Student stu2Delete = StudentsDBMock.studnets.FirstOrDefault(stu => stu.Id == id);
                if (stu2Delete != null)
                {
                    StudentsDBMock.studnets.Remove(stu2Delete);
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound, $"student with id = {id} was not found in Delete!");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e);
            }
        }


    }
}
