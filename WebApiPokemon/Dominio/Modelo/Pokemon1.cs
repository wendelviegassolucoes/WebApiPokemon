using Mongo.Repository;

namespace WebApiPokemon.Dominio.Modelo
{
    public class Pokemon1
    {
        public string count { get; set; }

        public string next { get; set; }

        public string previous { get; set; }

        public List<Results> results { get; set; }
    }

    public class Results
    {
        public string name { get; set; }

        public string url { get; set; }
    }   
}