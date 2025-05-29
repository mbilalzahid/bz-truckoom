using backend.Domain.Models;
using backend.Application.Interfaces;
using backend.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace backend.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IGenericRepository<Service> _serviceRepository;

        public ServiceService(IGenericRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            // Pass expression for Tasks navigation property
            return await _serviceRepository.GetAllAsync(s => s.Tasks);
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _serviceRepository.GetByIdAsync(serviceId, s => s.Tasks);
        }

        public async System.Threading.Tasks.Task AddServiceAsync(Service service)
        {
            await _serviceRepository.AddAsync(service);
        }


        public async System.Threading.Tasks.Task UpdateServiceAsync(Service service)
        {
            await _serviceRepository.UpdateAsync(service);
        }

        public async System.Threading.Tasks.Task DeleteServiceAsync(int serviceId)
        {
            var service = await _serviceRepository.GetByIdAsync(serviceId, s => s.Tasks);
            if (service != null)
            {
                await _serviceRepository.DeleteAsync(service);
            }
        }


    }
}
