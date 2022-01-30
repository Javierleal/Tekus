using API.DTOs.Services;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Services
{

    public class ServiceService : BaseService
    {

        public ServiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Obtener lista de servicios
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ServiceInfoDTO> SearchAsync(GetServiceRequest request)
        {
            var repository = UnitOfWork.AsyncRepository<Service>();
            List<Service> Services = new List<Service>();
            if (request.Search == null)
                Services = await repository.ListAsync(null);
            else
                Services = await repository.ListAsync(_ => _.Name.Contains(request.Search));
            // Get's No of Rows Count   
            int count = Services.Count();
            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
            // Returns List of Customer after applying Paging   
            Services = Services.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();

            var ServiceDTOs = new ServiceInfoDTO()
            {
                Services = Services,
                TotalPage = TotalPages,
                CurrentPage = request.Page,
                pageSize = request.PageSize,
            };

            return ServiceDTOs;
        }

        /// <summary>
        /// Agregar un nuevo Service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AddServiceInfoDTO> AddServiceAsync(AddServiceRequest request)
        {
            AddServiceInfoDTO result = new AddServiceInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Service>();

            Service NewService = new Service();
            NewService.Name = request.Name;
            NewService.Description = request.Description;
            //Validaciones
            var ValidService = NewService.Validate();
            if (ValidService.Count() > 0)
            {
                result.Message = ValidService.FirstOrDefault().ErrorMessage;
                return result;
            }
            var ServiceAdd = await repository.AddAsync(NewService);
            if (ServiceAdd == null)
                result.Message = "An error occurred while inserting Service data";
            var ServiceSave = await UnitOfWork.SaveChangesAsync();
            if (ServiceSave == 0)
                result.Message = "An error occurred while inserting Service data";
            if (result.Message == null)
            {
                result.Success = true;
                result.Service = NewService;
            }
            return result;
        }

        /// <summary>
        /// Actualiza un Service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AddServiceInfoDTO> UpdateServiceAsync(int id, UpdateServiceRequest request)
        {
            AddServiceInfoDTO result = new AddServiceInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Service>();

            Service UpdateService = await repository.GetAsync(_ => _.Id.Equals(id));
            if (UpdateService == null)
            {
                result.Message = "no service found";
                return result;
            }
            UpdateService.Name = request.Name;
            UpdateService.Description = request.Description;
            //Validaciones
            var ValidService = UpdateService.Validate();
            if (ValidService.Count() > 0)
            {
                result.Message = ValidService.FirstOrDefault().ErrorMessage;
                return result;
            }

            var ServiceUpdate = await repository.UpdateAsync(UpdateService);
            if (ServiceUpdate == null)
                result.Message = "An error occurred while updating Service data";
            var ServiceSave = await UnitOfWork.SaveChangesAsync();
            if (ServiceSave == 0)
                result.Message = "An error occurred while updating Service data";
            if (result.Message == null)
            {
                result.Success = true;
                result.Service = UpdateService;
            }
            return result;
        }

        /// <summary>
        /// Elimina un Service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AddServiceInfoDTO> DeleteServiceAsync(int id)
        {
            AddServiceInfoDTO result = new AddServiceInfoDTO();
            var repository = UnitOfWork.AsyncRepository<Service>();

            Service DeleteService = await repository.GetAsync(_ => _.Id.Equals(id));
            if (DeleteService == null)
            {
                result.Message = "no service found";
                return result;
            }
            var ServiceDelete = await repository.DeleteAsync(DeleteService);
            if (!ServiceDelete)
                result.Message = "An error occurred while deleting Service data";
            var ServiceSave = await UnitOfWork.SaveChangesAsync();
            if (ServiceSave == 0)
                result.Message = "An error occurred while deleting Service data";
            if (result.Message == null)
            {
                result.Service = DeleteService;
                result.Success = true;
            }
            return result;
        }
    }
}