using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CoronaOperations
    {
        public List<Datum> GetTheRecords(string sqlQuery)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            connString.UserID = "sa";
            connString.Password = "daliborJanda12";
            connString.DataSource = "localhost";
            connString.IntegratedSecurity = false; // if true then windows authentication
            connString.InitialCatalog = "master";
            List<Datum> theReply = new List<Datum>();
            using (SqlConnection connDB = new SqlConnection(connString.ConnectionString))
            {
                try
                {
                    connDB.Open();
                   
                    var sqlCmd = new SqlCommand(sqlQuery, connDB);
                    var reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int index = 0;
                        Datum newData = new Datum();
                        newData.countrycode = reader.GetString(index++);
                        newData.date = reader.GetString(index++).ToString();
                        newData.cases = reader.GetInt32(index++).ToString();
                        newData.deaths = reader.GetInt32(index++).ToString();
                        newData.recovered = reader.GetInt32(index++).ToString();
                        theReply.Add(newData);
                    }
                    reader.Close();
                    connDB.Close();
                }
                catch (SqlException ex)
                {
                    
                    return (theReply);
                }

            }
            return (theReply);
        }
        
        public bool PostTheRecords(Datum datum){  
            
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            connString.UserID = "sa";
            connString.Password = "daliborJanda12";
            connString.DataSource = "localhost";
            connString.IntegratedSecurity = false; // if true then windows authentication
            connString.InitialCatalog = "master";
            List<Datum> theReply = new List<Datum>();
            using (SqlConnection connDB = new SqlConnection(connString.ConnectionString))
            {
                try
                    {
                        connDB.Open();
                        string pQuery = "INSERT INTO theStats (countrycode, date, cases, deaths, recovered)";
                        pQuery += " VALUES (@countrycode, @date, @cases, @deaths, @recovered)";
                        SqlCommand myCommand = new SqlCommand(pQuery, connDB);
                        myCommand.Parameters.AddWithValue("@countrycode", datum.countrycode);
                        myCommand.Parameters.AddWithValue("@date", datum.date);
                        myCommand.Parameters.AddWithValue("@cases", datum.cases);
                        myCommand.Parameters.AddWithValue("@deaths", datum.deaths);
                        myCommand.Parameters.AddWithValue("@recovered", datum.recovered);
                        myCommand.ExecuteNonQuery();
                        connDB.Close();
                    }
                    catch (SqlException ex)
                    {
                        return false;
                    }
                }
                
                return true;
            }
        }
        
    }
    
    


