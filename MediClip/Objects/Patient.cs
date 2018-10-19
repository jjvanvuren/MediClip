using System;
using System.Collections.Generic;
using System.Text;

namespace MediClip.Objects
{
    public class Patient
    {
        public int PatientID { get; set; }
        public String PatientPicture { get; set; }
        public String PatientName { get; set; }
        public int WardID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime Dob { get; set; }
        public String Sex { get; set; }
        public String Dosage { get; set; }
        public int BedNumber { get; set; }
    }
}
