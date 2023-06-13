using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.PhotoDto
{
    /// <summary>
    /// Содержимое фотографии
    /// </summary>
    public class PhotoContentResponse
    {
        /// <summary>
        /// Контент фотографии в виде зашифрованной строки 
        /// </summary>
        public string KodBase64 { get; set; }
    }
}
