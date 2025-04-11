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
    public class TypeProduitService : BaseService, ITypeProduitService
    {
        private readonly IndustrialSystemDbContext _context;
        private readonly IMapper _mapper;

        public TypeProduitService(IndustrialSystemDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TypeProduitDto> GetByIdAsync(int id)
        {
            var typeProduit = await _context.TypeProduits
                .Include(tp => tp.Produits)
                .FirstOrDefaultAsync(tp => tp.Id == id);

            if (typeProduit == null)
                throw new NotFoundException($"TypeProduit with ID {id} not found.");

            return _mapper.Map<TypeProduitDto>(typeProduit);
        }

        public async Task<IEnumerable<TypeProduitDto>> GetAllAsync()
        {
            var typesProduits = await _context.TypeProduits
                .Include(tp => tp.Produits)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TypeProduitDto>>(typesProduits);
        }

        public async Task<TypeProduitDto> CreateAsync(TypeProduitDto typeProduitDto)
        {
            // Check if a type with the same name already exists
            var existingType = await _context.TypeProduits
                .FirstOrDefaultAsync(tp => tp.Nom.ToLower() == typeProduitDto.Nom.ToLower());

            if (existingType != null)
                throw new ValidationException($"A product type with the name '{typeProduitDto.Nom}' already exists.");

            var typeProduit = _mapper.Map<TypeProduit>(typeProduitDto);
            
            _context.TypeProduits.Add(typeProduit);
            await _context.SaveChangesAsync();

            return _mapper.Map<TypeProduitDto>(typeProduit);
        }

        public async Task<TypeProduitDto> UpdateAsync(int id, TypeProduitDto typeProduitDto)
        {
            var typeProduit = await _context.TypeProduits.FindAsync(id);
            if (typeProduit == null)
                throw new NotFoundException($"TypeProduit with ID {id} not found.");

            // Check if the new name conflicts with an existing type
            var existingType = await _context.TypeProduits
                .FirstOrDefaultAsync(tp => tp.Id != id && tp.Nom.ToLower() == typeProduitDto.Nom.ToLower());

            if (existingType != null)
                throw new ValidationException($"A product type with the name '{typeProduitDto.Nom}' already exists.");

            _mapper.Map(typeProduitDto, typeProduit);
            await _context.SaveChangesAsync();

            return _mapper.Map<TypeProduitDto>(typeProduit);
        }

        public async Task DeleteAsync(int id)
        {
            var typeProduit = await _context.TypeProduits
                .Include(tp => tp.Produits)
                .FirstOrDefaultAsync(tp => tp.Id == id);

            if (typeProduit == null)
                throw new NotFoundException($"TypeProduit with ID {id} not found.");

            // Check if there are any products using this type
            if (typeProduit.Produits.Any())
                throw new ValidationException($"Cannot delete product type '{typeProduit.Nom}' as it is being used by {typeProduit.Produits.Count} products.");

            _context.TypeProduits.Remove(typeProduit);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TypeProduitDto>> SearchAsync(string searchTerm)
        {
            var typesProduits = await _context.TypeProduits
                .Include(tp => tp.Produits)
                .Where(tp => tp.Nom.Contains(searchTerm))
                .ToListAsync();

            return _mapper.Map<IEnumerable<TypeProduitDto>>(typesProduits);
        }

        public async Task<bool> IsTypeInUseAsync(int id)
        {
            return await _context.Produits
                .AnyAsync(p => p.TypeProduitId == id);
        }

        public async Task<int> GetProductCountByTypeAsync(int typeId)
        {
            return await _context.Produits
                .CountAsync(p => p.TypeProduitId == typeId);
        }
    }
}