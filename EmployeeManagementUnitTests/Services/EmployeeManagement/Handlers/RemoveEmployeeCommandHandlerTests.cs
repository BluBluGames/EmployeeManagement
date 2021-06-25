using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Application.V1.Employee.RemoveEmployee;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Domain.Employees.ValueObjects;
using EmployeeManagement.Repositories;
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
            var result = await _sut.Handle(new RemoveEmployeeCommand {Id = It.IsAny<Guid>()}, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                _repositoryMock
                    .Setup(r => r.GetEmployeeByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(Task.FromResult(new Employee
                    {
                        EmployeeId = Guid.NewGuid(),
                        RegistrationNumber = EmployeeRegistrationNumber.From("00000001"),
                        Pesel = EmployeePesel.From("80122412456"),
                        BirthDate = EmployeeBirthDate.From(DateTime.Today),
                        Surname = EmployeeSurname.From("Stark"),
                        Name = EmployeeName.From("Tony"),
                        Sex = EmployeeSex.From(ESex.Male)
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
            var result = await _sut.Handle(new RemoveEmployeeCommand {Id = It.IsAny<Guid>()}, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                _repositoryMock
                    .Setup(r => r.GetEmployeeByIdAsync(It.IsAny<Guid>()));
            }

            void MakeAssertions()
            {
                Assert.False(result);
            }
        }
    }
}