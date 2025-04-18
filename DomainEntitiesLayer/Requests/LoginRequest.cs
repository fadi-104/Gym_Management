﻿using System.ComponentModel.DataAnnotations;

namespace DomainEntitiesLayer.Requests
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
