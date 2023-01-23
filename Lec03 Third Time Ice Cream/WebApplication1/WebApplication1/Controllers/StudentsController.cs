using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentsController : ApiController
    {
        //api/students
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(StudentsDBMock.students);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //api/students/2
        public IHttpActionResult Get(int id)
        {
            try
            {
                Student stu2Return = StudentsDBMock.students.FirstOrDefault(stu => stu.ID == id);
                if (stu2Return != null)
                {
                    return Ok(stu2Return);
                }
                return Content(HttpStatusCode.NotFound, $"the student with id={id} was not found in GET!!!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("~/sg/{id}")]
        public IHttpActionResult GetGrades(int id)
        {
            try
            {
                Student stu2Return = StudentsDBMock.students.FirstOrDefault(stu => stu.ID == id);
                if (stu2Return != null)
                {
                    return Ok(stu2Return.Grade);
                }
                return Content(HttpStatusCode.NotFound, $"the student with id={id} was not found in GET!!!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        public IHttpActionResult Post([FromBody] Student value) 
        {
            try
            {
                StudentsDBMock.students.Add(value);
                return Content(HttpStatusCode.Created, value);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
            }         
        }

        //update
        public IHttpActionResult Put(int id, [FromBody] Student value)
        {
            try
            {
                Student stu2Update = StudentsDBMock.students.FirstOrDefault(stu => stu.ID == id);
                if (stu2Update != null)
                {
                    stu2Update.Name = value.Name;
                    //stu2Update.Grade = value.Grade;
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound, $"the student with id={id} was not found in PUT!!!");
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
                Student stu2Del = StudentsDBMock.students.FirstOrDefault(stu => stu.ID == id);
                if (stu2Del != null)
                {
                    StudentsDBMock.students.Remove(stu2Del);
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound, $"the student with id={id} was not found in Del!!!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
