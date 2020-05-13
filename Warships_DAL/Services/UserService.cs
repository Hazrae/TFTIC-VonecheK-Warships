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

    //IRepo ; basic CRUD
    //IUser : specific user DB management
    public class UserService : IRepository<User>, IUser
    {

        internal UserService(){      }
        public void Create(User u)
        {
            //Add user in DB

            using (SqlCommand cmd = new SqlCommand("AddUser", Handler.ConnecDB))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("mail", u.Mail);
                cmd.Parameters.AddWithValue("login", u.Login);
                cmd.Parameters.AddWithValue("password", u.Password);
                cmd.Parameters.AddWithValue("birthDate", u.BirthDate);
                cmd.Parameters.AddWithValue("country", u.Country);

                Handler.ConnecDB.Open();
                
                cmd.ExecuteNonQuery();
                Handler.ConnecDB.Close();

            }
        }

        public void Delete(int id)
        {

            //Soft delete of user in DB

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
            
            // User list from DB

            List<User> list = new List<User>();
            Handler.ConnecDB.Open();
           
            using (SqlCommand cmd = new SqlCommand("GetUSers", Handler.ConnecDB))
            {                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {                   
                    while (dr.Read())
                    {

                        list.Add(new User
                        {
                            Id = (int)dr["id"],
                            Mail = dr["mail"].ToString(),
                            Login = dr["login"].ToString(),
                            BirthDate = (DateTime)dr["birthDate"],
                            Country = dr["country"].ToString(),
                            isActive = (bool)dr["isActive"],
                            IsDelete = (bool)dr["isDelete"],
                            IsAdmin = (bool)dr["isAdmin"]
                        }); ;
                    }
                }

                Handler.ConnecDB.Close();
                return list;

            }
        }
        public User GetOne(int id)
        {

            // Get a specific user through his ID

            using (SqlCommand cmd = new SqlCommand("GetOne", Handler.ConnecDB))
            {
                cmd.Parameters.AddWithValue("id", id);
                //execution
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    return new User
                    {
                        Id = (int)dr["id"],
                        Mail = dr["mail"].ToString(),
                        Login = dr["login"].ToString(),
                        BirthDate = (DateTime)dr["birthDate"],
                        Country = dr["country"].ToString(),
                        isActive = (bool)dr["isActive"],
                        IsDelete = (bool)dr["isDelete"],
                        IsAdmin = (bool)dr["isAdmin"]
                    }; ;
                }
            }
        }

        public void Update(User u)
        {
            //Update User details

            using (SqlCommand cmd = new SqlCommand("UpdateUser", Handler.ConnecDB))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", u.Id);
                cmd.Parameters.AddWithValue("mail", u.Mail);
                cmd.Parameters.AddWithValue("login", u.Login);
                cmd.Parameters.AddWithValue("password", u.Password);
                cmd.Parameters.AddWithValue("birthDate", u.BirthDate);
                cmd.Parameters.AddWithValue("country", u.Country);

                Handler.ConnecDB.Open();
                cmd.ExecuteNonQuery();
                Handler.ConnecDB.Close();

            }
        }

        public User Login(string login, string password)
        {

            //Log an user in through his credentials

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
                        Id = (int)dr["id"],
                        Mail = dr["mail"].ToString(),
                        Login = dr["login"].ToString(),
                        BirthDate = (DateTime)dr["birthDate"],
                        Country = dr["country"].ToString(),
                        isActive = (bool)dr["isActive"],
                        IsDelete = (bool)dr["isDelete"],
                        IsAdmin = (bool)dr["isAdmin"]
                    }; ;
                }

            }

        }
         
        public void ChangeAccountSpec(User u)
        {     

            //Change the Spec bits of an user

            using (SqlCommand cmd = new SqlCommand("UpdateAccountSpec", Handler.ConnecDB))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", u.Id);
                cmd.Parameters.AddWithValue("isActive", u.isActive);
                cmd.Parameters.AddWithValue("isAdmin", u.IsAdmin);
                cmd.Parameters.AddWithValue("isDelete", u.IsDelete);

                Handler.ConnecDB.Open();
                cmd.ExecuteNonQuery();
                Handler.ConnecDB.Close();

            }
        }

        public bool CheckMail(string mail)
        {
            using (SqlCommand cmd = new SqlCommand("CheckMail", Handler.ConnecDB))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("mail", mail);               

                Handler.ConnecDB.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return true;
                    else
                        return false;                    
                }
                

            }
        }

        public bool CheckLogin(string login)
        {
            using (SqlCommand cmd = new SqlCommand("CheckLogin", Handler.ConnecDB))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("login", login);

                Handler.ConnecDB.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return true;
                    else
                        return false;
                }


            }
        }
    }
}
