using Application.Commands;
using Application.Exceptions;
using Application.Helpers;
using Application.Responses;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class RegistrateHandler : IRequestHandler<RegistrateCommand, AuthenticateResponse>
    {
        private readonly IUserRepository _userRepo;

        private readonly ITokenService _tokenService;

        public RegistrateHandler(IUserRepository userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponse> Handle(RegistrateCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepo.GetByUsername(request.Username) != null)
                throw new UserException("User with such username is already exists.");

            var hashedPassword = HashPassword(request.Password);
            var user = await _userRepo.AddAsync(new User
            {
                Password = hashedPassword,
                Username = request.Username,
            });

            var token = _tokenService.GenerateJwtToken(user);
            var response = new AuthenticateResponse(user, token);
            return response;
        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new UserException("Password can't be null");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
