using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Otiva.AppServeces.IRepository;
using Otiva.Contracts.AdDto;
using Otiva.Domain;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class AdRepository : IAdRepository
    {
        public readonly IBaseRepository<Ad> _baseRepository;

        public AdRepository(IBaseRepository<Ad> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<Ad> FindWhere(Expression<Func<Ad, bool>> predicate)
        {
            var data = _baseRepository.GetAllFiltered(predicate);

            return await data.Where(predicate).FirstOrDefaultAsync();
        }

        public Task Add(Ad model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Ad ad, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(ad);
        }

        public async Task EditAdAsync(Ad edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Ad> FindByIdAsync(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyCollection<Ad>> GetAllAsync(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetAll()
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Ad>> GetByFilterAsync(SearchFilterAd search, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            var query = await _baseRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(search.Name))
                query =  query.Where(p => p.Name.ToLower().Contains(search.Name.ToLower()));

            if (search.SubcategoryId.HasValue)
                query = query.Where(c => c.SubcategoryId == search.SubcategoryId);

            if (search.UserId.HasValue)
                query = query.Where(c => c.DomainUserId == search.UserId);

            if (search.PriceFrom != null)
                query = query.Where(c => c.Price >= search.PriceFrom);

            if (search.PriceTo != null)
                query = query.Where(c => c.Price <= search.PriceTo);

            return await query.Select(p => new Ad
            {
                Id = p.Id,
                Name = p.Name,
                SubcategoryId = p.SubcategoryId,
                Description = p.Description,
                Region = p.Region,
                Price = p.Price,
                CreateTime = p.CreateTime
            }).ToListAsync();
        }
    }
}
