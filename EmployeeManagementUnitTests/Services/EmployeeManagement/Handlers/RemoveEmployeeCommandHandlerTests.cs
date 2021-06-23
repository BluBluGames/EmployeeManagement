using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Entities;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Commands;
using EmployeeManagement.Services.EmployeeManagement.Handlers;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Handlers
{
    internal class RemoveEmployeeCommandHandlerTests
    {
        private RemoveEmployeeCommandHandler _sut;
        private Mock<IEmployeeRepository> _repositoryMock;
        private CancellationTokenSource _cts;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _sut = new RemoveEmployeeCommandHandler(_repositoryMock.Object);
            _cts = new CancellationTokenSource();
        }

        [Test]
        public async Task RemoveEmployeeById_Correct()
        {
            SetMocks();
            var result = await _sut.Handle(new RemoveEmployeeCommand {Id = It.IsAny<int>()}, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                _repositoryMock
                    .Setup(r => r.GetEmployeeByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(Task.FromResult(new Employee
                    {
                        EmployeeId = 1,
                        RegistrationNumber = "00000001",
                        Pesel = "80122412456",
                        BirthDate = DateTime.Today,
                        Surname = "Stark",
                        Name = "Tony",
                        Sex = ESex.Male
                    }).Result);
                _repositoryMock
                    .Setup(r => r.RemoveEmployeeByIdAsync(It.IsAny<Employee>()))
                    .ReturnsAsync(Task.FromResult(true).Result);
            }

            void MakeAssertions()
            {
                Assert.True(result);
            }
        }

        [Test]
        public async Task RemoveEmployeeById_GuidNotInDb()
        {
            SetMocks();
            var result = await _sut.Handle(new RemoveEmployeeCommand {Id = It.IsAny<int>()}, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                _repositoryMock
                    .Setup(r => r.GetEmployeeByIdAsync(It.IsAny<int>()));
            }

            void MakeAssertions()
            {
                Assert.False(result);
            }
        }
    }
}