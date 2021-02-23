using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.HttpClient
{
    public interface IResilientHttpClient
    {
        Task<HttpResponseMessage> DeleteAsync(string uri, string authToken = null);

        Task<HttpResponseMessage> GetAsync(string uri, string authToken = null);

        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authToken = null);

        Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authToken = null);
    }
}