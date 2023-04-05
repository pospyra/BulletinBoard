using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface IMessageRepository
    {
        Task<Message> FindByIdAsync(Guid id, CancellationToken cancellation);
        IQueryable<Message> GetAll(CancellationToken cancellation);

        Task Add(Message model, CancellationToken cancellation);

        Task DeleteAsync(Message model, CancellationToken cancellation);

        Task EditAdAsync(Message model, CancellationToken cancellation);
    }
}
