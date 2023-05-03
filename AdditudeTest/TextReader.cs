using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace TextFileStatistics;
public static class TextReader
{
    //use a static HttpClient to be shared 
    public static readonly HttpClient httpClient = new HttpClient();

    public static async Task<Stream> GetStreamAsync(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("The path argument must not be null or empty.", nameof(path));
        }

        Stream stream;

        try
        {
            if (Uri.TryCreate(path, UriKind.Absolute, out Uri? uri) && uri.Scheme != "file")
            {
                //path is a URL
                var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                stream = await response.Content.ReadAsStreamAsync();
                return stream;
            }
            else
            {
                // path is a local file 
                stream =  new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
                return stream;
            }
        }
        catch (FileNotFoundException ex)
        {
            throw new Exception($"This file does not exist: {path}.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading file : {path}.", ex);
        }
    }


}
    