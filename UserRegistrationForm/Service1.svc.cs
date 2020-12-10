using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UserRegistrationForm
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        string connectionString = "Data Source=REC39;Database=UserRegistrationWCF;Persist Security Info=True;User ID=sa;Password=Nani1234";
        SqlConnection connection;
        SqlCommand comm;

        public string Insert(InsertUser user)
        {
            string msg;

            string name = user.Name;
            string email = user.Email;

            try
            {
                connection = new SqlConnection(connectionString);
                string sql = "insert into dbo.Users values ('" + user.Name + "', '" + user.Email + "')";

                comm = new SqlCommand(sql, connection);
                connection.Open();
                // cmd.Parameters.AddWithValue("@Name", user.Name);
                // cmd.Parameters.AddWithValue("@Email", user.Email);

                comm.ExecuteNonQuery();
                connection.Close();
                msg = "Successfully Inserted";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                msg = ex.ToString();
            }
            return msg;
        }

        public gettestdata GetInfo()
        {
            gettestdata g = new gettestdata();
            connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "Select * From dbo.Users";
            comm = new SqlCommand(sql, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(comm);
            DataTable dataTable = new DataTable("myTab");
            using (dataAdapter)
            {
                dataAdapter.Fill(dataTable);
            }
            g.userTab = dataTable;
            return g;
        }

        public string Update(UpdateUser user)
        {
            string message = "";
            connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "Update dbo.Users set Name = @Name, Email = @Email where UserID = @UserID";
            comm = new SqlCommand(sql, connection);
            comm.Parameters.AddWithValue("@UserID", user.UID);
            comm.Parameters.AddWithValue("@Name", user.Name);
            comm.Parameters.AddWithValue("@Email", user.Email);

            int res = comm.ExecuteNonQuery();
            if (res == 1)
            {
                message = "Successfully Updated";
            }
            else
            {
                message = "Failed to Update";
            }
            return message;
        }

        public string Delete(DeleteUser user)
        {
            string message = "";
            connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "Delete dbo.Users where UserID = @UserID";
            comm = new SqlCommand(sql, connection);
            comm.Parameters.AddWithValue("@UserID", user.UID);

            int res = comm.ExecuteNonQuery();
            if (res == 1)
            {
                message = "Successfully Updated";
            }
            else
            {
                message = "Failed to Update";
            }
            return message;
        }
    }
}
