using AutoMapper;
using IndustrialSystem.Data.Entities;
using IndustrialSystem.DTOs.Applications;
using IndustrialSystem.DTOs.Auth;
using IndustrialSystem.DTOs.Historique;
using IndustrialSystem.DTOs.Nomenclatures;
using IndustrialSystem.DTOs.Operations;
using IndustrialSystem.DTOs.Parametres;
using IndustrialSystem.DTOs.Postes;
using IndustrialSystem.DTOs.Produits;
using IndustrialSystem.DTOs.Utilisateurs;
using System.Text.Json;

namespace IndustrialSystem.API.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Utilisateurs
        CreateMap<Utilisateur, UtilisateurDto>();
        CreateMap<CreateUtilisateurDto, Utilisateur>();
        CreateMap<UpdateUtilisateurDto, Utilisateur>();

        // Postes et Types de Postes
        CreateMap<TypePoste, TypePosteDto>();
        CreateMap<CreateTypePosteDto, TypePoste>();
        CreateMap<UpdateTypePosteDto, TypePoste>();

        CreateMap<Poste, PosteDto>()
            .ForMember(dest => dest.TypePosteNom, 
                      opt => opt.MapFrom(src => src.TypePoste.Nom));
        CreateMap<Poste, PosteMinimalDto>()
            .ForMember(dest => dest.TypePosteNom, 
                      opt => opt.MapFrom(src => src.TypePoste.Nom));
        CreateMap<CreatePosteDto, Poste>();
        CreateMap<UpdatePosteDto, Poste>();

        // Applications
        CreateMap<Application, ApplicationDto>();
        CreateMap<Application, ApplicationMinimalDto>();
        CreateMap<Application, ApplicationListDto>()
            .ForMember(dest => dest.PosteNom, 
                      opt => opt.MapFrom(src => src.Poste.Nom));
        CreateMap<CreateApplicationDto, Application>();
        CreateMap<UpdateApplicationDto, Application>();

        // Produits et Types/Familles
        CreateMap<TypeProduit, TypeProduitDto>();
        CreateMap<CreateTypeProduitDto, TypeProduit>();
        CreateMap<UpdateTypeProduitDto, TypeProduit>();

        CreateMap<FamilleProduit, FamilleProduitDto>();
        CreateMap<CreateFamilleProduitDto, FamilleProduit>();
        CreateMap<UpdateFamilleProduitDto, FamilleProduit>();

        CreateMap<Produit, ProduitDto>()
            .ForMember(dest => dest.TypeProduitNom, 
                      opt => opt.MapFrom(src => src.TypeProduit.Nom))
            .ForMember(dest => dest.FamilleProduitNom, 
                      opt => opt.MapFrom(src => src.FamilleProduit.Nom));
        CreateMap<Produit, ProduitMinimalDto>()
            .ForMember(dest => dest.TypeProduitNom, 
                      opt => opt.MapFrom(src => src.TypeProduit.Nom))
            .ForMember(dest => dest.FamilleProduitNom, 
                      opt => opt.MapFrom(src => src.FamilleProduit.Nom));
        CreateMap<CreateProduitDto, Produit>();
        CreateMap<UpdateProduitDto, Produit>();

        // Opérations
        CreateMap<Operation, OperationDto>();
        CreateMap<Operation, OperationMinimalDto>();
        CreateMap<CreateOperationDto, Operation>();
        CreateMap<UpdateOperationDto, Operation>();

        // Nomenclatures
        CreateMap<Nomenclature, NomenclatureDto>()
            .ForMember(dest => dest.Composants, 
                      opt => opt.MapFrom(src => JsonSerializer.Deserialize<List<ComposantNomenclature>>(src.Composants)));
        CreateMap<Nomenclature, NomenclatureMinimalDto>()
            .ForMember(dest => dest.ProduitFiniNom, 
                      opt => opt.MapFrom(src => src.ProduitFini.Nom))
            .ForMember(dest => dest.NombreComposants, 
                      opt => opt.MapFrom(src => JsonSerializer.Deserialize<List<ComposantNomenclature>>(src.Composants)?.Count ?? 0));
        CreateMap<CreateNomenclatureDto, Nomenclature>()
            .ForMember(dest => dest.Composants, 
                      opt => opt.MapFrom(src => JsonSerializer.Serialize(src.Composants)));
        CreateMap<UpdateNomenclatureDto, Nomenclature>()
            .ForMember(dest => dest.Composants, 
                      opt => opt.MapFrom(src => JsonSerializer.Serialize(src.Composants)));

        // Paramètres
        CreateMap<Parametre, ParametreDto>();
        CreateMap<Parametre, ParametreMinimalDto>()
            .ForMember(dest => dest.PosteNom, 
                      opt => opt.MapFrom(src => src.Poste != null ? src.Poste.Nom : null));
        CreateMap<CreateParametreDto, Parametre>();
        CreateMap<UpdateParametreDto, Parametre>();

        // Historique des actions
        CreateMap<HistoriqueAction, HistoriqueActionDto>()
            .ForMember(dest => dest.UtilisateurNom, 
                      opt => opt.MapFrom(src => src.Utilisateur.Nom));
        CreateMap<HistoriqueAction, HistoriqueActionResumeeDto>()
            .ForMember(dest => dest.UtilisateurNom, 
                      opt => opt.MapFrom(src => src.Utilisateur.Nom));
        CreateMap<CreateHistoriqueActionDto, HistoriqueAction>();
    }
}