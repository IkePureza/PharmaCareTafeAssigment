using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using pharmaAPI;
using pharmaAPI.Models;

namespace pharmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        // GET api/test/5
        [HttpGet("{username}/{password}")]
        public ActionResult<string> Get(string username, string password)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM staff WHERE username='" + username + "' AND password='" + password + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();
                
                List<Object> temp = new List<Object>();

                
                rdr.Read();

                string msg = JsonConvert.SerializeObject(new message(200, new Staff(rdr)));

                conn.Close();

                return msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //conn.Close();
                return JsonConvert.SerializeObject(new message(404, null));
            }
        }

        // Get single staff
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM staff WHERE staffID=" + id + ";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                rdr.Read();

                string msg = JsonConvert.SerializeObject(new message(200, new Staff(rdr)));

                rdr.Close();

                conn.Close();
                
                return msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonConvert.SerializeObject(new message(404, null));
            }
        }

        
        // Create new staff
        [HttpPut("{sfirstName}/{slastName}/{susername}/{spassword}/{sjobTitle}/{swardID}/{sroomId}")]
        public void put(string sfirstName, string slastName, string susername, string spassword, int sjobTitle, int swardID, int sroomID)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();
                string countSQL = "SELECT COUNT(*) FROM pharmacare.staff;";
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
                string sql = "INSERT INTO pharmacare.staff (staffID, firstName, lastName, username, password, jobTitle, wardID, roomID) VALUES ("+ count +", '"+ sfirstName +"', '"+ slastName +"', '"+ susername +"',  '"+ spassword +"','"+ sjobTitle +"', "+ swardID +", "+ sroomID +");";
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

        //Update staff
        [HttpPut("{staffID}/{sfirstName}/{slastName}/{susername}/{spassword}/{sjobTitle}/{swardID}/{sroomID}/")]
        public void Put(int staffID, string sfirstName, string slastName, string susername, string spassword, int sjobTitle, int swardID, int sroomID)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "UPDATE staff SET firstName='"+ sfirstName +"', lastName='"+ slastName +"', username='"+ susername +"', password='"+ spassword +"', jobTitle='"+ sjobTitle +"', wardID='"+ swardID +  "', roomID='"+ sroomID +"' WHERE staffID='"+ staffID +"';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        
        // Delete Staff
        [HttpDelete("{staffID}")]
        public void Delete(int staffID)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "DELETE FROM staff WHERE staffID='"+ staffID +"';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


                // Get single patient
        /* [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM staff WHERE patientID=" + id + ";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                rdr.Read();

                string msg = JsonConvert.SerializeObject(new message(200, new Patients(rdr)));

                rdr.Close();

                conn.Close();
                
                return msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonConvert.SerializeObject(new message(404, null));
            }
        }*/
    }
}
        