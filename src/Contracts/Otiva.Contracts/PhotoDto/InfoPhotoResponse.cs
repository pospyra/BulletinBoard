using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.PhotoDto
{
    /// <summary>
    /// Инормация о фотографии
    /// </summary>
    public class InfoPhotoResponse
    {
        /// <summary>
        /// Идентификатор фотографии
        /// </summary>
        public Guid PhotoId { get; set; }
    }
}
