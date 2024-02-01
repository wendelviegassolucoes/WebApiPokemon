namespace Mongo.Repository
{
    public class MongoConfiguration
    {
        public MongoConfiguration Executa()
        {
            return new MongoConfiguration()
            {
                User = Environment.GetEnvironmentVariable("MONGO_USER"), //Inserir na variável de ambiente da máquina via DockerFile
                Password = Environment.GetEnvironmentVariable("MONGO_PASSWORD") //Inserir na variável de ambiente da máquina via DockerFile
            };
        }

        public string? User { get; set; }

        public string? Password { get; set; }
    }
}