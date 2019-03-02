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
    public class Distribution
    {
        public int wardID;

        public int roomID;

        public string firstName;

        public string lastName;
        
        public int prescriptionID;
        public int patientID;
        public int staffID;
        public string prescriptionDate;
        public string prescriptionDetails;
        public int indoor;

        public Distribution(int cwardID,int croomID,string cfisrtName,string clastName, int cprescriptionID, int cpatientID, int cstaffID, string cprescriptionDate, string cprescriptionDetails, int cstatus, int cindoor)
        {
            wardID = cwardID;
            roomID = croomID;
            firstName = cfisrtName;
            lastName = clastName;
            prescriptionID = cprescriptionID;
            patientID = croomID;
            staffID = cstaffID;
            prescriptionDate = cprescriptionDate;
            prescriptionDetails = cprescriptionDetails;
            indoor = cindoor;
        }

        public Distribution(MySqlDataReader crdr)
        {
            wardID = (int) crdr[0];
            roomID = (int) crdr[1];
            firstName = (string) crdr[2];
            lastName = (string) crdr[3];
            prescriptionID = (int) crdr[4];
            patientID = (int) crdr[5];
            staffID = (int) crdr[6];
            prescriptionDate = (string) crdr[7];
            prescriptionDetails = (string) crdr[8];
            indoor = (int) crdr[9];
        }
        
    }
}
