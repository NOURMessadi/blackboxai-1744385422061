using IndustrialSystem.DTOs.Produits;
using IndustrialSystem.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Produits
{
    public interface IFamilleProduitService : IBaseService
    {
        Task<FamilleProduitDto> GetByIdAsync(int id);
        Task<IEnumerable<FamilleProduitDto>> GetAllAsync();
        Task<FamilleProduitDto> CreateAsync(FamilleProduitDto familleProduitDto);
        Task<FamilleProduitDto> UpdateAsync(int id, FamilleProduitDto familleProduitDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<FamilleProduitDto>> SearchAsync(string searchTerm);
        Task<bool> IsFamilleInUseAsync(int id);
        Task<int> GetProductCountByFamilleAsync(int familleId);
        Task<IEnumerable<FamilleProduitDto>> GetFamillesByTypeProduitAsync(int typeProduitId);
    }
}