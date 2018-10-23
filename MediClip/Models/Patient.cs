using System;
using System.Collections.Generic;
using System.Text;

namespace MediClip.Models
{
    public class Patient
    {
        private String myFirstName;
        private String myLastName;

        public int PatientID { get; set; }
        public int WardID { get; set; }
        public String AssignDateFrom { get; set; }
        public String AssignDateTo { get; set; }
        public String FirstName
        {
            get
            {
                return myFirstName;
            }

            set
            {
                myFirstName = value;
            }
        }
        public String LastName
        {
            get
            {
                return myLastName;
            }

            set
            {
                myLastName = value;
            }
        }
        public String Dob { get; set; }
        public String Sex { get; set; }
        public String Dosage { get; set; }
        public String Picture { get; set; }

        
        public String FullName
        {

            get
            {
                return myFirstName + " " + myLastName;
            }

        }

        
    }
}
