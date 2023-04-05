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
        public Task Add(Message model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();
            return _baseRepository.AddAsync(model);
        }

        /// <summary>
        /// Удалить сообщение
        /// </summary>
        /// <param name="model">Моднль сообщения</param>
        /// <returns></returns>
        public async Task DeleteAsync(Message model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(model);
        }

        /// <summary>
        /// Редактировать сообщение
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditAdAsync(Message model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(model);
        }

        /// <summary>
        /// Найти сообщение по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Message> FindByIdAsync(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Получит все сообщения
        /// </summary>
        /// <returns></returns>
        public IQueryable<Message> GetAll(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.GetAll();
        }
    }
}
