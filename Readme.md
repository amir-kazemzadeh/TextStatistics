README

Method:
I took the following steps to solve the problem:

    1. Get one or more text files from input
    2. process all text files as asynchronous tasks in parallel to gain performance
    3. for each file, use 2 Dictionary data structures to keep track of word count and word length
    4. to be thread-safe, we use CuncurrentDictionary in .NET
    5. Start reading each file, using a StreamReader to read each line. Then split the line to words. Filter out any punctuation, blanks or empty lines.
    6. Handle possible IO errors while reading files
    7. for each word, update each Dictionary for word count and length, using CuncurrentDictionary’s methods
    8. When the file operation is finished, sort the dictionary on keys (words) and take the top n most frequent words and the top m longest words
    9. show the results seperately for each file
    10. combine the top results for all files to produce overall results


Notes:

I have done some modifications to the supplied interfaces to be more aligned with C# and .NET style, conventions and standards and also to extend the usability:
    • renamed the interfaces to follow the naming conventions in C# (start with letter I)
    • changed the interface methods to Properties to be more aligned with C# standards instead of Java
    • added members to get the combined overall statistics of all input texts
    • we are not counting empty line in our numberOfLines or totalNumberOfLines


