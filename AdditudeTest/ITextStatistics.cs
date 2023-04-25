using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileStatistics
{
    public interface ITextStatistics
    {
        List<IWordFrequency> topWords(int n);
        
        List<string> longestWords(int n);

        long numberOfWords();

        long numberOfLines();
    }
}
