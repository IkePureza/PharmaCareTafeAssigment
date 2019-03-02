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

namespace pharmaAPI.Controllers
{
    public class LoginAuth
    {
        public int jobTitle;

        public LoginAuth(int cjobTitle)
        {
            this.jobTitle = cjobTitle;
        }
    }
    
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        // GET api/test/5
        [HttpGet("{username}/{password}")]
        public ActionResult<string> Get(string username, string password)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "SELECT jobTitle FROM staff WHERE username='" + username + "' AND password='" + password + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    rdr.Read();

                    string msg = JsonConvert.SerializeObject(new LoginAuth(int.Parse(rdr[0].ToString())));

                    rdr.Close();

                    conn.Close();

                    return msg;
                }
                else
                {
                    conn.Close();
                    return JsonConvert.SerializeObject(new LoginAuth(0));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonConvert.SerializeObject(new LoginAuth(5));
            }
        }
    }
}
