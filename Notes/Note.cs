using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class NotesCl
    {
        public int Id { get; set; }

        public string Titel { get; set; }

        public string Content { get; set; }

        public NotesCl(int id, string titel, string content)
        {
            Id = id;
            Titel = titel;
            Content = content;
        }

        public override string? ToString()
        {
            return this.Titel;
        }
    }
}
