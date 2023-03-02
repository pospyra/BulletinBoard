using Otiva.AppServeces.IRepository;
using Otiva.Domain;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class SelectedAdsRepository : ISelectedAdsRepository
    {
        public readonly IBaseRepository<SelectedAd> _baseRepository;

        public SelectedAdsRepository(IBaseRepository<SelectedAd> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task AddAsync(SelectedAd model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(SelectedAd model)
        {
            await _baseRepository.DeleteAsync(model);
        }

        public IQueryable<SelectedAd> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
