﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otiva.Domain.Ads;

namespace Otiva.Domain.Photos
{
    public class PhotoAds
    {
        public Guid Id { get; set; }

        public string KodBase64 { get; set; }

        public Guid? AdId { get; set; }

        public Ad Ad { get; set; }
    }
}
