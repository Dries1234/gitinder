using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitinder
{
    public class Repository
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int stargazerCount { get; set; }
        public Language primaryLanguage { get; set; }

        public string url { get; set; }

        public bool Validate()
        {
            if (id != null && name != null && description != null && primaryLanguage.name !=null && url != null)
            {
                return true;
            }
            return false;
        }
    }
    public class Language
    {
        public string name { get; set; }
    }
}
