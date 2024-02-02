using Mongo.Repository;
using Newtonsoft.Json;

namespace WebApiPokemon.Dominio.Modelo
{
    public class Pokemon : MongoEntity
    {
        public int? idPokemon { get; set; }

        public string name { get; set; }

        public int? base_experience { get; set; }

        public int? height { get; set; }

        public bool is_default { get; set; }

        public int? order { get; set; }

        public int? weight { get; set; }

        public List<Ability> abilities { get; set; }

        public List<Form> forms { get; set; }

        public List<GameIndex> game_indices { get; set; }

        public List<HeldItem> held_items { get; set; }

        public string location_area_encounters { get; set; }

        public List<Move> moves { get; set; }

        public Species species { get; set; }

        public Sprites sprites { get; set; }

        public List<Stat> stats { get; set; }

        public List<Type> types { get; set; }

        public List<PastType> past_types { get; set; }

        public Feed feed { get; set; }

        public class Feed
        {
            public int? feedLevel { get; set; }
        }

        public class Ability
        {
            public bool is_hidden { get; set; }
            public int? slot { get; set; }
            public AbilityDetail ability { get; set; }
        }

        public class AbilityDetail
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Form
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class GameIndex
        {
            public int? game_index { get; set; }
            public Version version { get; set; }
        }

        public class Version
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class HeldItem
        {
            public Item item { get; set; }
            public List<VersionDetail> version_details { get; set; }
        }

        public class Item
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class VersionDetail
        {
            public int? rarity { get; set; }
            public Version version { get; set; }
        }

        public class Move
        {
            public MoveDetail move { get; set; }
            public List<VersionGroupDetail> version_group_details { get; set; }
        }

        public class MoveDetail
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class VersionGroupDetail
        {
            public int? level_learned_at { get; set; }
            public VersionGroup version_group { get; set; }
            public MoveLearnMethod move_learn_method { get; set; }
        }

        public class VersionGroup
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class MoveLearnMethod
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Species
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Sprites
        {
            public string back_default { get; set; }
            public object back_female { get; set; }
            public string back_shiny { get; set; }
            public object back_shiny_female { get; set; }
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
            public Other other { get; set; }
            public Versions versions { get; set; }
        }

        public class Other
        {
            public DreamWorld dream_world { get; set; }
            public Home home { get; set; }
            public OfficialArtwork official_artwork { get; set; }
            public Showdown showdown { get; set; }
        }

        public class DreamWorld
        {
            public string front_default { get; set; }
            public object front_female { get; set; }
        }

        public class Home
        {
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
        }

        public class OfficialArtwork
        {
            public string front_default { get; set; }
            public string front_shiny { get; set; }
        }

        public class Showdown
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

        public class Versions
        {
            public GenerationI generation_i { get; set; }
            public GenerationIi generation_ii { get; set; }
            public GenerationIii generation_iii { get; set; }
            public GenerationIv generation_iv { get; set; }
            public GenerationV generation_v { get; set; }
            public GenerationVi generation_vi { get; set; }
            public GenerationVii generation_vii { get; set; }
            public GenerationViii generation_viii { get; set; }
        }

        public class GenerationI
        {
            public RedBlue red_blue { get; set; }
            public Yellow yellow { get; set; }
        }

        public class RedBlue
        {
            public string back_default { get; set; }
            public string back_gray { get; set; }
            public string front_default { get; set; }
            public string front_gray { get; set; }
        }

        public class Yellow
        {
            public string back_default { get; set; }
            public string back_gray { get; set; }
            public string front_default { get; set; }
            public string front_gray { get; set; }
        }

        public class GenerationIi
        {
            public Crystal crystal { get; set; }
            public Crystal gold { get; set; }
            public Crystal silver { get; set; }
        }

        public class Crystal
        {
            public string back_default { get; set; }
            public string back_shiny { get; set; }
            public string front_default { get; set; }
            public string front_shiny { get; set; }
        }

        public class GenerationIii
        {
            public Emerald emerald { get; set; }
            public Emerald firered_leafgreen { get; set; }
            public Emerald ruby_sapphire { get; set; }
        }

        public class Emerald
        {
            public string front_default { get; set; }
            public string front_shiny { get; set; }
        }

        public class GenerationIv
        {
            public DiamondPearl diamond_pearl { get; set; }
            public DiamondPearl heartgold_soulsilver { get; set; }
            public DiamondPearl platinum { get; set; }
        }

        public class DiamondPearl
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

        public class GenerationV
        {
            public BlackWhite black_white { get; set; }
        }

        public class BlackWhite
        {
            public Animated animated { get; set; }
            public string back_default { get; set; }
            public object back_female { get; set; }
            public string back_shiny { get; set; }
            public object back_shiny_female { get; set; }
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
        }

        public class Animated
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

        public class GenerationVi
        {
            public OmegarubyAlphasapphire omegaruby_alphasapphire { get; set; }
            public XY x_y { get; set; }
        }

        public class OmegarubyAlphasapphire
        {
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
        }

        public class XY
        {
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
        }

        public class GenerationVii
        {
            public Icons icons { get; set; }
            public UltraSunUltraMoon ultra_sun_ultra_moon { get; set; }
        }

        public class Icons
        {
            public string front_default { get; set; }
            public object front_female { get; set; }
        }

        public class UltraSunUltraMoon
        {
            public string front_default { get; set; }
            public object front_female { get; set; }
            public string front_shiny { get; set; }
            public object front_shiny_female { get; set; }
        }

        public class GenerationViii
        {
            public Icons icons { get; set; }
        }

        public class Stat
        {
            public int? base_stat { get; set; }
            public int? effort { get; set; }
            public StatDetail stat { get; set; }
        }

        public class StatDetail
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Type
        {
            public int? slot { get; set; }
            public TypeDetail type { get; set; }
        }

        public class TypeDetail
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class PastType
        {
            public Generation generation { get; set; }
            public List<Type> types { get; set; }
        }

        public class Generation
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }
}