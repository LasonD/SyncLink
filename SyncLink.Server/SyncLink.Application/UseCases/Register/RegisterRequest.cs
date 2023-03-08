﻿using MediatR;
using SyncLink.Application.Contracts.Dtos;

namespace SyncLink.Application.UseCases.Register
{
    public class RegisterRequest : IRequest<AuthResult>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; }
    }
}