﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class ContactValues
    {
        public bool Sem { get; set; }

        public bool Active { get; set; }

        public string? Priority { get; set; }

        public List<List<string?>> Values { get; set; } = new List<List<string?>>();
    }
}

