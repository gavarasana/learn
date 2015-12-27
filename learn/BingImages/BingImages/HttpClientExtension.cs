using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BingImages
{
    public static class HttpClientExtension
    {
        public static Stream DownloadImage(this HttpClient httpClient, string uri)
        {
            //var response = httpClient.PostAsync(uri, new StringContent(string.Empty)).Result;
            var response = httpClient.GetAsync(uri).Result;
            var stream = response.Content.ReadAsStreamAsync().Result;
            
            return stream;
        }

        public static Task<Stream> DownloadImageAsync(this HttpClient httpClient, string uri)
        {
            var task = httpClient.PostAsync(uri, new StringContent(string.Empty));
            return task.ContinueWith((pt) =>
            {
                return pt.Result.Content.ReadAsStreamAsync();
            }).Unwrap();
        }
    }
}
