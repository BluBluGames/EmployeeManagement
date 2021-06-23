using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Queries;
using EmployeeManagement.Services.EmployeeManagement.Validators;
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
                Id = 1
            };

            _sut.ShouldNotHaveValidationErrorFor(r => r.Id, query.Id);
        }
    }
}