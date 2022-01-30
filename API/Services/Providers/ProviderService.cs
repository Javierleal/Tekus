
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Providers;
using Domain.ProviderServices;
using System;
using API.DTOs.Providers;
using Domain.Services;
using Domain.ProviderDetails;
using API.Extensions;

namespace API.Services.Providers
{

    public class ProviderService : BaseService
    {

        #region Provider

        public ProviderService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Obtener lista de proveedores
        /// </summary>
        /// <param name="request">Parametros de busqueda y paginación</param>
        /// <returns>Objeto con datos paginados y Lista de proveedores</returns>
        public async Task<ProviderInfoDTO> SearchProvidersAsync(GetProviderRequest request)
        {
            var repository = UnitOfWork.AsyncRepository<Provider>();
            List<Provider> Providers = new List<Provider>();
            if (request.Search == null)
                Providers = await repository.ListAsync(null);
            else
                Providers = await repository.ListAsync(_ => _.Name.Contains(request.Search) || _.NIT.Contains(request.Search) || _.Email.Contains(request.Search));
            // Get's No of Rows Count   
            int count = Providers.Count();
            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
            // Returns List of Customer after applying Paging   
            Providers = Providers.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();

            var providerDTOs = new ProviderInfoDTO()
            {
                Providers = Providers,
                TotalPage = TotalPages,
                CurrentPage = request.Page,
                pageSize = request.PageSize,
            };

            return providerDTOs;
        }

