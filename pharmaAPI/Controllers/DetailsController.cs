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
    public class DetailsController : ControllerBase
    {
        // Update details by detailsID
        [HttpPut("{id}/{form}/{dose}/{lTime}/{times}")]
        public void Put(int id, string form, string dose, string lTime, int times)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "UPDATE details SET drugForm='"+ form +"', drugDose='"+ dose +"', lastTime='"+ lTime +"', timesPerDay="+ times +" WHERE detailID="+ id +";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Create new detail and associate it with prescription
        [HttpPut("{prescriptionID}/{drugID}/{drugForm}/{drugDose}/{firstTime}/{lastTime}/{timesPerDay}")]
        public void Put(int prescriptionID, int drugID,string drugForm,string drugDose,string firstTime,string lastTime,string timesPerDay)
        {
            try
            {
                // Create new details record
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "INSERT INTO details (drugID, drugForm, drugDose, firstTime, lastTime, timesPerDay) VALUES  ("+ drugID +", '"+ drugForm +"', '"+ drugDose +"', '"+ firstTime +"', '"+ lastTime +"', "+ timesPerDay +");";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // Fetch last ID generated

                sql = "SELECT LAST_INSERT_ID()";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();

                int detailID = Convert.ToInt32(rdr[0]);

                rdr.Close();

                // Associate detail with prescription in linking table\

                sql = "INSERT INTO prescriptionsToDetails (prescriptionID, detailID) VALUES ("+ prescriptionID +", "+ detailID +")";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Delete details by detailsID
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                MySqlConnection conn = dbutils.OpenDB();

                string sql = "DELETE FROM prescriptionsToDetails WHERE detailID="+ id +";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();

                sql = "DELETE FROM details WHERE detailID="+ id +";";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
