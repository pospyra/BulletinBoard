﻿using Microsoft.AspNetCore.Http;
using Otiva.Contracts.Attributs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.UserDto
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; }

        public string Region { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateBirthday { get; set; }

        public string? Role { get; set; }

        public ICollection<Guid>? PhotoId { get; set; }
    }
}
