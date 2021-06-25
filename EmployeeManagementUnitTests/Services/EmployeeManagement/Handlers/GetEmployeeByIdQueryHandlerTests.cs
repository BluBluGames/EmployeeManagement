using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.V1.Employee.GetEmployee;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Domain.Employees.ValueObjects;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Handlers
{
    internal class GetEmployeeByIdQueryHandlerTests
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

            var result = await _sut.Handle(new GetEmployeeByIdQuery { Id = Guid.NewGuid() }, _cts.Token);
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
                _mapper
                    .Setup(m => m.Map<Employee, EmployeeResponse>(It.IsAny<Employee>()))
                    .Returns(new EmployeeResponse
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
            var result = await _sut.Handle(new GetEmployeeByIdQuery {Id = Guid.NewGuid()}, _cts.Token);
            MakeAssertions();

            void SetMocks()
            {
                _repositoryMock
                    .Setup(r => r.GetEmployeeByIdAsync(It.IsAny<Guid>()));
                _mapper
                    .Setup(m => m.Map<Employee, EmployeeResponse>(It.IsAny<Employee>()));
            }

            void MakeAssertions()
            {
                Assert.Null(result);
            }
        }
    }
}