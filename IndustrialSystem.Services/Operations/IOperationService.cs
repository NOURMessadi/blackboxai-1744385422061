using IndustrialSystem.DTOs.Operations;
using IndustrialSystem.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Operations
{
    public interface IOperationService : IBaseService
    {
        Task<OperationDto> GetByIdAsync(int id);
        Task<IEnumerable<OperationDto>> GetAllAsync();
        Task<OperationDto> CreateAsync(OperationDto operationDto);
        Task<OperationDto> UpdateAsync(int id, OperationDto operationDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<OperationDto>> GetByProduitAsync(int produitId);
        Task<IEnumerable<OperationDto>> SearchAsync(string searchTerm);
        Task<bool> ValidateOperationSequenceAsync(int produitId, IEnumerable<int> operationIds);
    }
}