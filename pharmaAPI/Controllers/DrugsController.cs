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
    public class DrugsController : ControllerBase
    {
        // Get all drugs
        [HttpGet]
        public ActionResult<string> GetDetails()
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT * FROM drugs";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Object> temp = new List<Object>();

                while (rdr.Read())
                {
                    temp.Add(new Drugs(rdr));
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
    }
}
