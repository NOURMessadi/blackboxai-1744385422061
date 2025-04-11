using IndustrialSystem.DTOs.Produits;
using IndustrialSystem.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Produits
{
    public interface ITypeProduitService : IBaseService
    {
        Task<TypeProduitDto> GetByIdAsync(int id);
        Task<IEnumerable<TypeProduitDto>> GetAllAsync();
        Task<TypeProduitDto> CreateAsync(TypeProduitDto typeProduitDto);
        Task<TypeProduitDto> UpdateAsync(int id, TypeProduitDto typeProduitDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<TypeProduitDto>> SearchAsync(string searchTerm);
        Task<bool> IsTypeInUseAsync(int id);
        Task<int> GetProductCountByTypeAsync(int typeId);
    }
}