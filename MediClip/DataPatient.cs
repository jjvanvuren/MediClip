using System;
using System.Collections.Generic;
using System.Text;

namespace MediClip
{
    class DataPatient
    {
        public List<Patient> Patients = new List<Patient>()
        {
            new Patient()
            {
                PatientID = 001,
                WardID = 001,
                FirstName = "Bob",
                LastName = "Ross",
                PatientName = "Bob Ross",
                PatientPicture = "bobross.jpeg"
            }
        };
    }
}
