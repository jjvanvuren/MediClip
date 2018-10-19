using System;
namespace MediClip.Objects
{
    public class Notes
    {
            public int NoteID { get; set; }
            public int PatientID { get; set; }
            public String NoteTitle { get; set; }
            public String Note { get; set; }
            public String Picture { get; set; }
    }
}
