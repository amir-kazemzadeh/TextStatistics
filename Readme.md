README

The Problem:
    This programm takes as input the path to one or more local or online text files and produces 
    the following outputs for each single text and also for all texts combined:
    1. Top N (default = 20) most frequent words
    2. Top M (default = 10) longest words
    3. Number of lines
    4. Number of words

Method:
    These are the general steps to get the input file and produce the outputs:

    1. Get one or more text files from input
    2. process all text files as asynchronous tasks in parallel to gain performance
    3. for each file, use 2 data structures to keep track of word count and word length
    4. to be thread-safe, we use CuncurrentDictionary in .NET
    5. Start reading each file, using a StreamReader to read each line. Then split the line to words using a Regex. 
       Filter out any punctuation, blanks or empty lines.
    6. Handle possible IO errors while reading files
    7. for each word, update each Dictionary for word count and length, using CuncurrentDictionary’s methods
    8. When the file operation is finished, sort the dictionaries on frequency or length 
       and take the top N most frequent words and the top M longest words
    9. show the results seperately for each file
    10. combine the top results for all files to produce overall results

Code Structure:
    The solution contains 2 projects. One that calculates the statistics and also a test project.
    Tha main project has two classes:
        1. TextReader is a static class whith a HttpClient member and a GetStreamAsync() method. 
           the method gets the path to the as input and determins if it's a local or on thw web. 
           It then uses an appropriate method to provide a "stream" as output.
        2. TextStatistics is responsible to get a file path and the ProcessTextAsync() method uses
           the TextReader class above, to get a stream for the input and maintain 2 thread-safe dictionaries
           of type CuncurrentDictionary to keep track of all the words in the text and their frequency and length.
           It also keeps two counter variables to keep the number of lines and words.
        3. The Program.cs file has 4 methods which are called by the Main method to implement and showcase 
           the functionality of the above 2 classes. GetPaths() reads the user input in a loop to get
           all the input paths. RunStatsAsync() uses the TextStatistics class to calculate statistics for each input.
           DisplayResults() sorts and selects the results. It also aggregates results from all inputs 
           to produce overall results. It then sends the results to WriteResultsToConsole to show them on screen.
           
Notes:
   
    • we are not counting empty or blank lines in our numberOfLines or totalNumberOfLines.
    • when calculating overal results for most frequent words, the number of occurances 
        for the same word in all input texts is not added up. Instead the most occurance in a single text is shown.
    • The number of input paths is not limited but it's a good idea to put a maximum on it to prevent high memory usage
      or overflow errors in the dictionaries and counters.
    

