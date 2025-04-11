using AutoMapper;
using IndustrialSystem.Data;
using IndustrialSystem.Data.Entities;
using IndustrialSystem.DTOs.Produits;
using IndustrialSystem.Services.Common;
using IndustrialSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Produits
{
    public class FamilleProduitService : BaseService, IFamilleProduitService
    {
        private readonly IndustrialSystemDbContext _context;
        private readonly IMapper _mapper;

        public FamilleProduitService(IndustrialSystemDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FamilleProduitDto> GetByIdAsync(int id)
        {
            var familleProduit = await _context.FamilleProduits
                .Include(fp => fp.Produits)
                .FirstOrDefaultAsync(fp => fp.Id == id);

            if (familleProduit == null)
                throw new NotFoundException($"FamilleProduit with ID {id} not found.");

            return _mapper.Map<FamilleProduitDto>(familleProduit);
        }

        public async Task<IEnumerable<FamilleProduitDto>> GetAllAsync()
        {
            var famillesProduits = await _context.FamilleProduits
                .Include(fp => fp.Produits)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FamilleProduitDto>>(famillesProduits);
        }

        public async Task<FamilleProduitDto> CreateAsync(FamilleProduitDto familleProduitDto)
        {
            // Check if a family with the same name already exists
            var existingFamily = await _context.FamilleProduits
                .FirstOrDefaultAsync(fp => fp.Nom.ToLower() == familleProduitDto.Nom.ToLower());

            if (existingFamily != null)
                throw new ValidationException($"A product family with the name '{familleProduitDto.Nom}' already exists.");

            var familleProduit = _mapper.Map<FamilleProduit>(familleProduitDto);
            
            _context.FamilleProduits.Add(familleProduit);
            await _context.SaveChangesAsync();

            return _mapper.Map<FamilleProduitDto>(familleProduit);
        }

        public async Task<FamilleProduitDto> UpdateAsync(int id, FamilleProduitDto familleProduitDto)
        {
            var familleProduit = await _context.FamilleProduits.FindAsync(id);
            if (familleProduit == null)
                throw new NotFoundException($"FamilleProduit with ID {id} not found.");

            // Check if the new name conflicts with an existing family
            var existingFamily = await _context.FamilleProduits
                .FirstOrDefaultAsync(fp => fp.Id != id && fp.Nom.ToLower() == familleProduitDto.Nom.ToLower());

            if (existingFamily != null)
                throw new ValidationException($"A product family with the name '{familleProduitDto.Nom}' already exists.");

            _mapper.Map(familleProduitDto, familleProduit);
            await _context.SaveChangesAsync();

            return _mapper.Map<FamilleProduitDto>(familleProduit);
        }

        public async Task DeleteAsync(int id)
        {
            var familleProduit = await _context.FamilleProduits
                .Include(fp => fp.Produits)
                .FirstOrDefaultAsync(fp => fp.Id == id);

            if (familleProduit == null)
                throw new NotFoundException($"FamilleProduit with ID {id} not found.");

            // Check if there are any products using this family
            if (familleProduit.Produits.Any())
                throw new ValidationException($"Cannot delete product family '{familleProduit.Nom}' as it is being used by {familleProduit.Produits.Count} products.");

            _context.FamilleProduits.Remove(familleProduit);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FamilleProduitDto>> SearchAsync(string searchTerm)
        {
            var famillesProduits = await _context.FamilleProduits
                .Include(fp => fp.Produits)
                .Where(fp => fp.Nom.Contains(searchTerm))
                .ToListAsync();

            return _mapper.Map<IEnumerable<FamilleProduitDto>>(famillesProduits);
        }

        public async Task<bool> IsFamilleInUseAsync(int id)
        {
            return await _context.Produits
                .AnyAsync(p => p.FamilleProduitId == id);
        }

        public async Task<int> GetProductCountByFamilleAsync(int familleId)
        {
            return await _context.Produits
                .CountAsync(p => p.FamilleProduitId == familleId);
        }

        public async Task<IEnumerable<FamilleProduitDto>> GetFamillesByTypeProduitAsync(int typeProduitId)
        {
            var famillesProduits = await _context.FamilleProduits
                .Include(fp => fp.Produits)
                .Where(fp => fp.Produits.Any(p => p.TypeProduitId == typeProduitId))
                .ToListAsync();

            return _mapper.Map<IEnumerable<FamilleProduitDto>>(famillesProduits);
        }
    }
}