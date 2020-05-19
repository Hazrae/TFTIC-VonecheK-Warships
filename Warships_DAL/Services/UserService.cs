using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Warships_DAL.Models;
using Warships_DAL.Repositories;
using Warships_DAL.Utils;
using System.Configuration;
using System.Reflection;

namespace Warships_DAL.Services
{

    //IRepo ; basic CRUD
    //IUser : specific user DB management
    public class UserService : Service, IRepository<User>, IUser
    {
        public UserService() : base(){ }    
        
        public void Create(User u)
        {
            using (SqlConnection connec = new SqlConnection(stringConnec))
            {
                //Add user in DB            
                using (SqlCommand cmd = new SqlCommand("AddUser", connec))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("mail", u.Mail);
                    cmd.Parameters.AddWithValue("login", u.Login);
                    cmd.Parameters.AddWithValue("password", u.Password);
                    cmd.Parameters.AddWithValue("birthDate", u.BirthDate);

                    connec.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
            
        
        }

        public void Delete(int id)
        {
            //Soft delete of user in DB
            using (SqlConnection connec = new SqlConnection(stringConnec))
            {

                using (SqlCommand cmd = new SqlCommand("DeleteUser", connec))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", id);

                    connec.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAll()
        {

            // User list from DB

            List<User> list = new List<User>();

            using (SqlConnection connec = new SqlConnection(stringConnec))
            {

                using (SqlCommand cmd = new SqlCommand("GetUSers", connec))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connec.Open();
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
                                isActive = (bool)dr["isActive"],
                                IsDelete = (bool)dr["isDelete"],
                                IsAdmin = (bool)dr["isAdmin"]
                            }); ;
                        }
                    }
                }

                return list;
            }
        }
        public User GetOne(int id)
        {
            // Get a specific user through his ID
            using (SqlConnection connec = new SqlConnection(stringConnec))
            {

                using (SqlCommand cmd = new SqlCommand("GetOne", connec))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    //execution
                    connec.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        return new User
                        {
                            Id = (int)dr["id"],
                            Mail = dr["mail"].ToString(),
                            Login = dr["login"].ToString(),
                            BirthDate = (DateTime)dr["birthDate"],                           
                            isActive = (bool)dr["isActive"],
                            IsDelete = (bool)dr["isDelete"],
                            IsAdmin = (bool)dr["isAdmin"]
                        }; ;
                    }
                }
            }
        }

        public void Update(int id,User u)
        {
            //Update User details
            using (SqlConnection connec = new SqlConnection(stringConnec))
            {

                using (SqlCommand cmd = new SqlCommand("UpdateUser", connec))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("mail", u.Mail);
                    cmd.Parameters.AddWithValue("login", u.Login);                    
                    cmd.Parameters.AddWithValue("birthDate", u.BirthDate);

                    connec.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public User Login(string login, string password)
        {
            //Log an user in through his credentials
            using (SqlConnection connec = new SqlConnection(stringConnec))
            {

                using (SqlCommand cmd = new SqlCommand("LoginUser", connec))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("login", login);
                    cmd.Parameters.AddWithValue("password", password);

                    connec.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {                     

                        if (dr.Read())
                        {
                            return new User
                            {
                                Id = (int)dr["id"],
                                Mail = dr["mail"].ToString(),
                                Login = dr["login"].ToString(),
                                BirthDate = (DateTime)dr["birthDate"],
                                isActive = (bool)dr["isActive"],
                                IsDelete = (bool)dr["isDelete"],
                                IsAdmin = (bool)dr["isAdmin"]
                            }; 
                        }
                        else
                            return new User();
                    }

                }
            }

        }

        public void ChangeAccountSpec(User u)
        {

            //Change the Spec bits of an user
            using (SqlConnection connec = new SqlConnection(stringConnec))
            {

                using (SqlCommand cmd = new SqlCommand("UpdateAccountSpec", connec))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", u.Id);
                    cmd.Parameters.AddWithValue("isActive", u.isActive);
                    cmd.Parameters.AddWithValue("isAdmin", u.IsAdmin);
                    cmd.Parameters.AddWithValue("isDelete", u.IsDelete);

                    connec.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int UpdatePassword(int id, UserPassword u)
        {     
            using (SqlConnection connec = new SqlConnection(stringConnec))
            {

                using (SqlCommand cmd = new SqlCommand("UpdatePassword", connec))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("oldpassword", u.OldPassword);                  
                    cmd.Parameters.AddWithValue("password", u.Password);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    connec.Open();
                    cmd.ExecuteNonQuery();
                    return (int)returnValue.Value;
                }
            }
        }


    }
}
