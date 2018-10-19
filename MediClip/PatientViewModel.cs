using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using MediClip.Objects;

namespace MediClip
{
    class PatientViewModel
    {
        private ObservableCollection<Patient> patients;
        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
            }
        }

        //List All Assigned Patients
        public PatientViewModel()
        {
            Patients = new ObservableCollection<Patient>();

            DataPatient _context = new DataPatient();

            foreach (var patient in _context.Patients)
            {
                Patients.Add(patient);
            }
        }

        //List Patients for a Specific Ward
        public PatientViewModel(int iWardID)
        {
            Patients = new ObservableCollection<Patient>();

            DataPatient _context = new DataPatient();

            foreach (var patient in _context.Patients)
            {
                if (iWardID == patient.WardID)
                Patients.Add(patient);
            }
        }
    }
}