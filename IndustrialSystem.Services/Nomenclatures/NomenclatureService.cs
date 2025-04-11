using AutoMapper;
using IndustrialSystem.Data;
using IndustrialSystem.Data.Entities;
using IndustrialSystem.DTOs.Nomenclatures;
using IndustrialSystem.Services.Common;
using IndustrialSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Nomenclatures
{
    public class NomenclatureService : BaseService, INomenclatureService
    {
        private readonly IndustrialSystemDbContext _context;
        private readonly IMapper _mapper;

        public NomenclatureService(IndustrialSystemDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NomenclatureDto> GetByIdAsync(int id)
        {
            var nomenclature = await _context.Nomenclatures
                .Include(n => n.Produit)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (nomenclature == null)
                throw new NotFoundException($"Nomenclature with ID {id} not found.");

            return _mapper.Map<NomenclatureDto>(nomenclature);
        }

        public async Task<IEnumerable<NomenclatureDto>> GetAllAsync()
        {
            var nomenclatures = await _context.Nomenclatures
                .Include(n => n.Produit)
                .ToListAsync();

            return _mapper.Map<IEnumerable<NomenclatureDto>>(nomenclatures);
        }

        public async Task<NomenclatureDto> CreateAsync(NomenclatureDto nomenclatureDto)
        {
            // Verify if Produit exists
            var produit = await _context.Produits.FindAsync(nomenclatureDto.ProduitId);
            if (produit == null)
                throw new NotFoundException($"Produit with ID {nomenclatureDto.ProduitId} not found.");

            // Validate composants
            if (!await ValidateComposantsAsync(nomenclatureDto.ProduitId, nomenclatureDto.Composants))
                throw new ValidationException("Invalid composants configuration.");

            var nomenclature = _mapper.Map<Nomenclature>(nomenclatureDto);
            
            _context.Nomenclatures.Add(nomenclature);
            await _context.SaveChangesAsync();

            return _mapper.Map<NomenclatureDto>(nomenclature);
        }

        public async Task<NomenclatureDto> UpdateAsync(int id, NomenclatureDto nomenclatureDto)
        {
            var nomenclature = await _context.Nomenclatures.FindAsync(id);
            if (nomenclature == null)
                throw new NotFoundException($"Nomenclature with ID {id} not found.");

            // Verify if Produit exists
            var produit = await _context.Produits.FindAsync(nomenclatureDto.ProduitId);
            if (produit == null)
                throw new NotFoundException($"Produit with ID {nomenclatureDto.ProduitId} not found.");

            // Validate composants
            if (!await ValidateComposantsAsync(nomenclatureDto.ProduitId, nomenclatureDto.Composants))
                throw new ValidationException("Invalid composants configuration.");

            _mapper.Map(nomenclatureDto, nomenclature);
            await _context.SaveChangesAsync();

            return _mapper.Map<NomenclatureDto>(nomenclature);
        }

        public async Task DeleteAsync(int id)
        {
            var nomenclature = await _context.Nomenclatures.FindAsync(id);
            if (nomenclature == null)
                throw new NotFoundException($"Nomenclature with ID {id} not found.");

            _context.Nomenclatures.Remove(nomenclature);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NomenclatureDto>> GetByProduitAsync(int produitId)
        {
            var nomenclatures = await _context.Nomenclatures
                .Include(n => n.Produit)
                .Where(n => n.ProduitId == produitId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<NomenclatureDto>>(nomenclatures);
        }

        public async Task<bool> ValidateComposantsAsync(int produitId, IDictionary<int, decimal> composantsQuantites)
        {
            // Verify that the product exists
            var produit = await _context.Produits.FindAsync(produitId);
            if (produit == null)
                return false;

            // Verify that all composants exist and are valid products
            foreach (var composantId in composantsQuantites.Keys)
            {
                var composant = await _context.Produits.FindAsync(composantId);
                if (composant == null)
                    return false;

                // Check that a product is not a component of itself
                if (composantId == produitId)
                    return false;

                // Check that quantities are positive
                if (composantsQuantites[composantId] <= 0)
                    return false;
            }

            // Check for circular dependencies
            var visited = new HashSet<int>();
            return !HasCircularDependency(produitId, visited);
        }

        private async Task<bool> HasCircularDependency(int produitId, HashSet<int> visited)
        {
            if (!visited.Add(produitId))
                return true;

            var nomenclatures = await _context.Nomenclatures
                .Where(n => n.ProduitId == produitId)
                .ToListAsync();

            foreach (var nomenclature in nomenclatures)
            {
                foreach (var composantId in nomenclature.Composants.Keys)
                {
                    if (await HasCircularDependency(composantId, visited))
                        return true;
                }
            }

            visited.Remove(produitId);
            return false;
        }

        public async Task<decimal> CalculerQuantiteTotaleComposantAsync(int composantId)
        {
            decimal quantiteTotale = 0;

            var nomenclatures = await _context.Nomenclatures
                .Where(n => n.Composants.ContainsKey(composantId))
                .ToListAsync();

            foreach (var nomenclature in nomenclatures)
            {
                if (nomenclature.Composants.TryGetValue(composantId, out decimal quantite))
                {
                    quantiteTotale += quantite;
                }
            }

            return quantiteTotale;
        }
    }
}