using System;
using System.Collections.Generic;
using System.Text;
using MediClip.Objects;

namespace MediClip
{
    class DataWard
    {
        public List<Ward> Wards = new List<Ward>()
        {
            new Ward()
            {
                WardID = 001,
                WardTitle = "Ward No: " + Convert.ToString(001),
                Location = "Somewhere",
                Description = "This is the description of ward 001."
            },
            new Ward()
            {
                WardID = 002,
                WardTitle = "Ward No: " + Convert.ToString(002),
                Location = "Somewhere else",
                Description = "This is the description of ward 002."
            },
            new Ward()
            {
                WardID = 003,
                WardTitle = "Ward No: " + Convert.ToString(003),
                Location = "Somewhere",
                Description = "This is the description of ward 003."
            },
            new Ward()
            {
                WardID = 004,
                WardTitle = "Ward No: " + Convert.ToString(004),
                Location = "Somewhere",
                Description = "This is the description of ward 004."
            },
            new Ward()
            {
                WardID = 005,
                WardTitle = "Ward No: " + Convert.ToString(005),
                Location = "Somewhere over the rainbow",
                Description = "This is the description of ward 005."
            }
        };
    }
}
