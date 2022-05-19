using Curso.CSharp.Models.Interfaces.Repository.Api;
using RestSharp;

namespace Curso.CSharp.Repository.Services
{
    public class PagSeguro : IClientRestApi
    {
        public RestRequest RestRequestGetAsync()
        {
            var request = new RestRequest("https://api.imirante.com/time");
            return request;
        }

        public RestRequest RestRequestPostAsync()
        {
            var request = new RestRequest("https://api.imirante.com/time", Method.Post);
            return request;
        }
    }
}
