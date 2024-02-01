using AutoMapper;
using WebApiPokemon.Dominio.Modelo;
using WebApiPokemon.Dto;

namespace WebApiPokemon.Mapper
{
    public class Mappping
    {
        public class MappingDtoPokemon : Profile
        {
            public MappingDtoPokemon()
            {
                MapperConfiguration config = new(cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.CreateMap<PokemonDto1, Pokemon>();
                    cfg.CreateMap<Dto.Ability, Dominio.Modelo.Pokemon.Ability>();
                    cfg.CreateMap<Dto.AbilityDetail, Dominio.Modelo.Pokemon.AbilityDetail>();
                    cfg.CreateMap<Dto.Form, Dominio.Modelo.Pokemon.Form>();
                    cfg.CreateMap<Dto.GameIndex, Dominio.Modelo.Pokemon.GameIndex>();
                    cfg.CreateMap<Dto.Version, Dominio.Modelo.Pokemon.Version>();
                    cfg.CreateMap<Dto.HeldItem, Dominio.Modelo.Pokemon.HeldItem>();
                    cfg.CreateMap<Dto.Item, Dominio.Modelo.Pokemon.Item>();
                    cfg.CreateMap<Dto.VersionDetail, Dominio.Modelo.Pokemon.VersionDetail>();
                    cfg.CreateMap<Dto.Version, Dominio.Modelo.Pokemon.Version>();
                    cfg.CreateMap<Dto.Move, Dominio.Modelo.Pokemon.Move>();
                    cfg.CreateMap<Dto.MoveDetail, Dominio.Modelo.Pokemon.MoveDetail>();
                    cfg.CreateMap<Dto.Species, Dominio.Modelo.Pokemon.Species>();
                    cfg.CreateMap<Dto.VersionGroupDetail, Dominio.Modelo.Pokemon.VersionGroupDetail>();
                    cfg.CreateMap<Dto.VersionGroup, Dominio.Modelo.Pokemon.VersionGroup>();
                    cfg.CreateMap<Dto.MoveLearnMethod, Dominio.Modelo.Pokemon.MoveLearnMethod>();
                    cfg.CreateMap<Dto.Stat, Dominio.Modelo.Pokemon.Stat>();
                    cfg.CreateMap<Dto.StatDetail, Dominio.Modelo.Pokemon.StatDetail>();
                    cfg.CreateMap<Dto.TypeDetail, Dominio.Modelo.Pokemon.TypeDetail>();
                    cfg.CreateMap<Dto.Type, Dominio.Modelo.Pokemon.Type>();
                    cfg.CreateMap<Dto.PastType, Dominio.Modelo.Pokemon.PastType>();
                    cfg.CreateMap<Dto.Generation, Dominio.Modelo.Pokemon.Generation>();
                    cfg.CreateMap<Dto.Sprites, Dominio.Modelo.Pokemon.Sprites>();
                    cfg.CreateMap<Dto.Other, Dominio.Modelo.Pokemon.Other>();
                    cfg.CreateMap<Dto.Versions, Dominio.Modelo.Pokemon.Versions>();
                    cfg.CreateMap<Dto.DreamWorld, Dominio.Modelo.Pokemon.DreamWorld>();
                    cfg.CreateMap<Dto.Home, Dominio.Modelo.Pokemon.Home>();
                    cfg.CreateMap<Dto.OfficialArtwork, Dominio.Modelo.Pokemon.OfficialArtwork>();
                    cfg.CreateMap<Dto.Showdown, Dominio.Modelo.Pokemon.Showdown>();
                    cfg.CreateMap<Dto.GenerationI, Dominio.Modelo.Pokemon.GenerationI>();
                    cfg.CreateMap<Dto.RedBlue, Dominio.Modelo.Pokemon.RedBlue>();
                    cfg.CreateMap<Dto.Yellow, Dominio.Modelo.Pokemon.Yellow>();
                    cfg.CreateMap<Dto.GenerationIi, Dominio.Modelo.Pokemon.GenerationIi>();
                    cfg.CreateMap<Dto.Crystal, Dominio.Modelo.Pokemon.Crystal>();
                    cfg.CreateMap<Dto.GenerationIii, Dominio.Modelo.Pokemon.GenerationIii>();
                    cfg.CreateMap<Dto.Emerald, Dominio.Modelo.Pokemon.Emerald>();
                    cfg.CreateMap<Dto.GenerationIv, Dominio.Modelo.Pokemon.GenerationIv>();
                    cfg.CreateMap<Dto.DiamondPearl, Dominio.Modelo.Pokemon.DiamondPearl>();
                    cfg.CreateMap<Dto.GenerationV, Dominio.Modelo.Pokemon.GenerationV>();
                    cfg.CreateMap<Dto.BlackWhite, Dominio.Modelo.Pokemon.BlackWhite>();
                    cfg.CreateMap<Dto.GenerationVi, Dominio.Modelo.Pokemon.GenerationVi>();
                    cfg.CreateMap<Dto.OmegarubyAlphasapphire, Dominio.Modelo.Pokemon.GenerationVi>();
                    cfg.CreateMap<Dto.XY, Dominio.Modelo.Pokemon.GenerationVi>();
                    cfg.CreateMap<Dto.GenerationVii, Dominio.Modelo.Pokemon.GenerationVii>();
                    cfg.CreateMap<Dto.Icons, Dominio.Modelo.Pokemon.Icons>();
                    cfg.CreateMap<Dto.UltraSunUltraMoon, Dominio.Modelo.Pokemon.UltraSunUltraMoon>();
                    cfg.CreateMap<Dto.GenerationViii, Dominio.Modelo.Pokemon.GenerationViii>();
                });

                Mapper = config.CreateMapper();
            }

            public IMapper Mapper { get; }
        }

        public class MappingDtoForm : Profile
        {
            public MappingDtoForm()
            {
                MapperConfiguration config = new(cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.CreateMap<FormDto, Dominio.Modelo.Form>();
                    cfg.CreateMap<FormDto.FormNameDto, Dominio.Modelo.Form.FormName>();
                    cfg.CreateMap<FormDto.NameDto, Dominio.Modelo.Form.Name>();
                    cfg.CreateMap<FormDto.LanguageDto, Dominio.Modelo.Form.Language>();
                    cfg.CreateMap<FormDto.PokemonDetailDto, Dominio.Modelo.Form.PokemonDetail>();
                    cfg.CreateMap<FormDto.TypeDataDto, Dominio.Modelo.Form.TypeData>();
                    cfg.CreateMap<FormDto.SpritesDto, Dominio.Modelo.Form.Sprites>();
                    cfg.CreateMap<FormDto.VersionGroupDto, Dominio.Modelo.Form.VersionGroup>();
                    cfg.CreateMap<FormDto.TypeDetailDto, Dominio.Modelo.Form.TypeDetail>();
                });

                Mapper = config.CreateMapper();
            }

            public IMapper Mapper { get; }
        }

        //     public class MappingEmpresaDto : Profile
        //     {
        //         public MappingEmpresaDto()
        //         {
        //             MapperConfiguration config = new(cfg =>
        //             {
        //                 cfg.AllowNullCollections = true;
        //                 cfg.CreateMap<Empresa, EmpresaDto>();
        //             });

        //             Mapper = config.CreateMapper();
        //         }

        //         public IMapper Mapper { get; }
        //     }
        // }
    }
}
