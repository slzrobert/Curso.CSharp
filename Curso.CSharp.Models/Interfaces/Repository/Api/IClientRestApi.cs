using RestSharp;

namespace Curso.CSharp.Models.Interfaces.Repository.Api
{
    public interface IClientRestApi
    {
        RestRequest RestRequestGetAsync();
        RestRequest RestRequestPostAsync();
    }
}
