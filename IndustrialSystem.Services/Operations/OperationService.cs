using AutoMapper;
using IndustrialSystem.Data;
using IndustrialSystem.Data.Entities;
using IndustrialSystem.DTOs.Operations;
using IndustrialSystem.Services.Common;
using IndustrialSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialSystem.Services.Operations
{
    public class OperationService : BaseService, IOperationService
    {
        private readonly IndustrialSystemDbContext _context;
        private readonly IMapper _mapper;

        public OperationService(IndustrialSystemDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationDto> GetByIdAsync(int id)
        {
            var operation = await _context.Operations
                .Include(o => o.Produit)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (operation == null)
                throw new NotFoundException($"Operation with ID {id} not found.");

            return _mapper.Map<OperationDto>(operation);
        }

        public async Task<IEnumerable<OperationDto>> GetAllAsync()
        {
            var operations = await _context.Operations
                .Include(o => o.Produit)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OperationDto>>(operations);
        }

        public async Task<OperationDto> CreateAsync(OperationDto operationDto)
        {
            // Verify if Produit exists
            if (operationDto.ProduitId.HasValue)
            {
                var produit = await _context.Produits.FindAsync(operationDto.ProduitId.Value);
                if (produit == null)
                    throw new NotFoundException($"Produit with ID {operationDto.ProduitId.Value} not found.");
            }

            var operation = _mapper.Map<Operation>(operationDto);
            
            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return _mapper.Map<OperationDto>(operation);
        }

        public async Task<OperationDto> UpdateAsync(int id, OperationDto operationDto)
        {
            var operation = await _context.Operations.FindAsync(id);
            if (operation == null)
                throw new NotFoundException($"Operation with ID {id} not found.");

            // Verify if Produit exists if it's being updated
            if (operationDto.ProduitId.HasValue)
            {
                var produit = await _context.Produits.FindAsync(operationDto.ProduitId.Value);
                if (produit == null)
                    throw new NotFoundException($"Produit with ID {operationDto.ProduitId.Value} not found.");
            }

            _mapper.Map(operationDto, operation);
            await _context.SaveChangesAsync();

            return _mapper.Map<OperationDto>(operation);
        }

        public async Task DeleteAsync(int id)
        {
            var operation = await _context.Operations.FindAsync(id);
            if (operation == null)
                throw new NotFoundException($"Operation with ID {id} not found.");

            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OperationDto>> GetByProduitAsync(int produitId)
        {
            var operations = await _context.Operations
                .Include(o => o.Produit)
                .Where(o => o.ProduitId == produitId)
                .OrderBy(o => o.Sequence)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OperationDto>>(operations);
        }

        public async Task<IEnumerable<OperationDto>> SearchAsync(string searchTerm)
        {
            var operations = await _context.Operations
                .Include(o => o.Produit)
                .Where(o => o.Nom.Contains(searchTerm) || o.Description.Contains(searchTerm))
                .ToListAsync();

            return _mapper.Map<IEnumerable<OperationDto>>(operations);
        }

        public async Task<bool> ValidateOperationSequenceAsync(int produitId, IEnumerable<int> operationIds)
        {
            // Get all operations for the product
            var productOperations = await _context.Operations
                .Where(o => o.ProduitId == produitId)
                .ToListAsync();

            // Check if all provided operation IDs belong to the product
            var validOperations = productOperations
                .Where(o => operationIds.Contains(o.Id))
                .OrderBy(o => o.Sequence)
                .ToList();

            // If the count doesn't match, some operations don't belong to this product
            if (validOperations.Count != operationIds.Count())
                return false;

            // Check if the sequence is continuous and starts from 1
            for (int i = 0; i < validOperations.Count; i++)
            {
                if (validOperations[i].Sequence != i + 1)
                    return false;
            }

            return true;
        }
    }
}