using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Queries;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Domain.Employees.ValueObjects;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Handlers;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Handlers
{
    internal class GetAllEmployeesQueryHandlerTests
    {
        private GetAllEmployeesQueryHandler _sut;
        private Mock<IEmployeeRepository> _repositoryMock;
        private Mock<IMapper> _mapper;
        private CancellationTokenSource _cts;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _mapper = new Mock<IMapper>();
            _sut = new GetAllEmployeesQueryHandler(_repositoryMock.Object, _mapper.Object);
            _cts = new CancellationTokenSource();

            _repositoryMock
                .Setup(r => r.GetAllEmployeesAsync())
                .ReturnsAsync(Task.FromResult<IEnumerable<Employee>>(new List<Employee>
                {
                    new()
                    {
                        EmployeeId = Guid.NewGuid(),
                        RegistrationNumber = EmployeeRegistrationNumber.From("00000001"),
                        Pesel = EmployeePesel.From("80122412456"),
                        BirthDate = EmployeeBirthDate.From(DateTime.Today),
                        Surname = EmployeeSurname.From("Stark"),
                        Name = EmployeeName.From("Tony"),
                        Sex = EmployeeSex.From(ESex.Male)
                    },
                    new()
                    {
                        EmployeeId = Guid.NewGuid(),
                        RegistrationNumber = EmployeeRegistrationNumber.From("00000002"),
                        Pesel = EmployeePesel.From("90122412456"),
                        BirthDate = EmployeeBirthDate.From(DateTime.Today),
                        Surname = EmployeeSurname.From("Romanoff"),
                        Name = EmployeeName.From("Natasha"),
                        Sex = EmployeeSex.From(ESex.Female)
                    }
                }).Result);

            _mapper
                .Setup(m => m.Map<IEnumerable<Employee>, List<EmployeeResponse>>(It.IsAny<List<Employee>>()))
                .Returns(new List<EmployeeResponse>
                {
                    new()
                    {
                        RegistrationNumber = "00000001",
                        Pesel = "80122412456",
                        BirthDate = DateTime.Today,
                        Surname = "Stark",
                        Name = "Tony"
                    },
                    new()
                    {
                        RegistrationNumber = "00000002",
                        Pesel = "90122412456",
                        BirthDate = DateTime.Today,
                        Surname = "Romanoff",
                        Name = "Natasha",
                        Sex = ESex.Female
                    }
                });
        }

        [Test]
        public async Task GetAllEmployeesTest_Correct()
        {
            var results = await _sut.Handle(new GetAllEmployeesQuery(), _cts.Token);
            var expectedValues = SetExpectedValues();
            for (var i = 0; i < results.Count; i++)
            {
                Assert.AreEqual(results[i].Name, expectedValues[i].Name);
                Assert.AreEqual(results[i].Surname, expectedValues[i].Surname);
                Assert.AreEqual(results[i].Pesel, expectedValues[i].Pesel);
                Assert.AreEqual(results[i].RegistrationNumber, expectedValues[i].RegistrationNumber);
            }
        }

        private List<EmployeeResponse> SetExpectedValues()
        {
            return new()
            {
                new EmployeeResponse
                {
                    RegistrationNumber = "00000001",
                    Pesel = "80122412456",
                    BirthDate = DateTime.Today,
                    Surname = "Stark",
                    Name = "Tony"
                },
                new EmployeeResponse()
                {
                    RegistrationNumber = "00000002",
                    Pesel = "90122412456",
                    BirthDate = DateTime.Today,
                    Surname = "Romanoff",
                    Name = "Natasha",
                    Sex = ESex.Female
                }
            };
        }
    }
}