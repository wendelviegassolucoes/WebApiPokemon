using Mongo.Repository;
using Newtonsoft.Json;
using RestSharp;
using WebApiPokemon.Dominio.Modelo;
using WebApiPokemon.Infra;
using WebApiPokemon.Dto;
using static WebApiPokemon.Mapper.Mappping;

namespace WebApiPokemon.Services
{
    public class PokemonAppService
    {
        private Conexao conexao;
        public IMongoRepository<Pokemon>? Repository { get; }

        public IMongoRepository<Dominio.Modelo.Form>? RepositoryForms { get; }

        private MappingDtoPokemon mappingEmpresaDto;

        private MappingDtoForm mappingDtoForm;

        public PokemonAppService()
        {
            conexao = new Conexao();
            Repository = new MongoRepository<Pokemon>("Pokemon", "Pokemon");
            RepositoryForms = new MongoRepository<Dominio.Modelo.Form>("Pokemon", "Forms");
            mappingEmpresaDto = new();
            mappingDtoForm = new();
        }

        public bool FeedPokemon(string pokemonId)
        {
            string pokemonIdSatinizado = Path.GetFileName(pokemonId);
            pokemonIdSatinizado = Path.GetFileNameWithoutExtension(pokemonIdSatinizado); // Obtém o nome do arquivo sem a extensão
            Pokemon? pokemon = Repository.TakeList(x => x.idPokemon.ToString() == pokemonIdSatinizado).FirstOrDefault();

            if (pokemon != null)
            {
                pokemon.feed ??= new Pokemon.Feed();
                pokemon.feed.feedLevel = 100;
                Repository.Update(pokemon);
                return true;
            }

            return false;
        }

        public bool KillPokemon(string pokemonId)
        {
            string pokemonIdSatinizado = Path.GetFileName(pokemonId);
            pokemonIdSatinizado = Path.GetFileNameWithoutExtension(pokemonIdSatinizado); // Obtém o nome do arquivo sem a extensão
            Pokemon? pokemon = Repository.TakeList(x => x.idPokemon.ToString() == pokemonIdSatinizado).FirstOrDefault();

            if (pokemon != null)
            {
                pokemon.killed = true;
                Repository.Update(pokemon);
                return true;
            }

            return false;
        }

