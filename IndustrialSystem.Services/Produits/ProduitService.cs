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
    public class ProduitService : BaseService, IProduitService
    {
        private readonly IndustrialSystemDbContext _context;
        private readonly IMapper _mapper;

        public ProduitService(IndustrialSystemDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProduitDto> GetByIdAsync(int id)
        {
            var produit = await _context.Produits
                .Include(p => p.TypeProduit)
                .Include(p => p.FamilleProduit)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produit == null)
                throw new NotFoundException($"Produit with ID {id} not found.");

            return _mapper.Map<ProduitDto>(produit);
        }

        public async Task<IEnumerable<ProduitDto>> GetAllAsync()
        {
            var produits = await _context.Produits
                .Include(p => p.TypeProduit)
                .Include(p => p.FamilleProduit)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProduitDto>>(produits);
        }

        public async Task<ProduitDto> CreateAsync(ProduitDto produitDto)
        {
            // Verify if TypeProduit exists
            if (produitDto.TypeProduitId.HasValue)
            {
                var typeProduit = await _context.TypeProduits.FindAsync(produitDto.TypeProduitId.Value);
                if (typeProduit == null)
                    throw new NotFoundException($"TypeProduit with ID {produitDto.TypeProduitId.Value} not found.");
            }

            // Verify if FamilleProduit exists
            if (produitDto.FamilleProduitId.HasValue)
            {
                var familleProduit = await _context.FamilleProduits.FindAsync(produitDto.FamilleProduitId.Value);
                if (familleProduit == null)
                    throw new NotFoundException($"FamilleProduit with ID {produitDto.FamilleProduitId.Value} not found.");
            }

            var produit = _mapper.Map<Produit>(produitDto);
            
            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProduitDto>(produit);
        }

        public async Task<ProduitDto> UpdateAsync(int id, ProduitDto produitDto)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
                throw new NotFoundException($"Produit with ID {id} not found.");

            // Verify if TypeProduit exists
            if (produitDto.TypeProduitId.HasValue)
            {
                var typeProduit = await _context.TypeProduits.FindAsync(produitDto.TypeProduitId.Value);
                if (typeProduit == null)
                    throw new NotFoundException($"TypeProduit with ID {produitDto.TypeProduitId.Value} not found.");
            }

            // Verify if FamilleProduit exists
            if (produitDto.FamilleProduitId.HasValue)
            {
                var familleProduit = await _context.FamilleProduits.FindAsync(produitDto.FamilleProduitId.Value);
                if (familleProduit == null)
                    throw new NotFoundException($"FamilleProduit with ID {produitDto.FamilleProduitId.Value} not found.");
            }

            _mapper.Map(produitDto, produit);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProduitDto>(produit);
        }

        public async Task DeleteAsync(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
                throw new NotFoundException($"Produit with ID {id} not found.");

            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProduitDto>> GetByTypeProduitAsync(int typeProduitId)
        {
            var produits = await _context.Produits
                .Include(p => p.TypeProduit)
                .Include(p => p.FamilleProduit)
                .Where(p => p.TypeProduitId == typeProduitId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProduitDto>>(produits);
        }

        public async Task<IEnumerable<ProduitDto>> GetByFamilleProduitAsync(int familleProduitId)
        {
            var produits = await _context.Produits
                .Include(p => p.TypeProduit)
                .Include(p => p.FamilleProduit)
                .Where(p => p.FamilleProduitId == familleProduitId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProduitDto>>(produits);
        }

        public async Task<IEnumerable<ProduitDto>> SearchAsync(string searchTerm)
        {
            var produits = await _context.Produits
                .Include(p => p.TypeProduit)
                .Include(p => p.FamilleProduit)
                .Where(p => p.Nom.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProduitDto>>(produits);
        }
    }
}
