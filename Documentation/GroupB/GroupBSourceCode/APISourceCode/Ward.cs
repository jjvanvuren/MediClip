using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediClipWebAPI.Models
{
    // Model for retrieving wards from the MediClip database
    public class Ward
    {
        public int WardID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}