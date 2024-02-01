namespace WebApiPokemon.Dto
{
    public class FormDto
    {
        public string form_name { get; set; }
        public List<FormNameDto> form_names { get; set; }
        public int? form_order { get; set; }
        public int? id { get; set; }
        public bool is_battle_only { get; set; }
        public bool is_default { get; set; }
        public bool is_mega { get; set; }
        public string name { get; set; }
        public List<object> names { get; set; }
        public int? order { get; set; }
        public PokemonDetailDto pokemon { get; set; }
        public SpritesDto sprites { get; set; }
        public List<TypeDataDto> types { get; set; }
        public VersionGroupDto version_group { get; set; }

        public class FormNameDto
        {
            public LanguageDto language { get; set; }
            public string name { get; set; }
        }

        public class LanguageDto
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class NameDto
        {
            public LanguageDto language { get; set; }
            public string name { get; set; }
        }


        public class PokemonDetailDto
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class SpritesDto
        {
            public string back_default { get; set; }
            public object back_female { get; set; }
            public string back_shiny { get; set; }
            public object back_shiny_female { get; set; }
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
        }

        public class TypeDataDto
        {
            public int? slot { get; set; }
            public TypeDetailDto type { get; set; }
        }

        public class TypeDetailDto
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class VersionGroupDto
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }
}