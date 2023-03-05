using Microsoft.VisualBasic;
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
    public class MessageRepository : IMessageRepository
    {
        public readonly IBaseRepository<Message> _baseRepository;

        public MessageRepository(IBaseRepository<Message> baseRepository) 
        { 
            _baseRepository = baseRepository;
        }
        public Task Add(Message model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Message model)
        {
           await _baseRepository.DeleteAsync(model);
        }

        public async Task EditAdAsync(Message model)
        {
            await _baseRepository.UpdateAsync(model);
        }

        public async Task<Message> FindByIdAsync(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<Message> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
