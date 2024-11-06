using System.Net.Http;

namespace API.Network.Web
{
    public interface IWebTransfer
    {
        /// <summary>
        ///  Send a GET request to the specified Uri including the WebIg parameters.
        /// </summary>
        /// <param name="url">Url the request is sent to.</param>
        /// <returns>Response to the get request.</returns>
        HttpResponseMessage Get(string url);
    }
}