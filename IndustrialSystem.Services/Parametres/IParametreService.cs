using IndustrialSystem.DTOs.Parametres;
using IndustrialSystem.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Parametres
{
    public interface IParametreService : IBaseService
    {
        Task<ParametreDto> GetByIdAsync(int id);
        Task<IEnumerable<ParametreDto>> GetAllAsync();
        Task<ParametreDto> CreateAsync(ParametreDto parametreDto);
        Task<ParametreDto> UpdateAsync(int id, ParametreDto parametreDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ParametreDto>> GetGlobalParametersAsync();
        Task<IEnumerable<ParametreDto>> GetDetailedParametersByPosteAsync(int posteId);
        Task<IEnumerable<ParametreDto>> SearchAsync(string searchTerm);
        Task<bool> ValidateParameterValueAsync(string type, string value);
        Task<ParametreDto> UpdateParameterValueAsync(int id, string newValue);
    }
}