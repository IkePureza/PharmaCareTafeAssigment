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
    public class DrDashController : ControllerBase
    {
        // Get prescriptions by patient ID
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM prescriptions WHERE patientID="+ id +";";
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
                //conn.Close();
                return JsonConvert.SerializeObject(new message(404, null));
            }
        }

        [HttpGet("single/{id}")]
        public ActionResult<string> GetPrescription(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM prescriptions WHERE prescriptionID="+ id +";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();

                string msg = JsonConvert.SerializeObject(new message(200, new Prescriptions(rdr)));

                rdr.Close();

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

        // Get list of details by prescription ID // working as intended

        [HttpGet("details/{id}")]
        public ActionResult<string> GetPrescriptions(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT details.*, drugs.name FROM prescriptionsToDetails JOIN details ON prescriptionsToDetails.detailID = details.detailID JOIN drugs ON details.drugID = drugs.drugID WHERE prescriptionID="+ id +";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Details> temp = new List<Details>();

                while(rdr.Read())
                {
                    temp.Add(new Details(rdr));
                }

                string msg = JsonConvert.SerializeObject(new message(200, temp));

                rdr.Close();

                conn.Close();

                return msg;
            }
            catch (System.Exception)
            {
                return JsonConvert.SerializeObject(new message(404, null));
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
