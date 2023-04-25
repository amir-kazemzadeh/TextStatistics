README

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
    5. Start reading each file, using a StreamReader to read each line. Then split the line to words. Filter out any punctuation, blanks or empty lines.
    6. Handle possible IO errors while reading files
    7. for each word, update each Dictionary for word count and length, using CuncurrentDictionary’s methods
    8. When the file operation is finished, sort the dictionary on frequency or length and take the top N most frequent words and the top M longest words
    9. show the results seperately for each file
    10. combine the top results for all files to produce overall results


Notes:
   
    • we are not counting empty or blank lines in our numberOfLines or totalNumberOfLines.
    • when calculating overal results for most frequent words, the number of occurances 
        for the same word in all input texts is not added up. Instead the most occurance in a single text is shown.
    

