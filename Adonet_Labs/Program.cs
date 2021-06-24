using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adonet_Labs
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();

            // ConnectedInsert(connString);
            // ConnectedSelect(connString);
            //  ConnectedUpdate(connString);
             // ConnectedDelete(connString);
            ConnectedSP(connString);
            ConnectScalar(connString);
            Console.ReadLine();
        }

        private static void ConnectScalar(string connString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "SELECT count(Id) FROM Student ";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var count = command.ExecuteScalar();
                    Console.WriteLine($"total student count :{count}");
                  


                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex) //
            {
                Console.WriteLine(ex.Message);


            }

        }

        private static void ConnectedSP(string connString)
        {
            Console.WriteLine("Enter student id");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        string query = $"usp_StudentDetail";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        //1 
                        command.Parameters.AddWithValue("@ID", id);
                        //2
                        //SqlParameter param = new SqlParameter("@ID", id);
                        //command.Parameters.Add(param);
                        connection.Open();
                        SqlDataReader reader= command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Console.WriteLine($"id:{reader["Id"]}\t" +
                           $"Name:{reader["Name"]}\t" +
                           $"Age: {reader["Age"]}\t" +
                           $"Address:{reader["Address"]}");



                        }
                        else
                        {
                            Console.WriteLine("student not found");
                        }
                       

                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex) //
                {
                    Console.WriteLine(ex.Message);


                }

            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }


    

        private static void ConnectedDelete(string connString)
        {
            Console.WriteLine("Enter student id");
            string input = Console.ReadLine();
            if(int.TryParse(input,out int id))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        string query = $"DELETE FROM StudenT where id={id}";
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("student deleted successfully");
                        }
                        else
                        {
                            Console.WriteLine("student not found !!");
                        }

                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex) //
                {
                    Console.WriteLine(ex.Message);


                }

            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }

        private static void ConnectedUpdate(string connString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "update Student set Address='Goa' where Id=1";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("student updated successfully");
                    }
                    else
                    {
                        Console.WriteLine("update failed !!");
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex) //
            {
                Console.WriteLine(ex.Message);


            }

        }

        private static void ConnectedSelect(string connString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "SELECT * FROM Student ";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader= command.ExecuteReader();
                    while(reader.Read())
                    {
                        Console.WriteLine($"id:{reader["Id"]}\t" +
                            $"Name:{reader["Name"]}\t" +
                            $"Age: {reader["Age"]}\t"+
                            $"Address:{reader["Address"]}");

                    }
                   

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex) //
            {
                Console.WriteLine(ex.Message);


            }

        }

        static void ConnectedInsert(string connString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "INSERT INTO Student (Name,Age,Address) VALUES('Mayur',23,'ABD')";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("student added successfully");
                    }
                    else
                    {
                        Console.WriteLine("insert failed !!");
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex) //
            {
                Console.WriteLine(ex.Message);

               
            }
        }
    }
}
