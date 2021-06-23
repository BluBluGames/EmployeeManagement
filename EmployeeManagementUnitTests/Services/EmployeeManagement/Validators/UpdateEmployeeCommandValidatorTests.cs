using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Entities;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Commands;
using EmployeeManagement.Services.EmployeeManagement.Validators;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Validators
{
    class UpdateEmployeeCommandValidatorTests
    {
        private UpdateEmployeeCommandValidator _sut;
        private Mock<IEmployeeRepository> _repositoryMock;

        [SetUp]
        public void OneTimeSetup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();

            _sut = new UpdateEmployeeCommandValidator(_repositoryMock.Object);
        }

        [Test]
        public void VerifyThatCorrectDataIsPassed()
        {
            _repositoryMock
                .Setup(r => r.GetEmployeeById(It.IsAny<int>()))
                .Returns(new Employee
                {
                    EmployeeId = 1,
                    RegistrationNumber = "00000001",
                    Pesel = "80122412456",
                    BirthDate = DateTime.Today,
                    Surname = "Stark",
                    Name = "Tony",
                    Sex = ESex.Male
                });
            _repositoryMock
                .Setup(r => r.CheckIfEmployeeExists(It.IsAny<int>()))
                .Returns(true);            
            _repositoryMock
                .Setup(r => r.CheckIfRegistrationNumberExistsOnDifferentEmployee(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);            
            _repositoryMock
                .Setup(r => r.CheckIfPeselExistsOnDifferentEmployee(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = 1,
                RegistrationNumber = "00000001",
                Pesel = "12345678912",
                BirthDate = DateTime.Now,
                Surname = "Wayne",
                Name = "Bruce",
                Sex = ESex.Male
            };

            _sut.ShouldNotHaveValidationErrorFor(r => r.EmployeeId, command.EmployeeId);
            _sut.ShouldNotHaveValidationErrorFor(r => r.Pesel, command.Pesel);
            _sut.ShouldNotHaveValidationErrorFor(r => new { r.Pesel, r.EmployeeId, r.RegistrationNumber }, command);
            _sut.ShouldNotHaveValidationErrorFor(r => r.BirthDate, command.BirthDate);
            _sut.ShouldNotHaveValidationErrorFor(r => r.Surname, command.Surname);
            _sut.ShouldNotHaveValidationErrorFor(r => r.Name, command.Name);
        }

        [Test]
        [TestCase("123456789123", "123456789123456789123456789123456789123456789123456789", "123456789123456789123456789", 8)]
        public void VerifyThatIncorrectDataIsNotPassed(string pesel, string surname, string name, ESex sex)
        {
            _repositoryMock
                .Setup(r => r.GetEmployeeById(It.IsAny<int>()))
                .Returns(new Employee
                {
                    EmployeeId = 1,
                    RegistrationNumber = "00000001",
                    Pesel = "80122412456",
                    BirthDate = DateTime.Today,
                    Surname = "Stark",
                    Name = "Tony",
                    Sex = ESex.Male
                });
            _repositoryMock
                .Setup(r => r.CheckIfEmployeeExists(It.IsAny<int>()))
                .Returns(false);
            _repositoryMock
                .Setup(r => r.CheckIfRegistrationNumberExistsOnDifferentEmployee(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(true);
            _repositoryMock
                .Setup(r => r.CheckIfPeselExistsOnDifferentEmployee(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(true);

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = 1,
                RegistrationNumber = "00000001",
                Pesel = pesel,
                BirthDate = DateTime.Now,
                Surname = surname,
                Name = name,
                Sex = sex
            };

            _sut.ShouldHaveValidationErrorFor(r => r.EmployeeId, command.EmployeeId);
            _sut.ShouldHaveValidationErrorFor(r => r.Pesel, command.Pesel);
            _sut.ShouldHaveValidationErrorFor(r => new { r.Pesel, r.EmployeeId, r.RegistrationNumber }, command);
            _sut.ShouldHaveValidationErrorFor(r => r.Surname, command.Surname);
            _sut.ShouldHaveValidationErrorFor(r => r.Name, command.Name);
        }

    }
}
