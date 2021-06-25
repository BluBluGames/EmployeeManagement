using System;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Commands;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Validators;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Repositories;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Validators
{
    internal class RemoveEmployeeCommandValidatorTests
    {
        private RemoveEmployeeCommandValidator _sut;
        private Mock<IEmployeeRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();

            _sut = new RemoveEmployeeCommandValidator(_repositoryMock.Object);
        }

        [Test]
        public void VerifyThatCorrectDataIsPassed()
        {
            _repositoryMock
                .Setup(r => r.CheckIfEmployeeExists(It.IsAny<Guid>()))
                .Returns(true);

            var command = new RemoveEmployeeCommand
            {
                Id = Guid.NewGuid()
            };

            _sut.ShouldNotHaveValidationErrorFor(r => r.Id, command.Id);
        }

        [Test]
        [TestCase("123456789123", "123456789123456789123456789123456789123456789123456789",
            "123456789123456789123456789", 8)]
        public void VerifyThatIncorrectDataIsNotPassed(string pesel, string surname, string name, ESex sex)
        {
            _repositoryMock
                .Setup(r => r.CheckIfEmployeeExists(It.IsAny<Guid>()))
                .Returns(false);

            var command = new RemoveEmployeeCommand
            {
                Id = Guid.NewGuid()
            };

            _sut.ShouldHaveValidationErrorFor(r => r.Id, command.Id);
        }
    }
}