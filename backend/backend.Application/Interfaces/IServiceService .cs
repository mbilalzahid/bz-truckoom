using System.Collections.Generic;
using backend.Domain.Models;

namespace backend.Application.Interfaces
{
    public interface IServiceService
    {
        System.Threading.Tasks.Task<IEnumerable<Service>> GetAllServicesAsync();
        System.Threading.Tasks.Task<Service> GetServiceByIdAsync(int serviceId);
        System.Threading.Tasks.Task AddServiceAsync(Service service);
        System.Threading.Tasks.Task UpdateServiceAsync(Service service);
        System.Threading.Tasks.Task DeleteServiceAsync(int serviceId);
    }
}
