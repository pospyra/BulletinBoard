﻿
using Microsoft.AspNetCore.Identity;
using Otiva.Domain.Ads;
using Otiva.Domain.Photos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Domain.User
{
    public class DomainUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Region { get; set; } 

        public string PhoneNumber { get; set; }

        public DateTime DateBirthday { get; set; }

        public DateTime DateRegistration { get; set; } = DateTime.UtcNow;
        
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }

        public ICollection<Ad> Ads { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PhotoUsers> Photos { get; set; }
        public ICollection<ItemSelectedAd> ItemSelectedAds { get; set; }
    }
}
