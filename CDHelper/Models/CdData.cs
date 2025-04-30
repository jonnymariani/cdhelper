using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDHelper.Models
{
    public class CdData
    {
        public string Name { get; set; }
        public string User { get; set; }

        public CdData(string name, string user)
        {
            Name = name;
            User = user;
        }

        public CdData(TraxSongInfo info)
        {
            Name = info.CdName ?? string.Empty;
            User = info.User ?? string.Empty;
        }
    }
}
