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
    public class Details
    {
        public int detailID;
        public int drugID;
        public string drugForm;
        public string drugDose;
        public string firstTime;
        public string lastTime;
        public int timesPerDay;
        public string name;

        public Details(int cdetailID, int cdrugID, string cdrugForm, string cdrugDose, string cfirstTime, string clastTime, int ctimesPerDay, string cname)
        {
            detailID = cdetailID;
            drugID = cdrugID;
            drugForm = cdrugForm;
            drugDose = cdrugDose;
            firstTime = cfirstTime;
            lastTime = clastTime;
            timesPerDay = ctimesPerDay;
            name = cname;
        }

        public Details(MySqlDataReader crdr)
        {
            detailID = (int) crdr[0];
            drugID = (int) crdr[1];
            drugForm = (string) crdr[2];
            drugDose = (string) crdr[3];
            firstTime = (string) crdr[4];
            lastTime = (string) crdr[5];
            timesPerDay = (int) crdr[6];
            name = (string) crdr[7];
        }
    }
}