        public bool RevivePokemons()
        {
            try
            {
                List<Pokemon>? pokemons = Repository.TakeList(x => x.killed).ToList();
                pokemons.ForEach(x => x.killed = false);
                Repository.UpdateMany(pokemons);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public async Task InsertFormsAsync()
        //{

        //    List<Pokemon>? pokemons = Repository.TakeList().ToList();

        //    List<Dominio.Modelo.Form> forms = new List<Dominio.Modelo.Form>();

        //    if (pokemons != null)
        //    {
        //        foreach (Pokemon pokemon in pokemons)
        //        {
        //            try
        //            {
        //                string pokemonFormEndpoint = pokemon.forms.FirstOrDefault().url;
        //                conexao.request = new RestRequest(pokemonFormEndpoint);
        //                RestResponse response = await conexao.client.ExecuteAsync(conexao.request);
        //                FormDto? formdto = JsonConvert.DeserializeObject<FormDto>(response.Content);
        //                Dominio.Modelo.Form form = mappingDtoForm.Mapper.Map<FormDto, Dominio.Modelo.Form>(formdto);
        //                form.Id = null;
        //                form.idPokemon = formdto.id;
        //                forms.Add(form);
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }

        //        await RepositoryForms.InsertManyAsync(forms);
        //    }

        //}

        //public async Task<bool> InsertPokemonAsync()
        //{
        //    int pageNumber = 1;
        //    int pageSize = 20;
        //    int offset = (pageNumber - 1) * pageSize;
        //    string uriPagination = conexao.baseUri + conexao.endpoint + $"?limit={pageSize}&offset={offset}";
        //    conexao.request = new RestRequest(uriPagination);
        //    RestResponse response = await conexao.client.ExecuteAsync(conexao.request);
        //    Pokemon1? pokemon1 = JsonConvert.DeserializeObject<Pokemon1>(response.Content);
        //    List<Pokemon> pokemons = new();

        //    while (pokemon1.next != null)
        //    {
        //        foreach (Dominio.Modelo.Results result in pokemon1.results)
        //        {
        //            try
        //            {
        //                Pokemon? pokemonBD = Repository.TakeList(x => x.name == result.name).FirstOrDefault();

        //                if (pokemonBD != null)
        //                {
        //                    continue;
        //                }

        //                conexao.request = new RestRequest($"https://pokeapi.co/api/v2/pokemon/{result.name}/");
        //                response = await conexao.client.ExecuteAsync(conexao.request);
        //                PokemonDto1? pokemondto = JsonConvert.DeserializeObject<PokemonDto1>(response.Content);
        //                pokemonBD = mappingEmpresaDto.Mapper.Map<PokemonDto1, Pokemon>(pokemondto);
        //                pokemonBD.Id = null;
        //                pokemonBD.idPokemon = pokemondto.id;
        //                pokemons.Add(pokemonBD);
        //            }
        //            catch (Exception ex)
        //            {
        //                return false;
        //            }
        //        }

        //        await Repository.InsertManyAsync(pokemons);
        //        pokemons = new();
        //        uriPagination = pokemon1.next;
        //        conexao.request = new RestRequest(uriPagination);
        //        response = await conexao.client.ExecuteAsync(conexao.request);
        //        pokemon1 = JsonConvert.DeserializeObject<Pokemon1>(response.Content);
        //    }

        //    return true;
        //}

        public async Task<List<PokemonDto>> GetAllPokemonsDtoPaginationAsync()
        {
            try
            {
                IEnumerable<Pokemon>? pokemons = await Repository.TakeListAsync(x => x.killed == false);
                List<Dominio.Modelo.Form> forms = RepositoryForms.TakeList().ToList();
                List<PokemonDto> pokemons_dto = new();

                if (pokemons != null)
                {
                    foreach (Pokemon pokemon in pokemons.OrderBy(x => x.idPokemon).ToList())
                    {
                        Dominio.Modelo.Form? form = forms.FirstOrDefault(x => x.idPokemon == pokemon.idPokemon || x.name == pokemon.name);

                        if (form == null ||
                            form.sprites == null ||
                            string.IsNullOrWhiteSpace(form.sprites.front_default))
                        {
                            continue;
                        }

                        pokemons_dto.Add(new PokemonDto()
                        {
                            Name = pokemon.name,
                            ImageUrl = form.sprites.front_default
                        });
                    }
                }

                return pokemons_dto;
            }
            catch (Exception)
            {
                return new List<PokemonDto>();
            }
        }

        public List<PokemonDto> GetPokemonsDtoPagination(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                int skipElements = (pageNumber - 1) * pageSize;
                List<Pokemon>? pokemons = Repository.TakeList(x => x.killed == false, skipElements: skipElements, returnElements: pageSize).OrderBy(x => x.idPokemon).ToList();
                List<Dominio.Modelo.Form> forms = RepositoryForms.TakeList().ToList();
                List<PokemonDto> pokemons_dto = new();

                if (pokemons != null)
                {
                    foreach (Pokemon pokemon in pokemons.OrderBy(x => x.idPokemon).ToList())
                    {
                        Dominio.Modelo.Form? form = forms.FirstOrDefault(x => x.idPokemon == pokemon.idPokemon || x.name == pokemon.name);

                        if (form == null ||
                            form.sprites == null ||
                            string.IsNullOrWhiteSpace(form.sprites.front_default))
                        {
                            continue;
                        }

                        pokemons_dto.Add(new PokemonDto()
                        {
                            Name = pokemon.name,
                            ImageUrl = form.sprites.front_default
                        });
                    }
                }

                return pokemons_dto;
            }
            catch (Exception)
            {
                return new List<PokemonDto>();
            }
        }

        //public async Task<List<PokemonDto>>? GetPokemonsDtoAsync()
        //{
        //    RestResponse response = await conexao.client.ExecuteAsync(conexao.request);

        //    try
        //    {
        //        Pokemon1? pokemon1 = JsonConvert.DeserializeObject<Pokemon1>(response.Content);
        //        List<PokemonDto> pokemons_dto = new();

        //        if (pokemon1.results != null)
        //        {
        //            foreach (Dominio.Modelo.Results result in pokemon1.results)
        //            {
        //                string pokemonFormEndpoint = "https://pokeapi.co/api/v2/pokemon-form/" + result.name;
        //                conexao.request = new RestRequest(pokemonFormEndpoint);
        //                response = await conexao.client.ExecuteAsync(conexao.request);
        //                Forms? forms = JsonConvert.DeserializeObject<Forms>(response.Content);

        //                pokemons_dto.Add(new PokemonDto()
        //                {
        //                    Name = result.name,
        //                    ImageUrl = forms.sprites.front_default
        //                });
        //            }
        //        }

        //        return pokemons_dto.OrderBy(x => x.Name).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return new List<PokemonDto>();
        //    }
        //}
    }
}