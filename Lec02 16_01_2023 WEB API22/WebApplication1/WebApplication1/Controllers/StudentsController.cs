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
        //Select/Read ALL
        public List<Student> Get()
        {
            return StudentsDBMock.studnets;
        }

        //SELECT/Read By ID
        public Student Get(int id)
        {
            return StudentsDBMock.studnets.FirstOrDefault(stu => stu.Id == id);
        }

        //INSERT/Create
        public int Post([FromBody] Student value)
        {
            StudentsDBMock.studnets.Add(value);
            return value.Id;
        }

        //Update
        public string Put(int id, [FromBody] Student value)
        {
            Student stu2Update = StudentsDBMock.studnets.FirstOrDefault(stu => stu.Id == id);
            stu2Update.Name = value.Name;
            stu2Update.Grade = value.Grade;
            return "done:)";
        }

        //Delete
        public IHttpActionResult Delete(int id)
        {
            Student stu2Del = StudentsDBMock.studnets.FirstOrDefault(stu=> stu.Id == id);
            StudentsDBMock.studnets.Remove(stu2Del);
            var v = new { Result= "delete successfully!" };
            var j = Json(v);
            return j;
        }


    }
}
