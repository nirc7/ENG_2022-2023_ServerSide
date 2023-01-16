using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public static class StudentsDBMock
    {
        public static List<Student> studnets = new List<Student>() {
            new Student(){Id=1 ,Name="avi" , Grade=100 },
            new Student(){Id=2 ,Name="dora" , Grade=97 },
            new Student(){Id=3 ,Name="chalrie" , Grade=98 },
            new Student(){Id=4 ,Name="benny" , Grade=99 },
        };

    }
}