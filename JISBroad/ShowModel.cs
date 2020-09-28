using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JISBroad
{
    public class ShowModel
    {
        public string Description { get; set; }
        public string CurrentSequence { get; set; }
        public string QueueCount { get; set; }
        public string LastShipped { get; set; }
        public string LocalBank { get; set; }
        public string OEMBank { get; set; }
    }
}