        /// <summary>
        /// Agregar un nuevo proveedor
        /// </summary>
        /// <param name="request">Datos del nuevo proveedor</param>
        /// <returns>Objeto con resultado y objeto proveedor agregado</returns>
        public async Task<AddProviderInfoDTO> AddProviderAsync(AddProviderRequest request)
        {
            AddProviderInfoDTO result = new AddProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Provider>();

            Provider NewProvider = new Provider();
            NewProvider.NIT = request.NIT;
            NewProvider.Name = request.Name;
            NewProvider.Email = request.Email;
            //Validaciones
            var ValidProvider = NewProvider.Validate();
            if (ValidProvider.Count() > 0)
            {
                result.Message = ValidProvider.FirstOrDefault().ErrorMessage;
                return result;
            }
            //vitar se registre dos veces el mismo nit
            var ProvidersValid = await repository.ListAsync(_ => _.NIT.Equals(request.NIT));
            if (ProvidersValid.Count() > 0)
            {
                result.Message = "There is already a provider with the same NIT";
                return result;
            }
            var ProviderAdd = await repository.AddAsync(NewProvider);
            if (ProviderAdd == null)
                result.Message = "An error occurred while inserting provider data";
            var ProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ProviderSave == 0)
                result.Message = "An error occurred while inserting provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.Provider = NewProvider;
            }
            return result;
        }

        /// <summary>
        /// Actualizar un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Datos a actualizar del proveedor</param>
        /// <returns>Objeto con resultado y objeto proveedor actualizado</returns>
        public async Task<AddProviderInfoDTO> UpdateProviderAsync(int id, UpdateProviderRequest request)
        {
            AddProviderInfoDTO result = new AddProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Provider>();

            Provider UpdateProvider = await repository.GetAsync(_ => _.Id.Equals(id));
            if (UpdateProvider == null)
            {
                result.Message = "No provider found";
                return result;
            }
            UpdateProvider.Name = request.Name;
            UpdateProvider.Email = request.Email;
            //Validaciones
            var ValidProvider = UpdateProvider.Validate();
            if (ValidProvider.Count() > 0)
            {
                result.Message = ValidProvider.FirstOrDefault().ErrorMessage;
                return result;
            }

            var providerUpdate = await repository.UpdateAsync(UpdateProvider);
            if (providerUpdate == null)
                result.Message = "An error occurred while updating provider data";
            var ProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ProviderSave == 0)
                result.Message = "An error occurred while updating provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.Provider = UpdateProvider;
            }
            return result;
        }

        /// <summary>
        /// Eliminar un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <returns>Objeto con resultado y objeto proveedor eliminado</returns>
        public async Task<AddProviderInfoDTO> DeleteProviderAsync(int id)
        {
            AddProviderInfoDTO result = new AddProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Provider>();

            Provider DeleteProvider = await repository.GetAsync(_ => _.Id.Equals(id));
            if (DeleteProvider == null)
            {
                result.Message = "No provider found";
                return result;
            }
            var providerDelete = await repository.DeleteAsync(DeleteProvider);
            if (!providerDelete)
                result.Message = "An error occurred while deleting provider data";
            var ProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ProviderSave == 0)
                result.Message = "An error occurred while deleting provider data";
            if (result.Message == null)
            {
                result.Provider = DeleteProvider;
                result.Success = true;
            }  
            return result;
        }

        #endregion

        #region Provider Services

        /// <summary>
        /// Obtener la lista de servicios de un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Parametros de busqueda y paginación</param>
        /// <returns>Objeto con datos paginados y Lista de servicios</returns>
        internal async Task<ProviderServiceInfoDTO> GetServicesAsync(int id, GetProviderServiceRequest request)
        {
            var repositoryProvider = UnitOfWork.AsyncRepository<Provider>();
            var repositoryService = UnitOfWork.AsyncRepository<Service>();

            var repository = UnitOfWork.AsyncRepository<Domain.ProviderServices.ProviderService>();
            List<Domain.ProviderServices.ProviderService> ProviderServices = new List<Domain.ProviderServices.ProviderService>();
            if (request.Search == null)
                ProviderServices = await repository.ListAsync(_ => _.IDProvider.Equals(id));
            else
                ProviderServices = await repository.ListAsync(_ => _.CountryISO.Contains(request.Search) && _.IDProvider.Equals(id));
            // Get's No of Rows Count   
            int count = ProviderServices.Count();
            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
            // Returns List of Customer after applying Paging   
            ProviderServices = ProviderServices.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();

            List<ProviderServiceDetail> ListProviderServiceDetails = new List<ProviderServiceDetail>();
            //Esto es un proceso mas lento seria mejor un LINQ InnerJoin Quiza
            foreach (var item in ProviderServices)
            {
                var Provider = await repositoryProvider.GetAsync(_ => _.Id.Equals(item.IDProvider));
                var Service = await repositoryService.GetAsync(_ => _.Id.Equals(item.IDService));

                ListProviderServiceDetails.Add(new ProviderServiceDetail()
                {
                    Id = item.Id,
                    IDPrivider = item.IDProvider,
                    ProviderName = Provider.Name,
                    IDService = item.IDService,
                    ServiceName = Service.Name,
                    CountryISO = item.CountryISO,
                    CountryName = await item.CountryISO.CountryNameByISO()
                });
            }

            var providerDTOs = new ProviderServiceInfoDTO()
            {
                ProviderServices = ListProviderServiceDetails,
                TotalPage = TotalPages,
                CurrentPage = request.Page,
                pageSize = request.PageSize,
            };

            return providerDTOs;
        }

        /// <summary>
        /// Agregar un nuevo servicios asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Datos de servicio asociado</param>
        /// <returns>Objeto con resultado y objeto servicios a proveedor asociado</returns>
        public async Task<AddServiceProviderInfoDTO> AddServiceProviderAsync(int id, AddServiceProviderRequest request)
        {
            AddServiceProviderInfoDTO result = new AddServiceProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Domain.ProviderServices.ProviderService>();

            Domain.ProviderServices.ProviderService NewServiceProvider = new Domain.ProviderServices.ProviderService();
            NewServiceProvider.IDProvider = id;
            NewServiceProvider.IDService = request.IDService;
            NewServiceProvider.PriceHour = request.PriceHour;
            NewServiceProvider.CountryISO = request.CountryISO;
            //Validaciones
            var ValidProvider = NewServiceProvider.Validate();
            if (ValidProvider.Count() > 0)
            {
                result.Message = ValidProvider.FirstOrDefault().ErrorMessage;
                return result;
            }
            //vitar se registre dos veces el mismo nit
            var ProvidersValid = await repository.ListAsync(_ => _.IDProvider.Equals(id) && _.IDService.Equals(request.IDService) && _.CountryISO.Equals(request.CountryISO));
            if (ProvidersValid.Count() > 0)
            {
                result.Message = "The service is already associated with this provider and country";
                return result;
            }
            var ProviderAdd = await repository.AddAsync(NewServiceProvider);
            if (ProviderAdd == null)
                result.Message = "An error occurred while inserting provider data";
            var ProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ProviderSave == 0)
                result.Message = "An error occurred while inserting provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.ServiceProvider = NewServiceProvider;
            }
            return result;
        }

        /// <summary>
        /// Actualizar servicios asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del servicio asociado al proveedor</param>
        /// <param name="request">Datos de servicio asociado</param>
        /// <returns>Objeto con resultado y objeto servicios a proveedor asociado</returns>
        public async Task<UpdateServiceProviderInfoDTO> UpdateServiceProviderAsync(int id, UpdateServiceProviderRequest request)
        {
            UpdateServiceProviderInfoDTO result = new UpdateServiceProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Domain.ProviderServices.ProviderService>();

            Domain.ProviderServices.ProviderService UpdateServiceProvider = await repository.GetAsync(_ => _.Id.Equals(id));
            if (UpdateServiceProvider == null)
            {
                result.Message = "No services associated with provider were found";
                return result;
            }
            UpdateServiceProvider.PriceHour = request.PriceHour;
            UpdateServiceProvider.CountryISO = request.CountryISO;
            //Validaciones
            var ValidProvider = UpdateServiceProvider.Validate();
            if (ValidProvider.Count() > 0)
            {
                result.Message = ValidProvider.FirstOrDefault().ErrorMessage;
                return result;
            }
            var ProvidersValid = await repository.ListAsync(_ => _.Id != id && _.IDProvider.Equals(UpdateServiceProvider.IDProvider) && _.IDService.Equals(UpdateServiceProvider.IDService) && _.CountryISO.Equals(request.CountryISO));
            if (ProvidersValid.Count() > 0)
            {
                result.Message = "The service is already associated with this provider and country";
                return result;
            }
            var ProviderUpdate = await repository.UpdateAsync(UpdateServiceProvider);
            if (ProviderUpdate == null)
                result.Message = "An error occurred while updating provider data";
            var ProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ProviderSave == 0)
                result.Message = "An error occurred while updating provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.ServiceProvider = UpdateServiceProvider;
            }
            return result;
        }

        /// <summary>
        /// Eliminar servicios asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <returns>Objeto con resultado y objeto servicios a proveedor asociado</returns>
        public async Task<UpdateServiceProviderInfoDTO> DeleteServiceProviderAsync(int id)
        {
            UpdateServiceProviderInfoDTO result = new UpdateServiceProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Domain.ProviderServices.ProviderService>();

            Domain.ProviderServices.ProviderService DeleteServiceProvider = await repository.GetAsync(_ => _.Id.Equals(id));
            if (DeleteServiceProvider == null)
            {
                result.Message = "No services associated with provider were found";
                return result;
            }
            //Validaciones
            var ServiceProviderDelete = await repository.DeleteAsync(DeleteServiceProvider);
            if (!ServiceProviderDelete)
                result.Message = "An error occurred while deleting provider data";
            var ServiceProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ServiceProviderSave == 0)
                result.Message = "An error occurred while deleting provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.ServiceProvider = DeleteServiceProvider;
            }
            return result;
        }

        #endregion

        #region Provider Detail

        /// <summary>
        /// Obtener la lista de detalle de un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Parametros de busqueda y paginación</param>
        /// <returns>Objeto con datos paginados y Lista de servicios</returns>
        internal async Task<ProviderDetailInfoDTO> GetDetailsAsync(int id, GetProviderDetailsRequest request)
        {
            var repository = UnitOfWork.AsyncRepository<ProviderDetail>();

            List<ProviderDetail> ProviderDetails = new List<ProviderDetail>();
            if (request.Search == null)
                ProviderDetails = await repository.ListAsync(_ => _.IDProvider.Equals(id));
            else
                ProviderDetails = await repository.ListAsync(_ => _.RowName.Contains(request.Search) && _.IDProvider.Equals(id));
            // Get's No of Rows Count   
            int count = ProviderDetails.Count();
            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
            // Returns List of Customer after applying Paging   
            ProviderDetails = ProviderDetails.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();

            var providerdetailDTOs = new ProviderDetailInfoDTO()
            {
                ProviderDetails = ProviderDetails,
                TotalPage = TotalPages,
                CurrentPage = request.Page,
                pageSize = request.PageSize,
            };

            return providerdetailDTOs;
        }

        /// <summary>
        /// Agregar un nuevo detalle asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Datos de detalle asociado</param>
        /// <returns>Objeto con resultado y objeto detalle a proveedor</returns>
        public async Task<AddDetailProviderInfoDTO> AddDetailProviderAsync(int id, AddDetailProviderRequest request)
        {
            AddDetailProviderInfoDTO result = new AddDetailProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<ProviderDetail>();

            ProviderDetail NewDetailProvider = new ProviderDetail();
            NewDetailProvider.IDProvider = id;
            NewDetailProvider.RowName = request.RowName;
            NewDetailProvider.RowValue = request.RowValue;
            //Validaciones
            var ValidProvider = NewDetailProvider.Validate();
            if (ValidProvider.Count() > 0)
            {
                result.Message = ValidProvider.FirstOrDefault().ErrorMessage;
                return result;
            }
            //vitar se registre dos veces el mismo nit
            var ProvidersValid = await repository.ListAsync(_ => _.IDProvider.Equals(id) && _.RowName.Equals(request.RowName));
            if (ProvidersValid.Count() > 0)
            {
                result.Message = String.Format("There is already a detail field called {0} associated with this provider", request.RowName);
                return result;
            }
            var ProviderAdd = await repository.AddAsync(NewDetailProvider);
            if (ProviderAdd == null)
                result.Message = "An error occurred while inserting provider data";
            var ProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ProviderSave == 0)
                result.Message = "An error occurred while inserting provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.ServiceDetail = NewDetailProvider;
            }
            return result;
        }

        /// <summary>
        /// Actualiza un detalle asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del detalle asociado a proveedor</param>
        /// <param name="request">Datos de detalle asociado</param>
        /// <returns>Objeto con resultado y objeto detalle a proveedor</returns>
        public async Task<UpdateDetailProviderInfoDTO> UpdateDetailProviderAsync(int id, UpdateDetailProviderRequest request)
        {
            UpdateDetailProviderInfoDTO result = new UpdateDetailProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<ProviderDetail>();

            ProviderDetail UpdateDetailProvider = await repository.GetAsync(_ => _.Id.Equals(id));
            if (UpdateDetailProvider == null)
            {
                result.Message = "No detail associated with supplier was found";
                return result;
            }
            UpdateDetailProvider.RowValue = request.RowValue;
            //Validaciones
            var ValidProvider = UpdateDetailProvider.Validate();
            if (ValidProvider.Count() > 0)
            {
                result.Message = ValidProvider.FirstOrDefault().ErrorMessage;
                return result;
            }
            var ProviderUpdate = await repository.UpdateAsync(UpdateDetailProvider);
            if (ProviderUpdate == null)
                result.Message = "An error occurred while updating provider data";
            var ProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ProviderSave == 0)
                result.Message = "An error occurred while updating provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.DetailProvider = UpdateDetailProvider;
            }
            return result;
        }

        /// <summary>
        /// Eliminar detalle asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del detalle asociado a proveedor</param>
        /// <returns>Objeto con resultado y objeto detalle a proveedor asociado</returns>
        public async Task<UpdateDetailProviderInfoDTO> DeleteDetailProviderAsync(int id)
        {
            UpdateDetailProviderInfoDTO result = new UpdateDetailProviderInfoDTO();
            var repository = UnitOfWork.AsyncRepository<ProviderDetail>();

            ProviderDetail DeleteDetailProvider = await repository.GetAsync(_ => _.Id.Equals(id));
            if (DeleteDetailProvider == null)
            {
                result.Message = "No detail associated with supplier was found";
                return result;
            }
            //Validaciones
            var ServiceProviderDelete = await repository.DeleteAsync(DeleteDetailProvider);
            if (!ServiceProviderDelete)
                result.Message = "An error occurred while deleting provider data";
            var ServiceProviderSave = await UnitOfWork.SaveChangesAsync();
            if (ServiceProviderSave == 0)
                result.Message = "An error occurred while deleting provider data";
            if (result.Message == null)
            {
                result.Success = true;
                result.DetailProvider = DeleteDetailProvider;
            }
            return result;
        }

        #endregion
    }
}