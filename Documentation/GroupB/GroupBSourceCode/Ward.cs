using System;
using System.Collections.Generic;
using System.Text;

namespace MediClip.Models
{
    // Purpose: Used to POST and GET Ward data from the SQL server
    public class Ward
    {
        public int WardID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
