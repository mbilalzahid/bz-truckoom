using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domain.Models;
using backend.Application.Interfaces.Repositories;
using backend.Application.Services;
using System.Linq;

namespace backend.Test
{
    [TestClass]
    public class ServiceServiceTests
    {
        private Mock<IGenericRepository<Service>> _mockRepo;
        private ServiceService _serviceService;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IGenericRepository<Service>>();
            _serviceService = new ServiceService(_mockRepo.Object);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetAllServicesAsync_ReturnsAllServices()
        {
            // Arrange
            var mockServices = new List<Service>
            {
                new Service { ServiceId = 1, ServiceName = "Oil Change", ServiceDate = DateTime.Now },
                new Service { ServiceId = 2, ServiceName = "Tire Rotation", ServiceDate = DateTime.Now }
            };

            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Service, object>>[]>()))
                     .ReturnsAsync(mockServices);

            // Act
            var result = await _serviceService.GetAllServicesAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetServiceByIdAsync_ReturnsCorrectService()
        {
            // Arrange
            var mockService = new Service { ServiceId = 1, ServiceName = "Brake Inspection", ServiceDate = DateTime.Now };

            _mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<System.Linq.Expressions.Expression<Func<Service, object>>[]>()))
                     .ReturnsAsync(mockService);

            // Act
            var result = await _serviceService.GetServiceByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Brake Inspection", result.ServiceName);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task AddServiceAsync_CallsRepositoryAdd()
        {
            // Arrange
            var newService = new Service { ServiceId = 3, ServiceName = "Engine Check", ServiceDate = DateTime.Now };

            _mockRepo.Setup(r => r.AddAsync(newService)).Returns(System.Threading.Tasks.Task.CompletedTask);

            // Act
            await _serviceService.AddServiceAsync(newService);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(newService), Times.Once);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task UpdateServiceAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var serviceToUpdate = new Service { ServiceId = 1, ServiceName = "Updated Name", ServiceDate = DateTime.Now };

            _mockRepo.Setup(r => r.UpdateAsync(serviceToUpdate)).Returns(System.Threading.Tasks.Task.CompletedTask);

            // Act
            await _serviceService.UpdateServiceAsync(serviceToUpdate);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(serviceToUpdate), Times.Once);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteServiceAsync_DeletesServiceIfExists()
        {
            // Arrange
            var existingService = new Service { ServiceId = 1, ServiceName = "Test", ServiceDate = DateTime.Now };

            _mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<System.Linq.Expressions.Expression<Func<Service, object>>[]>()))
                     .ReturnsAsync(existingService);
            _mockRepo.Setup(r => r.DeleteAsync(existingService)).Returns(System.Threading.Tasks.Task.CompletedTask);

            // Act
            await _serviceService.DeleteServiceAsync(1);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(existingService), Times.Once);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteServiceAsync_DoesNothingIfServiceNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(99, It.IsAny<System.Linq.Expressions.Expression<Func<Service, object>>[]>()))
                     .ReturnsAsync((Service?)null);

            // Act
            await _serviceService.DeleteServiceAsync(99);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Service>()), Times.Never);
        }
    }
}
