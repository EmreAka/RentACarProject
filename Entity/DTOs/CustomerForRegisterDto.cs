﻿using Core.Entities;

namespace Entity.DTOs
{
    public class CustomerForRegisterDto: IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
}