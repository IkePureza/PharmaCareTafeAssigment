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
    public class Prescriptions
    {
        public int prescriptionID;
        public int patientID;
        public int staffID;
        public string prescriptionDate;
        public string prescriptionDetails;
        public int status;
        public int indoor;

        public Prescriptions(int cprescriptionID, int cpatientID, int cstaffID, string cprescriptionDate, string cprescriptionDetails, int cstatus, int cindoor)
        {
            prescriptionID = cprescriptionID;
            patientID = cpatientID;
            staffID = cstaffID;
            prescriptionDate = cprescriptionDate;
            prescriptionDetails = cprescriptionDetails;
            status = cstatus;
            indoor = cindoor;
        }

        public Prescriptions(MySqlDataReader crdr)
        {
            prescriptionID = (int) crdr[0];
            patientID = (int) crdr[1];
            staffID = (int) crdr[2];
            prescriptionDate = (string) crdr[3];
            prescriptionDetails = (string) crdr[4];
            status = (int) crdr[5];
            indoor = (int) crdr[6];
        }
        
    }
}