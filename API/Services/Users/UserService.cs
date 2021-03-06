using API.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using API.Helpers;

namespace API.Services.Users
{

    public class UserService : BaseService
    {

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Metodo de autenticacion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, IJwtAuthManager jwtAuthManager)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            var user = await AutenticateDataBaseAsync(model);
            if (user == null)
            {
                response.Message = "User or Password Incorrect";
                return response;
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var jwtResult = jwtAuthManager.GenerateTokens(user.UserName, claims, DateTime.Now);
            response.Success = true;
            response.User = user;
            response.Token = jwtResult.AccessToken;
            return response;
        }

        /// <summary>
        /// Metodo de de busqueda de valores de usuario en base de datos.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserInfoDTO> AutenticateDataBaseAsync(AuthenticateRequest request)
        {
            var repository = UnitOfWork.AsyncRepository<User>();
            var users = await repository.ListAsync(_ => _.UserName.Equals(request.Username.ToLower()) && _.Password.Equals(request.Password));
            if (users == null)
            {
                return null;
            }
            var userDTOs = users.Select(_ => new UserInfoDTO()
            {
                Id = _.Id,
                UserName = _.UserName,
            })
            .ToList().FirstOrDefault();

            return userDTOs;
        }

        /// <summary>
        /// Obtener lista de usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserInfoDTO>> SearchAsync(GetUserRequest request)
        {
            List<User> users = new List<User>();
            var repository = UnitOfWork.AsyncRepository<User>();
            if (request.Search == null)
                users = await repository.ListAsync(null);
            else
                users = await repository.ListAsync(_ => _.UserName.Contains(request.Search));
            var userDTOs = users.Select(_ => new UserInfoDTO()
            {
                Id = _.Id,
                UserName = _.UserName
            })
            .ToList();

            return userDTOs;
        }

    }
}