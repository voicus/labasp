﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phone_Market.DTO
{
    public class CurrentUserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
