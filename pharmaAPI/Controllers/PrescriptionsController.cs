using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pharmaAPI.Models;
using MySql.Data.MySqlClient;
namespace pharmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        // Get prescription by patient ID
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return JsonConvert.SerializeObject(new message(200, RTree.getTree(id)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonConvert.SerializeObject(new message(404, null));
            }
        }

        [HttpGet("distribution")]
        public ActionResult<string> GetDist()
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT patients.wardID, patients.roomID, patients.firstName, patients.lastName, prescriptions.* FROM prescriptions INNER JOIN patients ON prescriptions.patientID=patients.patientID WHERE status=1;";
                
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                while (rdr.Read())
                {
                    temp.Add(new Distribution(rdr));
                }
                
                string msg = JsonConvert.SerializeObject(new message(200, temp));

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


        // Create new prescription
        [HttpPut("{patientID}/{staffID}/{prescriptionDate}/{prescriptionDetails}/{status}/{indoor}")]
        public void Put(int patientID, int staffID, string prescriptionDate, string prescriptionDetails, int status, int indoor)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "INSERT INTO prescriptions (patientID, staffID, prescriptionDate, prescriptionDetails, status, indoor) VALUES ("+ patientID +", "+ staffID +", '"+ prescriptionDate +"', '"+ prescriptionDetails +"', "+ status +", "+ indoor +");";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // Update prescription by prescriptionID
        [HttpPut("{id}/{details}/{status}/{indoor}")]
        public void Put(int id, string details, int status, int indoor)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "UPDATE prescriptions SET prescriptionDetails='"+ details +"', status='"+ status +"', indoor='"+ indoor +"' WHERE prescriptionID='"+ id +"';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // Get all details for active prescriptions
        [HttpGet("alldetails")]
        public ActionResult<string> GetDetails()
        {
            try
            {
                return JsonConvert.SerializeObject(new message(200, RTree.getDetails()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonConvert.SerializeObject(new message(404, null));
            }
        }


// Get all active Prescriptions
        [HttpGet("active")]
        public ActionResult<string> Get()
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM prescriptions WHERE status='1';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                while (rdr.Read())
                {
                    temp.Add(new Prescriptions(rdr));
                }
                
                string msg = JsonConvert.SerializeObject(new message(200, temp));

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

        [HttpGet("inactive")]
        public ActionResult<string> GetInactive()
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM prescriptions WHERE status='0';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                while (rdr.Read())
                {
                    temp.Add(new Prescriptions(rdr));
                }
                
                string msg = JsonConvert.SerializeObject(new message(200, temp));

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

        //Dispatch Prescription
        [HttpPut("{id}/dispatch")]
        public void Put(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "UPDATE prescriptions SET status='0' WHERE prescriptionID='"+ id +"';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        

        // Delete prescription by prescriptionID
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "DELETE FROM prescriptions WHERE prescriptionID='"+ id +"'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
