using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileStatistics
{
    public interface IWordFrequency
    {
        public string word { get; set; }
        public long frequency { get; set; }
    }
}
