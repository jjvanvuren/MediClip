using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

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

        public PatientViewModel()
        {
            Patients = new ObservableCollection<Patient>();

            DataPatient _context = new DataPatient();

            foreach (var patient in _context.Patients)
            {
                Patients.Add(patient);
            }
        }
    }
}