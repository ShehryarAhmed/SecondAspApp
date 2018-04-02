using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using shehryar.ModelClass;
using System.Net;

namespace shehryar.ReposoitryModel
{
    public class User
    {

        string message = string.Empty;

        string CS = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public List<users> GetClasses()
        {
            List<users> list_Class = new List<users>();
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGetAll_Users", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (dr.Read())
                            {
                                users class_get = new users();
                                class_get.userID = Convert.ToInt32(dr["userID"]);
                                if (dr["userID"] != DBNull.Value)
                                    class_get.userName = dr["userName"].ToString();
                                
                                list_Class.Add(class_get);
                               // int totalRecord = (int) cmd.ExecuteScalar.ToString();
                            }
                        }
                        catch (SqlException ex)
                        {
                            message = ex.Message;
                        }
                    }
                }

                return list_Class;
            }
        }

        public users GetUserByID(int id)
        {
            users list_Class = new users();
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGet_userByID", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassID", id);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (dr.Read())
                            {
                                users class_get = new users();
                                class_get.userID = Convert.ToInt32(dr["userID"]);
                                if (dr["userID"] != DBNull.Value && dr["userID"].Equals(0))
                                {
                                    class_get.userName = dr["userName"].ToString();
                                    list_Class = class_get;
                                }
                                else
                                {

                                }

                            }
                        }
                        catch (SqlException ex)
                        {
                            message = ex.Message;
                        }
                        con.Close();
                    }
                }
                return list_Class;
            }
        }

        public String Delete(int id)
        {
            String message = "";
            //users list_Class = new users();
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDeleteInto_class", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassID", id);

                    //SqlParameter output = new SqlParameter();
                    //output.ParameterName = "@Result";
                    //output.SqlDbType = SqlDbType.VarChar;
                    //output.Size = 500;
                    //output.Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add(output);

                    con.Open();

                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        //success
                        message = "Success";
                        return message;
                    }
                    else
                    {
                        message = "Failed";
                        return message;
                    }


                }
            }

            // return list_Class;
        }

        public String AddUsers([FromBody]int id, String name)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spInsertInto_class", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassID", id);
                    cmd.Parameters.AddWithValue("@ClassName", name);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        //success
                        message = "Add Successfully";
                        return message;
                    }
                    else
                    {
                        message = "Failed";
                        return message;
                    }
                }

            }
        }

        public String update(int id,String name) {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateInto_class", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassID", id);
                    cmd.Parameters.AddWithValue("@ClassName", name);
                    
                    con.Open();
                    int result = cmd.ExecuteNonQuery();

                    con.Close();

                    if (result > 0)
                    {
                        //success
                        message = "Update Successfully";
                        return message;
                    }
                    else
                    {
                        message = "Update Failed";
                        return message;
                    }
                }

            }
        }
    }
}
