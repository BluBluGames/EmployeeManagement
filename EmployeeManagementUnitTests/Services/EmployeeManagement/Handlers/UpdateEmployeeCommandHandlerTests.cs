using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class UpdateEmployeeCommandHandlerTests
    {
        private UpdateEmployeeCommandHandler _sut;
        private Mock<IEmployeeRepository> _repositoryMock;
        private Mock<IMapper> _mapper;
        private CancellationTokenSource _cts;

        [SetUp]
        public void OneTimeSetup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _mapper = new Mock<IMapper>();
            _sut = new UpdateEmployeeCommandHandler(_repositoryMock.Object, _mapper.Object);
            _cts = new CancellationTokenSource();
        }

        [Test]
        [TestCase("Banner", "Bruce", "00000001", "99110111111", ESex.Male)]
        public async Task UpdateEmployeeTest_Correct(string expSurname, string expName, string expRegistrationNumber, string expPesel, ESex expSex)
        {
            UpdateEmployeeCommand updateCommand;
            SetMocks();
            var result = await _sut.Handle(updateCommand, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                updateCommand = new UpdateEmployeeCommand
                {
                    EmployeeId = 1,
                    RegistrationNumber = "00000001",
                    Pesel = "199110111111",
                    BirthDate = DateTime.Now,
                    Surname = "Banner",
                    Name = "Bruce",
                    Sex = ESex.Male
                };

                _repositoryMock
                    .Setup(r => r.UpdateEmployee(It.IsAny<Employee>()))
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
                    .Setup(m => m.Map<Employee, EmployeeModel>(It.IsAny<Employee>()))
                    .Returns(new EmployeeModel
                    {
                        EmployeeId = 2,
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
        [Test]
        public async Task UpdateEmployeeTest_Correct_EmployeeDoesntExist()
        {
            UpdateEmployeeCommand updateCommand;
            SetMocks();
            var result = await _sut.Handle(updateCommand, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                updateCommand = new UpdateEmployeeCommand
                {
                    EmployeeId = 1,
                    RegistrationNumber = "00000001",
                    Pesel = "199110111111",
                    BirthDate = DateTime.Now,
                    Surname = "Banner",
                    Name = "Bruce",
                    Sex = ESex.Male
                };
            }
            void MakeAssertions()
            {
                Assert.Null(result);
            }
        }

    }
}
