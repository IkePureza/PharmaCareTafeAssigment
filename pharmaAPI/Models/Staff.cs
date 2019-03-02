using MySql.Data.MySqlClient;

namespace pharmaAPI.Models
{
    public class Staff
    {
        public int staffID { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public int wardID { get; set; }
        public int roomID { get; set; }
        public int jobTitle { get; set; }
        public Staff(int staffID, string username, string password, string firstName, string lastName, int wardID, int roomID, int jobTitle)
        {
            this.staffID = staffID;
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.wardID = wardID;
            this.roomID = roomID;
            this.jobTitle = jobTitle;

        }

        public Staff(MySqlDataReader crdr)
        {
            this.staffID = (int)crdr[0];
            this.firstName = (string)crdr[1];
            this.lastName = (string)crdr[2];
            this.username = (string)crdr[3];
            this.password = (string)crdr[4];
            this.jobTitle = (int)crdr[5];
            this.wardID = (int)crdr[6];
            this.roomID = (int)crdr[7];

        }




    }
}