using Mongo.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiPokemon.Dominio.Modelo
{
    public class Form : MongoEntity
    {
        [BsonIgnoreIfNull]
        public string form_name { get; set; }

        [BsonIgnoreIfNull]

        public List<FormName> form_names { get; set; }

        [BsonIgnoreIfNull]
        public int? form_order { get; set; }

        [BsonIgnoreIfNull]
        public int? idPokemon { get; set; }

        [BsonIgnoreIfNull]
        public bool is_battle_only { get; set; }

        [BsonIgnoreIfNull]
        public bool is_default { get; set; }

        [BsonIgnoreIfNull]
        public bool is_mega { get; set; }

        [BsonIgnoreIfNull]
        public string name { get; set; }

        [BsonIgnoreIfNull]
        public List<Name> names { get; set; }

        [BsonIgnoreIfNull]
        public int? order { get; set; }

        [BsonIgnoreIfNull]
        public PokemonDetail pokemon { get; set; }

        [BsonIgnoreIfNull]
        public Sprites sprites { get; set; }

        [BsonIgnoreIfNull]
        public List<TypeData> types { get; set; }

        [BsonIgnoreIfNull]
        public VersionGroup version_group { get; set; }

        public class FormName
        {
            public Language language { get; set; }
            public string name { get; set; }
        }

        public class Language
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Name
        {
            public Language Language { get; set; }
            public string name { get; set; }
        }


        public class PokemonDetail
        {
            [BsonIgnoreIfNull]
            public string name { get; set; }

            [BsonIgnoreIfNull]
            public string url { get; set; }
        }

        public class Sprites
        {
            [BsonIgnoreIfNull]
            public string back_default { get; set; }

            [BsonIgnoreIfNull]
            public object back_female { get; set; }

            [BsonIgnoreIfNull]
            public string back_shiny { get; set; }

            [BsonIgnoreIfNull]
            public object back_shiny_female { get; set; }

            [BsonIgnoreIfNull]
            public string front_default { get; set; }

            [BsonIgnoreIfNull]
            public object front_female { get; set; }

            [BsonIgnoreIfNull]
            public string front_shiny { get; set; }

            [BsonIgnoreIfNull]
            public object front_shiny_female { get; set; }
        }

        public class TypeData
        {
            [BsonIgnoreIfNull]
            public int? slot { get; set; }

            [BsonIgnoreIfNull]
            public TypeDetail type { get; set; }
        }

        public class TypeDetail
        {
            [BsonIgnoreIfNull]
            public string name { get; set; }

            [BsonIgnoreIfNull]
            public string url { get; set; }
        }

        public class VersionGroup
        {
            [BsonIgnoreIfNull]
            public string name { get; set; }

            [BsonIgnoreIfNull]
            public string url { get; set; }
        }
    }
}
