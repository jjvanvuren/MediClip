using System;
using System.Collections.Generic;
using System.Text;
using MediClip.Objects;

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
            },
            new Patient()
            {
                PatientID = 002,
                WardID = 001,
                FirstName = "John",
                LastName = "Brown",
                PatientName = "John Brown",
                PatientPicture = "blankPersonMale.png"
            },
            new Patient()
            {
                PatientID = 003,
                WardID = 002,
                FirstName = "Jake",
                LastName = "Snake",
                PatientName = "Jake Snake",
                PatientPicture = "blankPersonMale.png"
            }
        };
    }
}
