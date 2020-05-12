using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Warships_DAL.Models;
using Warships_DAL.Repositories;
using Warships_DAL.Utils;

namespace Warships_DAL.Services
{
    public class UserService : IRepository<User>, IUser
    {
        public void Create(User u)
        {
            using (SqlCommand cmd = new SqlCommand("AddUser", Handler.ConnecDB))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("mail", u.mail);
                cmd.Parameters.AddWithValue("login", u.login);
                cmd.Parameters.AddWithValue("password", u.password);
                cmd.Parameters.AddWithValue("birthDate", u.birthDate);
                cmd.Parameters.AddWithValue("country", u.country);

                Handler.ConnecDB.Open();
                cmd.ExecuteNonQuery();
                Handler.ConnecDB.Close();

            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DeleteUser", Handler.ConnecDB))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", id);

                Handler.ConnecDB.Open();
                cmd.ExecuteNonQuery();
                Handler.ConnecDB.Close();

            }
        }

        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            Handler.ConnecDB.Open();

            //creation de la cmd
            using (SqlCommand cmd = new SqlCommand("GetUSers", Handler.ConnecDB))
            {

                //execution
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //creation de la liste en bouclant sur le DR
                    while (dr.Read())
                    {

                        list.Add(new User
                        {
                            id = (int)dr["id"],
                            mail = dr["mail"].ToString(),
                            login = dr["login"].ToString(),
                            birthDate = (DateTime)dr["birthDate"],
                            country = dr["country"].ToString(),
                            isActive = (bool)dr["isActive"],
                            isDelete = (bool)dr["isDelete"],
                            isAdmin = (bool)dr["isAdmin"]
                        }); ;
                    }
                }

                Handler.ConnecDB.Close();
                return list;

            }
        }
        public User GetOne(int id)
        {
            using (SqlCommand cmd = new SqlCommand("GetOne", Handler.ConnecDB))
            {
                cmd.Parameters.AddWithValue("id", id);
                //execution
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    return new User
                    {
                        id = (int)dr["id"],
                        mail = dr["mail"].ToString(),
                        login = dr["login"].ToString(),
                        birthDate = (DateTime)dr["birthDate"],
                        country = dr["country"].ToString(),
                        isActive = (bool)dr["isActive"],
                        isDelete = (bool)dr["isDelete"],
                        isAdmin = (bool)dr["isAdmin"]
                    }; ;
                }
            }
        }

        public void Update(User u)
        {
            using (SqlCommand cmd = new SqlCommand("UpdateUser", Handler.ConnecDB))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", u.id);
                cmd.Parameters.AddWithValue("mail", u.mail);
                cmd.Parameters.AddWithValue("login", u.login);
                cmd.Parameters.AddWithValue("password", u.password);
                cmd.Parameters.AddWithValue("birthDate", u.birthDate);
                cmd.Parameters.AddWithValue("country", u.country);

                Handler.ConnecDB.Open();
                cmd.ExecuteNonQuery();
                Handler.ConnecDB.Close();

            }
        }

        public User Login(string login, string password)
        {
            using (SqlCommand cmd = new SqlCommand("LoginUser", Handler.ConnecDB))
            {

                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("login", login);                
                cmd.Parameters.AddWithValue("password", password);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    return new User
                    {
                        id = (int)dr["id"],
                        mail = dr["mail"].ToString(),
                        login = dr["login"].ToString(),
                        birthDate = (DateTime)dr["birthDate"],
                        country = dr["country"].ToString(),
                        isActive = (bool)dr["isActive"],
                        isDelete = (bool)dr["isDelete"],
                        isAdmin = (bool)dr["isAdmin"]
                    }; ;
                }

            }

        }

        public void ChangeAccountSpec(User u)
        {

          
                using (SqlCommand cmd = new SqlCommand("UpdateAccountSpec", Handler.ConnecDB))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", u.id);
                    cmd.Parameters.AddWithValue("isActive", u.isActive);
                    cmd.Parameters.AddWithValue("isAdmin", u.isAdmin);
                    cmd.Parameters.AddWithValue("isDelete", u.isDelete);
              
                    Handler.ConnecDB.Open();
                    cmd.ExecuteNonQuery();
                    Handler.ConnecDB.Close();

                }
            


        }
    }
}
