using System;
namespace MediClip.Models
{
    // Purpose: Used to POST and GET Note data from the SQL server
    public class Note
    {
            public int NoteID { get; set; }
            public int PatientID { get; set; }
            public String Title { get; set; }
            public String Text { get; set; }
            public String Picture { get; set; }
    }
}
