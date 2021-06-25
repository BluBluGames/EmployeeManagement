using System;
using EmployeeManagement.Application.V1.Employee.GetEmployee;
using EmployeeManagement.Repositories;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EmployeeManagementUnitTests.Services.EmployeeManagement.Validators
{
    internal class GetEmployeeByIdQueryTests
    {
        private GetEmployeeByIdQueryValidator _sut;
        private Mock<IEmployeeRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _sut = new GetEmployeeByIdQueryValidator();
        }

        [Test]
        public void VerifyThatCorrectDataIsPassed()
        {
            var query = new GetEmployeeByIdQuery
            {
                Id = Guid.NewGuid()
            };

            _sut.ShouldNotHaveValidationErrorFor(r => r.Id, query.Id);
        }
    }
}