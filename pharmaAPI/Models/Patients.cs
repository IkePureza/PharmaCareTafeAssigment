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
    public class Patients
    {
        public int patientID;
        public string firstName;
        public string lastName;
        public int wardID;
        public int roomID;

        public Patients(int cpatientID, string cfirstName, string clastName, int cwardID, int croomID)
        {
            patientID = cpatientID;
            firstName = cfirstName;
            lastName = clastName;
            wardID = cwardID;
            roomID = croomID;
        }

        public Patients(MySqlDataReader crdr)
        {
            patientID = (int) crdr[0];
            firstName = (string) crdr[1];
            lastName = (string) crdr[2];
            wardID = (int) crdr[3];
            roomID = (int) crdr[4];
        }
    }
}