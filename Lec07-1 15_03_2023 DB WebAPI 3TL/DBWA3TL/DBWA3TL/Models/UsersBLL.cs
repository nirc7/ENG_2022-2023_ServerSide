﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBWA3TL.Models
{
    public enum UserRole { ADMIN, USER, SUPERUSER }
    static public class UsersBLL
    {
        static public User[] GetAllUsersFromDB(UserRole ur)
        {
            User[] users = null;
            if (ur == UserRole.ADMIN)
            {
                users = (User[])UsersDBService.GetAllUsers();
            }
            else
            {
                users = UsersDBService.GetAllUsers().Where(user => user.Id != 1).ToArray();

            }
            return users;
        }

        public static User GetUserById(int id)
        {
            User user = UsersDBService.GetUserById(id);
            //...code algorithm...
            return user;
        }

        internal static List<User> GetUsersGreaterThenId(int id)
        {
            //code complex ocde ...
            return UsersDBService.GetUsersGreaterThenId(id);
        }
    }
}