using IndustrialSystem.DTOs.Nomenclatures;
using IndustrialSystem.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Nomenclatures
{
    public interface INomenclatureService : IBaseService
    {
        Task<NomenclatureDto> GetByIdAsync(int id);
        Task<IEnumerable<NomenclatureDto>> GetAllAsync();
        Task<NomenclatureDto> CreateAsync(NomenclatureDto nomenclatureDto);
        Task<NomenclatureDto> UpdateAsync(int id, NomenclatureDto nomenclatureDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<NomenclatureDto>> GetByProduitAsync(int produitId);
        Task<bool> ValidateComposantsAsync(int produitId, IDictionary<int, decimal> composantsQuantites);
        Task<decimal> CalculerQuantiteTotaleComposantAsync(int composantId);
    }
}