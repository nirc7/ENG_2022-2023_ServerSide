using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

        public static bool UpdateUser(User u)
        {
            return ExcNonQ(
                $" UPDATE TBUsers SET Name='{u.Name}' , Family='{u.Family}'" +
                 " WHERE Id=" + u.Id);
        }

        public static bool DeleteUser(int id)
        {
            return ExcNonQ($" DELETE TBUsers WHERE Id=" + id);
        }

        private static bool ExcNonQ(string query)
        {
            try
            {
                SqlCommand comm = new SqlCommand(
                           query, con);

                con.Open();
                int res = -1;
                res = comm.ExecuteNonQuery();
                throw new DataException("you should respect your teacher!");
                return res == 1;
            }
            catch (Exception e)
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                path = path.Replace(@"file:\", "") + @"\Error Logs.txt";

                File.AppendAllText (path ,
                    "________________\n" + e.Message + " \n-- userid=???\n -- " +
                    DateTime.Now.ToShortDateString() + " -- " + DateTime.Now.ToShortTimeString()  + "\n");
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return false;
        }

        public static User InsertUser2DB(User u)
        {
            SqlCommand comm = new SqlCommand(
                " INSERT INTO TBUsers(Name, Family) VALUES(@NamePar, @FamPar);" +
                " SELECT SCOPE_IDENTITY() as UID;", con);

            comm.Parameters.Add(new SqlParameter("NamePar", u.Name));

            SqlParameter parF = new SqlParameter("FamPar", u.Family);
            comm.Parameters.Add(parF);

            con.Open();
            int res = -1;
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                res = int.Parse(reader["UID"].ToString());
            }
            con.Close();

            if (res != -1)
            {
                u.Id = res;
                return u;
            }
            else
            {
                return null;
            }
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
                userList.Add(new User()
                {
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