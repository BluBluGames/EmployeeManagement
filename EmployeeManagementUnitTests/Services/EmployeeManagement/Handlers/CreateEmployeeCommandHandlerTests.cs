using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Commands;
using EmployeeManagement.Services.EmployeeManagement.Handlers;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Handlers
{
    internal class CreateEmployeeCommandHandlerTests
    {
        private CreateEmployeeCommandHandler _sut;
        private Mock<IEmployeeRepository> _repositoryMock;
        private Mock<IMapper> _mapper;
        private CancellationTokenSource _cts;

        [SetUp]
        public void OneTimeSetup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _mapper = new Mock<IMapper>();
            _sut = new CreateEmployeeCommandHandler(_repositoryMock.Object, _mapper.Object);
            _cts = new CancellationTokenSource();
        }

        [Test]
        [TestCase("Banner", "Bruce", "00000005", "99110111111", ESex.Male)]
        public async Task UpdateEmployeeTest_Correct(string expSurname, string expName, string expRegistrationNumber,
            string expPesel, ESex expSex)
        {
            CreateEmployeeCommand createCommand;
            SetMocks();
            var result = await _sut.Handle(createCommand, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                createCommand = new CreateEmployeeCommand
                {
                    Pesel = "199110111111",
                    BirthDate = DateTime.Today,
                    Surname = "Banner",
                    Name = "Bruce",
                    Sex = ESex.Male
                };

                _repositoryMock
                    .Setup(r => r.GetAllRegistrationNumbersAsync())
                    .ReturnsAsync(Task.FromResult(new List<string>
                    {
                        "00000001", "00000002", "00000003", "00000004"
                    }).Result);

                _repositoryMock
                    .Setup(r => r.CreateEmployeeAsync(It.IsAny<Employee>()))
                    .ReturnsAsync(Task.FromResult(new Employee
                    {
                        EmployeeId = 1,
                        RegistrationNumber = "00000005",
                        Pesel = "99110111111",
                        BirthDate = DateTime.Today,
                        Surname = "Banner",
                        Name = "Bruce",
                        Sex = ESex.Male
                    }).Result);

                _mapper
                    .Setup(m => m.Map<CreateEmployeeCommand, Employee>(It.IsAny<CreateEmployeeCommand>()))
                    .Returns(new Employee
                    {
                        EmployeeId = 0,
                        RegistrationNumber = "00000000",
                        Pesel = "99110111111",
                        BirthDate = DateTime.Today,
                        Surname = "Banner",
                        Name = "Bruce",
                        Sex = ESex.Male
                    });

                _mapper
                    .Setup(m => m.Map<Employee, EmployeeModel>(It.IsAny<Employee>()))
                    .Returns(new EmployeeModel
                    {
                        EmployeeId = 1,
                        RegistrationNumber = "00000005",
                        Pesel = "99110111111",
                        BirthDate = DateTime.Today,
                        Surname = "Banner",
                        Name = "Bruce",
                        Sex = ESex.Male
                    });
            }

            void MakeAssertions()
            {
                Assert.AreEqual(result.Name, expName);
                Assert.AreEqual(result.Surname, expSurname);
                Assert.AreEqual(result.Pesel, expPesel);
                Assert.AreEqual(result.RegistrationNumber, expRegistrationNumber);
                Assert.AreEqual(result.Sex, expSex);
            }
        }

        [Test]
        [TestCase("Banner", "Bruce", "00000001", "99110111111", ESex.Male)]
        public async Task UpdateEmployeeTest_Correct2(string expSurname, string expName, string expRegistrationNumber,
            string expPesel, ESex expSex)
        {
            CreateEmployeeCommand createCommand;
            SetMocks();
            var result = await _sut.Handle(createCommand, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                createCommand = new CreateEmployeeCommand
                {
                    Pesel = "199110111111",
                    BirthDate = DateTime.Today,
                    Surname = "Banner",
                    Name = "Bruce",
                    Sex = ESex.Male
                };

                _repositoryMock
                    .Setup(r => r.GetAllRegistrationNumbersAsync())
                    .ReturnsAsync(Task.FromResult(new List<string>()).Result);

                _repositoryMock
                    .Setup(r => r.CreateEmployeeAsync(It.IsAny<Employee>()))
                    .ReturnsAsync(Task.FromResult(new Employee
                    {
                        EmployeeId = 1,
                        RegistrationNumber = "00000001",
                        Pesel = "99110111111",
                        BirthDate = DateTime.Today,
                        Surname = "Banner",
                        Name = "Bruce",
                        Sex = ESex.Male
                    }).Result);

                _mapper
                    .Setup(m => m.Map<CreateEmployeeCommand, Employee>(It.IsAny<CreateEmployeeCommand>()))
                    .Returns(new Employee
                    {
                        EmployeeId = 0,
                        RegistrationNumber = "00000000",
                        Pesel = "99110111111",
                        BirthDate = DateTime.Today,
                        Surname = "Banner",
                        Name = "Bruce",
                        Sex = ESex.Male
                    });

                _mapper
                    .Setup(m => m.Map<Employee, EmployeeModel>(It.IsAny<Employee>()))
                    .Returns(new EmployeeModel
                    {
                        EmployeeId = 1,
                        RegistrationNumber = "00000001",
                        Pesel = "99110111111",
                        BirthDate = DateTime.Today,
                        Surname = "Banner",
                        Name = "Bruce",
                        Sex = ESex.Male
                    });
            }

            void MakeAssertions()
            {
                Assert.AreEqual(result.Name, expName);
                Assert.AreEqual(result.Surname, expSurname);
                Assert.AreEqual(result.Pesel, expPesel);
                Assert.AreEqual(result.RegistrationNumber, expRegistrationNumber);
                Assert.AreEqual(result.Sex, expSex);
            }
        }
    }
}