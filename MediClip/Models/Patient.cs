using System;
using System.Collections.Generic;
using System.Text;

namespace MediClip.Models
{
    // Purpose: Used to POST and GET Patient data from the SQL server
    public class Patient
    {
        // Private attributes
        private String myFirstName;
        private String myLastName;
        private String myPicture;

        // Public properties
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
        public String Picture
        {
            get
            {
                return myPicture;
            }

            set
            {
                // If there is no picture associated with the patient use the default image
                if (!value.Equals(""))
                {
                    myPicture = value;
                }
                else
                {
                    myPicture = "blankPersonMale.png";
                }
                
            }
        }

        // Concatination of the Patient FirstName and LastName for display purposes
        public String FullName
        {
            get
            {
                return myFirstName + " " + myLastName;
            }

        }

        
    }
}
