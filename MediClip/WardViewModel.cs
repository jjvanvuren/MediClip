using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace MediClip
{
    class WardViewModel
    {
        private ObservableCollection<Ward> wards;
        public ObservableCollection<Ward> Wards
        {
            get { return wards; }
            set
            {
                wards = value;
            }
        }

        public WardViewModel()
        {
            Wards = new ObservableCollection<Ward>();

            DataWard _context = new DataWard();

            foreach (var ward in _context.Wards)
            {
                Wards.Add(ward);
            }
        }
    }
}
