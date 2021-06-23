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
    class RemoveEmployeeCommandValidatorTests
    {
        private RemoveEmployeeCommandValidator _sut;
        private Mock<IEmployeeRepository> _repositoryMock;

        [SetUp]
        public void OneTimeSetup()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();

            _sut = new RemoveEmployeeCommandValidator(_repositoryMock.Object);
        }
        [Test]
        public void VerifyThatCorrectDataIsPassed()
        {
            _repositoryMock
                .Setup(r => r.CheckIfEmployeeExists(It.IsAny<int>()))
                .Returns(true);

            var command = new RemoveEmployeeCommand
            {
                Id = 1
            };

            _sut.ShouldNotHaveValidationErrorFor(r => r.Id, command.Id);
        }

        [Test]
        [TestCase("123456789123", "123456789123456789123456789123456789123456789123456789", "123456789123456789123456789", 8)]
        public void VerifyThatIncorrectDataIsNotPassed(string pesel, string surname, string name, ESex sex)
        {
            _repositoryMock
                .Setup(r => r.CheckIfEmployeeExists(It.IsAny<int>()))
                .Returns(false);

            var command = new RemoveEmployeeCommand
            {
                Id = 1
            };

            _sut.ShouldHaveValidationErrorFor(r => r.Id, command.Id);
        }
    }
}
