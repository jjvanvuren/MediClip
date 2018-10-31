using System;
namespace MediClip.Models
{
    // Purpose: Used to POST and GET Nurse data from the SQL server
    public class Nurse
    {
            public String NurseID { get; set; }
            public String UserName { get; set; }
            public String FirstName { get; set; }
            public String LastName { get; set; }
            public String Password { get; set; }

    }
}
