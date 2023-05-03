using TextFileStatistics;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        HashSet<string> paths = GetPaths();

        var watch = Stopwatch.StartNew();

        List<TextStatistics> FilesStats = await RunStatsAsync(paths);
        DisplayResults(FilesStats);

        watch.Stop();
        WriteLine("\nElapsed milliseconds: " + watch.ElapsedMilliseconds.ToString());

        WriteLine("\nPress Enter to close this window.");
        ReadLine();

    }


    public static HashSet<string> GetPaths()
    {
        //TODO: Limit the number of input paths ?

        //use a HashSet to prevent duplication
        HashSet<string> paths = new();

        Console.WriteLine("Enter local file path or URL and press Enter:");

        //loop to get all the input paths
        //break when user presses Enter withour any input
        while (true)
        {
            string? path = ReadLine()?.Trim().ToLower();
            if (string.IsNullOrEmpty(path)) break;
            if (paths.Contains(path)) WriteLine("Path already exxists!");
            else paths.Add(path);
            Console.WriteLine("Add more paths or just press Enter to start calculations.");
        }

        return paths;
    }

    public static async Task<List<TextStatistics>> RunStatsAsync(HashSet<string> paths)
    {
        if (paths == null || paths.Count == 0)
        {
            Console.WriteLine("No input provided");
            return null;
        }

        Console.WriteLine($"Calculating statistics for {paths.Count} inputs.\nPlease wait ...\n");
        var results = new List<TextStatistics>();

        foreach (var path in paths)
        {
            try
            {
                //create an instance of the TextStatistics class
                TextStatistics textStatistics = new TextStatistics(path);

                //calculate the statistics for the input file
                //await textStatistics.ProcessTextAsync();
                await textStatistics.ProcessTextReallyAsync();

                results.Add(textStatistics);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        return results;
    }

    private static void DisplayResults(List<TextStatistics> stats)
    {
        if (stats == null || stats.Count == 0)
        {
            Console.WriteLine("Terminating the program");
            return;
        }
        try
        {
            List<IWordFrequency> totalTopFrequentWords = new();
            List<string> totalTopLongestWords = new();
            long totalNumberOfLines = 0;
            long totalNumberOfWords = 0;

            foreach (var s in stats)
            {

                List<IWordFrequency> topFrequentWords = s.topWords(20);
                List<string> topLongestWords = s.longestWords(10);
                long numberOfLines = s.numberOfLines();
                long numberOfWords = s.numberOfWords();

                WriteResultsToConsole(s.filePath, topFrequentWords, topLongestWords, numberOfLines, numberOfWords);
                                
                //calculate totals
                totalTopFrequentWords.AddRange(topFrequentWords);
                totalTopLongestWords.AddRange(topLongestWords);
                totalNumberOfLines += numberOfLines;
                totalNumberOfWords += numberOfWords;
            }

            //sort and select top n items from totals
            var overalTops = totalTopFrequentWords
                .OrderByDescending(x => x.frequency)
                .Take(20)
                .ToList();

            var overalLongests = totalTopLongestWords
                .OrderByDescending(x => x.Length)
                .Take(10)
                .ToList();

            WriteResultsToConsole("Total statistics for all inputs combined", overalTops, overalLongests, totalNumberOfLines, totalNumberOfWords);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static void WriteResultsToConsole(string path, List<IWordFrequency> topFrequentWords, List<string> topLongestWords, long numberOfLines, long numberOfWords)
    {
        Console.WriteLine($"\nStatistics for: {path} \n");

        Console.WriteLine("Top frequent words:");
        foreach (var word in topFrequentWords)
        {
            Console.WriteLine($"{word.word}: {word.frequency}");
        }

        Console.WriteLine("\nTop longest words:");
        foreach (var word in topLongestWords)
        {
            Console.WriteLine(word);
        }

        Console.WriteLine($"Number of lines : {numberOfLines}");
        Console.WriteLine($"Number of words : {numberOfWords}");
    }

}
 