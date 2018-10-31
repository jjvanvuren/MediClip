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
            // The ObservableCollection that contains the wards returned by the client
            wards = new ObservableCollection<Ward>();

            // Runs the task in the background (as it may take a long time)
            Task.Run(async () =>
            {
                // Create a new client
                MediClipClient client = new MediClipClient();

                // Create a list of all the wards returned by the client
                List<Ward> result = await client.ListWard();

                // Add all the wards to the wards ObservableCollection
                foreach (Ward wWard in result)
                {
                    wards.Add(wWard);
                }
            });
        }
    }
}
