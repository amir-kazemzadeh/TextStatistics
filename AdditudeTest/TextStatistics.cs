using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace TextFileStatistics;

public class TextStatistics : ITextStatistics
{
    // thread safe dictionary to store the frequency of each word 
    private ConcurrentDictionary<string, long> wordFrequencies 
        = new ConcurrentDictionary<string, long>();

    // thrad safe dictionary to store the length of each word
    private ConcurrentDictionary<string, int> wordLengths 
        = new ConcurrentDictionary<string, int>();

    // total number of words in the text file
    private long wordCount = 0;

    // total number of lines in the text file
    private long lineCount = 0;

    public readonly string filePath;

    public TextStatistics(string filePath)
    {
        this.filePath = filePath;
    }
    
    // 
    public async Task ProcessTextAsync()
    {
        try
        {
            //use a StreamReader to read the text file and set it to use Async
            using var stream = await TextReader.GetStreamAsync(filePath);

            using var streamReader = new StreamReader(stream);

            //using var streamReader = await TextReader.GetFileStreamAsync(filePath);

            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();

                if (!string.IsNullOrWhiteSpace(line))
                {
                    //thread-safe increment for line count 
                    Interlocked.Increment(ref lineCount);

                    // Split the line into words
                    var words = Regex.Split(line, @"[^\p{L}']+");


                    // Increment the frequency count for each word
                    foreach (var word in words)
                    {
                        if (!string.IsNullOrWhiteSpace(word))
                        {
                            //thread-safe increment for wordCount
                            Interlocked.Increment(ref wordCount);

                            // Increment the frequency count for this word
                            wordFrequencies.AddOrUpdate(word.ToLowerInvariant(), 1, (k, v) => v + 1);

                            // Update the list of longest words
                            wordLengths.TryAdd(word.ToLowerInvariant(), word.Length);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception($"Could not generate text statistics for file: {filePath} \nInner Exception:{ex?.InnerException.Message}");
        }
    }

    public List<IWordFrequency> topWords(int n)
    {
        // Return the top n most frequent words
        return wordFrequencies
            .AsParallel()
            .OrderByDescending(x => x.Value)
            .Take(n)
            .Select(x => new WordFrequency(x.Key, x.Value))
            .ToList<IWordFrequency>();
    }

    public List<string> longestWords(int n)
    {
        // Return the n longest words
        return wordLengths
            .AsParallel()
            .OrderByDescending(x => x.Value)
            .Take(n)
            .Select(x => x.Key)
            .ToList();

    }

    public long numberOfWords()
    {
        // Return the total number of words in the text
        return wordCount;
    }

    public long numberOfLines()
    {
        // Return the total number of lines in the text
        return lineCount;
    }
}


