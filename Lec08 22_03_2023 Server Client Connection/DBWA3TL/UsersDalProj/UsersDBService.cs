using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UsersDalProj
{
    public static class UsersDBService
    {
        //static string conStr = @"Data Source=LAB-G700;Initial Catalog=DBUsers;Integrated Security=True";
        static bool isLocal = true;
        static string conStr = ConfigurationManager.ConnectionStrings[isLocal ? "localDB" : "RuppinDB"].ConnectionString;
        static SqlConnection con = new SqlConnection(conStr);


        public static User[] GetAllUsers()
        {
            return GetUsersByQuery(
                " SELECT * " +
                " FROM TBUsers");
        }

        private static User[] GetUsersByQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Connection.Open();
            List<User> users = new List<User>();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User()
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Family = (string)reader["Family"]
                });
            }
            cmd.Connection.Close();
            return users.ToArray();
        }

        public static List<User> GetUsersGreaterThenId(int id)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(
                " SELECT * " +
                " FROM TBUsers " +
                " WHERE id > " + id, con);
            adapter.Fill(ds, "T1");
            DataTable usersOverId = ds.Tables["T1"];
            List<User> userList = new List<User>();

            foreach (DataRow userRow in usersOverId.Rows)
            {
                userList.Add(new User() {
                    Id = (int)userRow["ID"],
                    Name = (string)userRow["Name"],
                    Family = (string)userRow["Family"],
                });
            }
            return userList;
        }

        public static User GetUserById(int id)
        {
            return GetUsersByQuery(
                " SELECT * " +
                " FROM TBUsers " +
                " WHERE Id=" + id)[0];
        }
    }
}