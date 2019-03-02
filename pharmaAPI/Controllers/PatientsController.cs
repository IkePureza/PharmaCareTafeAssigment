using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using pharmaAPI;
using pharmaAPI.Models;

namespace pharmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        // Get all patients
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM patients;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                while (rdr.Read())
                {
                    temp.Add(new Patients(rdr));
                }

                conn.Close();
                return JsonConvert.SerializeObject(new message(200, temp));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonConvert.SerializeObject(new message(404, null));
            }
        }

        //Update patient
        [HttpPut("{patientID}/{firstName}/{lastName}/{wardID}/{roomID}/")]
        public void Put(int patientID, string firstName, string lastName, int wardID, int roomID)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "UPDATE patients SET firstName='"+ firstName +"', lastName='"+ lastName +"', wardID='"+ wardID +  "', roomID='"+ roomID +"' WHERE patientID='"+ patientID +"';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (System.Exception)
            {
                throw;
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

        // Delete Patient
        [HttpDelete("{patientID}")]
        public void Delete(int patientID)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "DELETE FROM patients  WHERE patientID='"+ patientID +"';";
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
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM patients WHERE patientID=" + id + ";";
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
        }
    }
}
