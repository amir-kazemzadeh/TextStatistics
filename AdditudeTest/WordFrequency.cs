using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileStatistics
{
    internal class WordFrequency : IWordFrequency
    {
        public string word { get; set; }
        public long frequency { get; set; }

        public WordFrequency(string word, long frequency)
        {
            this.word = word;
            this.frequency = frequency;
        }
    }
}
