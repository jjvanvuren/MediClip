using System;
using System.Collections.Generic;
using System.Text;

namespace MediClip.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public int WardID { get; set; }
        public String AssignDateFrom { get; set; }
        public String AssignDateTo { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Dob { get; set; }
        public String Sex { get; set; }
        public String Dosage { get; set; }
        public String Picture { get; set; }
    }
}
