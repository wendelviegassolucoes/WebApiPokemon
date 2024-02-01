using RestSharp;

namespace WebApiPokemon.Infra
{
    public class Conexao
    {
        public string baseUri {get;} = "https://pokeapi.co/api/v2/pokemon/";

        public string? endpoint {get; set;}

        public  RestClient client {get;}

        public RestRequest request {get; set;}

        public Conexao(string? endpoint = null)
        {
            this.endpoint = endpoint;

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                client = new RestClient(baseUri);
            }
            else
            {
                client = new RestClient("https://pokeapi.co/api/v2/pokemon/" + endpoint);
            }

            request = new RestRequest();
        }

        public Conexao CreateConnection()
        {
            return new Conexao();
        }     
    }
}