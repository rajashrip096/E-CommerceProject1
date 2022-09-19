using E_CommerceProject1.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceProject1.DAL
{
    public class UserDAL
    {
        
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public UserDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration["ConnectionStrings:defaultConnection"];
            con = new SqlConnection(constr);
        }
        public int UserRegister(User user)
        {
            string qry = "insert into UserData values(@name,@email,@contact,@pass)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@contact", user.Contact);
            cmd.Parameters.AddWithValue("@pass", user.Password);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public User UserLogin(User u)
        {
            User user = new User();
            string qry = "select * from UserData where Email=@email";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("email", u.Email);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.Id = Convert.ToInt32(dr["UserId"]);
                    user.Name = dr["Name"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Contact = Convert.ToInt32(dr["Contact"]);
                    user.Password = dr["Password"].ToString();
                }
                con.Close();
                return user;

            }
            else
            {
                return user;
            }
        }
    }
}
