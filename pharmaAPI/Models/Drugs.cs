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
    public class Drugs
    {
        public int drugID;
        public string name;
        public int dangerous;

        public Drugs(int cdrugID, string cname, int cdangerous)
        {
            drugID = cdrugID;
            name = cname;
            dangerous = cdangerous;
        }

        public Drugs(MySqlDataReader crdr)
        {
            drugID = (int) crdr[0];
            name = (string) crdr[1];
            if ( (bool) crdr[2])
            {
                dangerous = 1;
            } else {
                dangerous = 0;
            }
        }
    }
}