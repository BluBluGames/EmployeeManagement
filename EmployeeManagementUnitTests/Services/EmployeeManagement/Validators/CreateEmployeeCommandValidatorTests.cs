using System;
using EmployeeManagement.Application.V1.Employee.CreateEmployee;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Repositories;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Validators
{
    internal class CreateEmployeeCommandValidatorTests
    {
        private CreateEmployeeCommandValidator _sut;
        private Mock<IEmployeeRepository> _repositoryMock;

        [SetUp]
        public void OneTimeSetup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();

            _sut = new CreateEmployeeCommandValidator(_repositoryMock.Object);
        }

        [Test]
        public void VerifyThatCorrectDataIsPassed()
        {
            _repositoryMock
                .Setup(r => r.CheckIfPeselExistsInDb(It.IsAny<string>()))
                .Returns(false);

            var command = new CreateEmployeeCommand
            {
                Pesel = "12345678912",
                BirthDate = DateTime.Now,
                Surname = "Wayne",
                Name = "Bruce",
                Sex = ESex.Male
            };

            _sut.ShouldNotHaveValidationErrorFor(r => r.Pesel, command.Pesel);
            _sut.ShouldNotHaveValidationErrorFor(r => r.BirthDate, command.BirthDate);
            _sut.ShouldNotHaveValidationErrorFor(r => r.Surname, command.Surname);
            _sut.ShouldNotHaveValidationErrorFor(r => r.Name, command.Name);
        }

        [Test]
        [TestCase("123456789123", "123456789123456789123456789123456789123456789123456789",
            "123456789123456789123456789", 8)]
        public void VerifyThatIncorrectDataIsNotPassed(string pesel, string surname, string name, ESex sex)
        {
            _repositoryMock
                .Setup(r => r.CheckIfPeselExistsInDb(It.IsAny<string>()))
                .Returns(true);

            var command = new CreateEmployeeCommand
            {
                Pesel = pesel,
                BirthDate = DateTime.Now,
                Surname = surname,
                Name = name,
                Sex = sex
            };

            _sut.ShouldHaveValidationErrorFor(r => r.Pesel, command.Pesel);
            _sut.ShouldHaveValidationErrorFor(r => r.Surname, command.Surname);
            _sut.ShouldHaveValidationErrorFor(r => r.Name, command.Name);
        }
    }
}