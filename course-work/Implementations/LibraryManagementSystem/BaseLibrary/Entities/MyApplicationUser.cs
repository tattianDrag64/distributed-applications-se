﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class MyApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
