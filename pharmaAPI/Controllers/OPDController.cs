using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using pharmaAPI.Models;
using pharmaAPI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pharmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OPDController : Controller
    {
        // Get all 
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {              
                MySqlConnection con = dbutils.OpenDB();

                string sql = "SELECT * FROM opdPrescription;";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                while (rdr.Read())
                {
                    temp.Add(new OPDprescription(rdr));
                }
                
                return JsonConvert.SerializeObject(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //conn.Close();
                return null;
            }
        }
        [HttpGet("{username}/{password}")]
        public ActionResult<string> Get(string username, string password)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT patientID FROM patients WHERE firstName='" + username + "' AND lastName='" + password + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    rdr.Read();
                    string temp = JsonConvert.SerializeObject(int.Parse(rdr[0].ToString()));

                    MySqlConnection con = dbutils.OpenDB();

                    string sql2 = "SELECT prescriptionID FROM prescriptions WHERE patientID='" + temp + "';";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, con);

                    MySqlDataReader rdr2 = cmd2.ExecuteReader();

                    if (rdr2.Read())
                    {
                        rdr2.Read();
                        string id = JsonConvert.SerializeObject(int.Parse(rdr2[0].ToString())) ;

                        MySqlConnection conn3 = dbutils.OpenDB();

                        string sql3 = "SELECT * FROM opdPrescription WHERE prescriptionID=" + id + ";";
                        MySqlCommand cmd3 = new MySqlCommand(sql3, conn3);

                        MySqlDataReader rdr3 = cmd3.ExecuteReader();

                        if (rdr3.Read())
                        {
                            rdr3.Read();
                            
                            return JsonConvert.SerializeObject(new message(200, new OPDprescription(rdr3)));
                        } 
                        else {
                            return "hi";
                        } 
                    }
                    else
                    {
                        con.Close();
                        return temp;
                    }
                }
                else
                {
                    conn.Close();
                    return "2";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //conn.Close();
                return "3";
            }
        }

        // Get single patient
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM opdPrescription WHERE prescriptionID=" + id + ";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                rdr.Read();
                
                return JsonConvert.SerializeObject(new message(200, new OPDprescription(rdr)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //conn.Close();
                return JsonConvert.SerializeObject(new message(404, null));
            }
        }

        // Create new patient
        [HttpPut("{firstName}/{lastName}/{wardID}/{roomId}")]
        public void put(string firstName, string lastName, int wardID, int roomId)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();
                string countSQL = "SELECT COUNT(*) FROM pharmacare.patients;";
                MySqlCommand cmd = new MySqlCommand(countSQL, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                string temp = "0";

                while (rdr.Read())
                {
                    temp = JsonConvert.SerializeObject(int.Parse(rdr[0].ToString()));
                }

                int count = Convert.ToInt32(temp) + 1;
                conn.Close();
                MySqlConnection con = dbutils.OpenDB();
                string sql = "INSERT INTO pharmacare.patients (patientID, firstName, lastName, wardID, roomId) VALUES ("+ count +", '"+ firstName +"', '"+ lastName +"', "+ wardID +", "+ roomId +");";
                MySqlCommand cmd2 = new MySqlCommand(sql, con);
                cmd2.ExecuteReader();
                conn.Close();
                Console.Write(sql) ;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
