using API.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using API.Helpers;
using Infrastructure.Data;
using Domain.Providers;
using API.DTOs.Dashboard;
using Domain.Services;
using Domain.ProviderServices;
using Domain.ProviderDetails;

namespace API.Services.Dashboard
{

    public class DashboardService : BaseService
    {

        public DashboardService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        /// <summary>
        /// Obtener Datos de dashboard basico
        /// </summary>
        /// <returns></returns>
        public async Task<DashBoardResult> DashboardAsync()
        {
            var repositoryProvider = UnitOfWork.AsyncRepository<Provider>();
            var repositoryService = UnitOfWork.AsyncRepository<Service>();
            var repositoryProviderService = UnitOfWork.AsyncRepository<ProviderService>();
            var repositoryProviderDetails = UnitOfWork.AsyncRepository<ProviderDetail>();
            DashBoardResult Result = new DashBoardResult();
            
            Result.ProviderCount = repositoryProvider.ListAsync(null).Result.Count();
            Result.ServiceCount = repositoryService.ListAsync(null).Result.Count();
            Result.ProviderServiceCount = repositoryProviderService.ListAsync(null).Result.Count();
            Result.ProviderDetailCount = repositoryProviderDetails.ListAsync(null).Result.Count();
            return Result;
        }

    }
}