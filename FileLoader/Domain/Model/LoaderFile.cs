using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class LoaderFile
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public bool Status { get; set; }
        public string PathToFile { get; set;}
    }
}
