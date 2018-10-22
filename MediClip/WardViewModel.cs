using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using MediClip.Models;
using System.Net.Http;
using System.Threading.Tasks;
using MediClip.Client;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
            wards = new ObservableCollection<Ward>();

            // Run the task in the background (as it may take a long time)
            Task.Run(async () =>
            {
                // Create a new client
                MediClipClient client = new MediClipClient();


                // Perform the search operation
                List<Ward> result = await client.ListWard();

                foreach (Ward wWard in result)
                {
                    wards.Add(wWard);
                }

            });


            //Wards = new ObservableCollection<Ward>();

            //DataWard dWardList = new DataWard();

            //foreach (var ward in _context.Wards)
            //{
            //    Wards.Add(ward);
            //}
        }
    }
}
