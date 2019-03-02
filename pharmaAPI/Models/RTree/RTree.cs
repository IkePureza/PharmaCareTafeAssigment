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

namespace pharmaAPI.Models
{
    public class RTree
    {
        public static RNode getTree(int patientID) {
            // Top node

            MySqlConnection conn = dbutils.OpenDB();

            string sql = "SELECT * FROM patients WHERE patientID=" + patientID + ";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            rdr.Read();

            RNode topNode = new RNode(new List<RNode>(), new Patients(rdr));

            rdr.Close();

            // List of prescriptions

            sql = "SELECT * FROM prescriptions WHERE patientID=" + patientID + ";";
            cmd = new MySqlCommand(sql, conn);

            rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                topNode.forward.Add(new RNode(new List<RNode>(), new Prescriptions(rdr)));
            }

            rdr.Close();

            // List of details for each prescription

            for (int i = 0; i < topNode.forward.Count; i++)
            {
                Prescriptions pre = (Prescriptions) topNode.forward[i].content;

                sql = "SELECT details.*, drugs.name FROM prescriptionsToDetails JOIN details ON prescriptionsToDetails.detailID = details.detailID JOIN drugs ON details.drugID = drugs.drugID WHERE prescriptionID="+ pre.prescriptionID +";";
                cmd = new MySqlCommand(sql, conn);

                rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    topNode.forward[i].forward.Add(new RNode(new List<RNode>(), new Details(rdr)));
                }

                rdr.Close();
            }

            return topNode;
        }

        public static List<object> getDetails() {
            List<object> details = new List<object>();

            MySqlConnection conn = dbutils.OpenDB();

            string sql = "SELECT prescriptionID FROM prescriptions WHERE status=1;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            List<int> temp = new List<int>();

            while (rdr.Read())
            {
                temp.Add( (int) rdr[0]);
            }

            rdr.Close();

            // Start foreach

            foreach (var prescriptionID in temp)
            {

                sql = "SELECT details.*, drugs.name FROM prescriptionsToDetails JOIN details ON prescriptionsToDetails.detailID = details.detailID JOIN drugs ON details.drugID = drugs.drugID WHERE prescriptionID="+ prescriptionID +";";
            
                cmd = new MySqlCommand(sql, conn);

                rdr = cmd.ExecuteReader();

                List<Details> det = new List<Details>();

                while (rdr.Read())
                {
                    details.Add(new Details(rdr));
                }

                rdr.Close();
            }

            // End foreach

            conn.Close();

            return details;
        }
    }
}