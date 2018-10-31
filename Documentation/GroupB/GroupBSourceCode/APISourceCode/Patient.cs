using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediClipWebAPI.Models
{
    // Model for retrieving patients from the MediClip database
    public class Patient
    {
        public int PatientID { get; set; }
        public int WardID { get; set; }
        public string AssignDateFrom { get; set; }
        public string AssignDateTo { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string Dob { get; set; }
        public String Sex { get; set; }
        public String Dosage { get; set; }
        public String Picture { get; set; }
    }
}