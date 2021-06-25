using System;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Commands;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Validators;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Domain.Employees.ValueObjects;
using EmployeeManagement.Repositories;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Validators
{
    internal class UpdateEmployeeCommandValidatorTests
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
                .Setup(r => r.GetEmployeeById(It.IsAny<Guid>()))
                .Returns(new Employee
                {
                    EmployeeId = Guid.NewGuid(),
                    RegistrationNumber = EmployeeRegistrationNumber.From("80122412456"),
                    Pesel = EmployeePesel.From("99110111111"),
                    BirthDate = EmployeeBirthDate.From(DateTime.Today),
                    Surname = EmployeeSurname.From("Stark"),
                    Name = EmployeeName.From("Tony"),
                    Sex = EmployeeSex.From(ESex.Male)
                });
            _repositoryMock
                .Setup(r => r.CheckIfEmployeeExists(It.IsAny<Guid>()))
                .Returns(true);
            _repositoryMock
                .Setup(r => r.CheckIfRegistrationNumberExistsOnDifferentEmployee(It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(false);
            _repositoryMock
                .Setup(r => r.CheckIfPeselExistsOnDifferentEmployee(It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(false);

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = Guid.NewGuid(),
                RegistrationNumber = "00000001",
                Pesel = "12345678912",
                BirthDate = DateTime.Now,
                Surname = "Wayne",
                Name = "Bruce",
                Sex = ESex.Male
            };

            _sut.ShouldNotHaveValidationErrorFor(r => r.EmployeeId, command.EmployeeId);
            _sut.ShouldNotHaveValidationErrorFor(r => r.Pesel, command.Pesel);
            _sut.ShouldNotHaveValidationErrorFor(r => new {r.Pesel, r.EmployeeId, r.RegistrationNumber}, command);
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
                .Setup(r => r.GetEmployeeById(It.IsAny<Guid>()))
                .Returns(new Employee
                {
                    EmployeeId = Guid.NewGuid(),
                    RegistrationNumber = EmployeeRegistrationNumber.From("80122412456"),
                    Pesel = EmployeePesel.From("99110111111"),
                    BirthDate = EmployeeBirthDate.From(DateTime.Today),
                    Surname = EmployeeSurname.From("Stark"),
                    Name = EmployeeName.From("Tony"),
                    Sex = EmployeeSex.From(ESex.Male)
                });
            _repositoryMock
                .Setup(r => r.CheckIfEmployeeExists(It.IsAny<Guid>()))
                .Returns(false);
            _repositoryMock
                .Setup(r => r.CheckIfRegistrationNumberExistsOnDifferentEmployee(It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(true);
            _repositoryMock
                .Setup(r => r.CheckIfPeselExistsOnDifferentEmployee(It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(true);

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = Guid.NewGuid(),
                RegistrationNumber = "00000001",
                Pesel = pesel,
                BirthDate = DateTime.Now,
                Surname = surname,
                Name = name,
                Sex = sex
            };

            _sut.ShouldHaveValidationErrorFor(r => r.EmployeeId, command.EmployeeId);
            _sut.ShouldHaveValidationErrorFor(r => r.Pesel, command.Pesel);
            _sut.ShouldHaveValidationErrorFor(r => new {r.Pesel, r.EmployeeId, r.RegistrationNumber}, command);
            _sut.ShouldHaveValidationErrorFor(r => r.Surname, command.Surname);
            _sut.ShouldHaveValidationErrorFor(r => r.Name, command.Name);
        }
    }
}