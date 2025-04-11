using AutoMapper;
using IndustrialSystem.Data;
using IndustrialSystem.Data.Entities;
using IndustrialSystem.DTOs.Parametres;
using IndustrialSystem.Services.Common;
using IndustrialSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Parametres
{
    public class ParametreService : BaseService, IParametreService
    {
        private readonly IndustrialSystemDbContext _context;
        private readonly IMapper _mapper;

        public ParametreService(IndustrialSystemDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParametreDto> GetByIdAsync(int id)
        {
            var parametre = await _context.Parametres
                .Include(p => p.Poste)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (parametre == null)
                throw new NotFoundException($"Parametre with ID {id} not found.");

            return _mapper.Map<ParametreDto>(parametre);
        }

        public async Task<IEnumerable<ParametreDto>> GetAllAsync()
        {
            var parametres = await _context.Parametres
                .Include(p => p.Poste)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ParametreDto>>(parametres);
        }

        public async Task<ParametreDto> CreateAsync(ParametreDto parametreDto)
        {
            // Validate parameter type
            if (!await ValidateParameterValueAsync(parametreDto.Type, parametreDto.Valeur))
                throw new ValidationException("Invalid parameter value for the specified type.");

            // Verify if Poste exists when it's a detailed parameter
            if (parametreDto.PosteId.HasValue)
            {
                var poste = await _context.Postes.FindAsync(parametreDto.PosteId.Value);
                if (poste == null)
                    throw new NotFoundException($"Poste with ID {parametreDto.PosteId.Value} not found.");
            }

            var parametre = _mapper.Map<Parametre>(parametreDto);
            
            _context.Parametres.Add(parametre);
            await _context.SaveChangesAsync();

            return _mapper.Map<ParametreDto>(parametre);
        }

        public async Task<ParametreDto> UpdateAsync(int id, ParametreDto parametreDto)
        {
            var parametre = await _context.Parametres.FindAsync(id);
            if (parametre == null)
                throw new NotFoundException($"Parametre with ID {id} not found.");

            // Validate parameter type
            if (!await ValidateParameterValueAsync(parametreDto.Type, parametreDto.Valeur))
                throw new ValidationException("Invalid parameter value for the specified type.");

            // Verify if Poste exists when it's a detailed parameter
            if (parametreDto.PosteId.HasValue)
            {
                var poste = await _context.Postes.FindAsync(parametreDto.PosteId.Value.Value);
                if (poste == null)
                    throw new NotFoundException($"Poste with ID {parametreDto.PosteId.Value} not found.");
            }

            _mapper.Map(parametreDto, parametre);
            await _context.SaveChangesAsync();

            return _mapper.Map<ParametreDto>(parametre);
        }

        public async Task DeleteAsync(int id)
        {
            var parametre = await _context.Parametres.FindAsync(id);
            if (parametre == null)
                throw new NotFoundException($"Parametre with ID {id} not found.");

            _context.Parametres.Remove(parametre);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ParametreDto>> GetGlobalParametersAsync()
        {
            var globalParameters = await _context.Parametres
                .Where(p => p.Type == "global" && !p.PosteId.HasValue)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ParametreDto>>(globalParameters);
        }

        public async Task<IEnumerable<ParametreDto>> GetDetailedParametersByPosteAsync(int posteId)
        {
            var detailedParameters = await _context.Parametres
                .Include(p => p.Poste)
                .Where(p => p.Type == "détaillé" && p.PosteId == posteId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ParametreDto>>(detailedParameters);
        }

        public async Task<IEnumerable<ParametreDto>> SearchAsync(string searchTerm)
        {
            var parametres = await _context.Parametres
                .Include(p => p.Poste)
                .Where(p => p.Nom.Contains(searchTerm) || p.Valeur.Contains(searchTerm))
                .ToListAsync();

            return _mapper.Map<IEnumerable<ParametreDto>>(parametres);
        }

        public async Task<bool> ValidateParameterValueAsync(string type, string value)
        {
            // Basic validation based on parameter type
            switch (type.ToLower())
            {
                case "global":
                    // Global parameters can have any value
                    return true;

                case "détaillé":
                    // Detailed parameters should not be empty
                    return !string.IsNullOrWhiteSpace(value);

                case "numérique":
                    // Numeric parameters should be valid numbers
                    return decimal.TryParse(value, out _);

                case "booléen":
                    // Boolean parameters should be true/false
                    return bool.TryParse(value, out _);

                case "date":
                    // Date parameters should be valid dates
                    return DateTime.TryParse(value, out _);

                default:
                    return false;
            }
        }

        public async Task<ParametreDto> UpdateParameterValueAsync(int id, string newValue)
        {
            var parametre = await _context.Parametres.FindAsync(id);
            if (parametre == null)
                throw new NotFoundException($"Parametre with ID {id} not found.");

            // Validate the new value
            if (!await ValidateParameterValueAsync(parametre.Type, newValue))
                throw new ValidationException("Invalid parameter value for the specified type.");

            parametre.Valeur = newValue;
            await _context.SaveChangesAsync();

            return _mapper.Map<ParametreDto>(parametre);
        }
    }
}