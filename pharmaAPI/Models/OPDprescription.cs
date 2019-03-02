using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pharmaAPI.Models
{
    public class OPDprescription
    {
        public int prescriptionID;
        public int toFill;
        public int filledAndDispatched;
        public string dateDispatched;
        public string timeDispatched;
        public int indoorEmergency;


        public OPDprescription(int cprescriptionID, int ctoFill, int cfilledAndDispatched, string cdateDispatched, string ctimeDispatched, int cindoorEmergency)
        {
            prescriptionID = cprescriptionID;
            toFill = ctoFill;
            filledAndDispatched = cfilledAndDispatched;
            dateDispatched = cdateDispatched;
            timeDispatched = ctimeDispatched;
            indoorEmergency = cindoorEmergency;
    }

        public OPDprescription(MySqlDataReader crdr)
        {
            prescriptionID = (int)crdr[0];
            toFill = (int)crdr[1];
            filledAndDispatched = (int)crdr[2];
            dateDispatched = (string)crdr[3];
            timeDispatched = (string)crdr[4];
            indoorEmergency = (int)crdr[5];
        }
    }
}
