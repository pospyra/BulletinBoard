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
        Task<Message> FindByIdAsync(Guid id);
        IQueryable<Message> GetAll();

        Task Add(Message model);

        Task DeleteAsync(Message model);

        Task EditAdAsync(Message model);
    }
}
