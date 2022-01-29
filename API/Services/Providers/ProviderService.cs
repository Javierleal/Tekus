using API.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using API.Helpers;
using Domain.Providers;

namespace API.Services.Providers
{

    public class ProviderService : BaseService
    {

        public ProviderService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        /// <summary>
        /// Obtener lista de proveedores
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProviderInfoDTO>> SearchAsync(GetProviderRequest request)
        {
            var repository = UnitOfWork.AsyncRepository<Provider>();
            var users = await repository
                .ListAsync(_ => _.Name.Contains(request.Search));

            var providerDTOs = users.Select(_ => new ProviderInfoDTO()
            {
                Id = _.Id,
                NIT = _.NIT,
                Name = _.Name,
                Email = _.Email
            })
            .ToList();

            return providerDTOs;
        }


        /// <summary>
        /// Agregar un nuevo proveedor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AddProviderInfoDTO> AddProviderAsync(AddProviderRequest request)
        {
            AddProviderInfoDTO result = new AddProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Provider>();

            Provider NewProvider = new Provider();
            NewProvider.NIT = request.NIT;
            NewProvider.Name = request.Name;
            NewProvider.Email = request.Email;
            //Validaciones
            var ValidData = NewProvider.Validate();
            if (ValidData.Count() > 0)
                result.Message = ValidData.FirstOrDefault().ErrorMessage;
            var users = await repository.AddAsync(NewProvider);
            if (users == null)
                result.Message = "An error occurred while inserting provider data";
            var Save = await UnitOfWork.SaveChangesAsync();
            if (Save == 0)
                result.Message = "An error occurred while inserting provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.Provider = NewProvider;
            }
            return result;
        }

    }
}