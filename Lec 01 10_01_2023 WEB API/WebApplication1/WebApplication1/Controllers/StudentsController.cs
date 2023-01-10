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
        public Student[] Get()
        {
            return DBStudentsMock.students.ToArray();
        }

        //api/students/2
        public Student Get(int id)
        {
            Student s = DBStudentsMock.students.FirstOrDefault(stu=> stu.Id == id);
            return s;
        }
    }
}
