﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Handlers;
using EmployeeManagement.Services.EmployeeManagement.Queries;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Handlers
{
    class GetEmployeeByIdQueryHandlerTests
    {
        private GetEmployeeByIdQueryHandler _sut;
        private Mock<IEmployeeRepository> _repositoryMock;
        private Mock<IMapper> _mapper;
        private CancellationTokenSource _cts;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _mapper = new Mock<IMapper>();
            _sut = new GetEmployeeByIdQueryHandler(_repositoryMock.Object, _mapper.Object);
            _cts = new CancellationTokenSource();
        }

        [Test]
        [TestCase("Stark", "Tony", "00000001", "80122412456", ESex.Male)]
        public async Task GetEmployeeByIdTest_Correct(string expSurname, string expName, string expRegistrationNumber,
            string expPesel, ESex expSex)
        {
            SetMocks();

            var result = await _sut.Handle(new GetEmployeeByIdQuery(It.IsAny<int>()), _cts.Token);
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
                _mapper
                    .Setup(m => m.Map<Employee, EmployeeModel>(It.IsAny<Employee>()))
                    .Returns(new EmployeeModel
                    {
                        RegistrationNumber = "00000001",
                        Pesel = "80122412456",
                        BirthDate = DateTime.Today,
                        Surname = "Stark",
                        Name = "Tony",
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
        public async Task GetEmployeeByIdTest_GuidNotInDb()
        {
            SetMocks();
            var result = await _sut.Handle(new GetEmployeeByIdQuery(It.IsAny<int>()), _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                _repositoryMock
                    .Setup(r => r.GetEmployeeByIdAsync(It.IsAny<int>()));
                _mapper
                    .Setup(m => m.Map<Employee, EmployeeModel>(It.IsAny<Employee>()));
            }

            void MakeAssertions()
            {
                Assert.Null(result);
            }
        }
    }
}
