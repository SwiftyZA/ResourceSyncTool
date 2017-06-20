using System;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoogleAPIClient
{
    /// <summary>
    /// GET YOUR MIND OUT OF THE GUTTER!!! This is a reference to watergate...
    /// Information originating from an obscure source.
    /// </summary>
    public class DeepThroat : IDisposable
    {
        private HttpClient Client { get; set; }

        /// <summary>
        /// GET YOUR MIND OUT OF THE GUTTER!!! This is a reference to watergate...
        /// Information originating from an obscure source.
        /// </summary>
        public DeepThroat()
        {
            Client = new HttpClient { BaseAddress = new Uri("https://translate.googleapis.com/") };
        }

        /// <summary>
        /// Calls Google's translate API to translate provided text
        /// </summary>
        /// <param name="sourceLang">
        /// The culture string of the source text language. "auto" if the language is unknown
        /// </param>
        /// <param name="targetLang">
        /// The langauge to which the text should be translated
        /// </param>
        /// <param name="sourceText">
        /// The text to be translated
        /// </param>
        /// <returns>
        /// Translated text as a string
        /// </returns>
        public async Task<string> TranslateText(string sourceLang, string targetLang, string sourceText)
        {
            //Hit google
            var resp = await Client.GetAsync(GoolgeURL(sourceLang, targetLang, sourceText));
            //return empty string if the call failed
            if (!resp.IsSuccessStatusCode) return string.Empty;
            //Extract result string
            var googleResult = await resp.Content.ReadAsStringAsync();
            //Convert to json
            var json = (JArray)JsonConvert.DeserializeObject(googleResult);
            //retrun result - but check that there is something to return
            return !json[0].HasValues ? string.Empty : json[0][0][0].ToString();
        }

        /// <summary>
        /// Build google api url
        /// </summary>
        /// <param name="sourceLang">
        /// The culture string of the source text language. "auto" if the language is unknown
        /// </param>
        /// <param name="targetLang">
        /// The langauge to which the text should be translated
        /// </param>
        /// <param name="sourceText">
        /// The text to be translated
        /// </param>
        /// <returns>
        /// Translated text as a string
        /// </returns>
        private string GoolgeURL(string sourceLang, string targetLang, string sourceText)
        {
            return $"translate_a/single?client=gtx&sl={EncodeKey(sourceLang)}&tl={EncodeKey(targetLang)}&dt=t&q={EncodeKey(sourceText)}";
        }

        /// <summary>
        /// Encoded a string value
        /// </summary>
        /// <param name="key">
        /// Value to encode
        /// </param>
        /// <returns></returns>
        private string EncodeKey(string key)
        {
            return HttpUtility.UrlEncode(key);
        }

        public void Dispose()
        {
            Client.CancelPendingRequests();
            Client.Dispose();
        }
    }
}
