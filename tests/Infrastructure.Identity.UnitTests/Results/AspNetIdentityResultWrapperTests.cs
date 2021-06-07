using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using Infrastructure.Identity.Results;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;


namespace Infrastructure.Identity.UnitTests.Results
{
    /// <summary>
    /// Contains unit tests for type <see cref="AspNetIdentityResultWrapper"/>.
    /// </summary>
    public class AspNetIdentityResultWrapperTests
    {
        #region Unit Tests

        [Fact]
        public void Succeeded_SucceededIdentityResultObjectPassedIntoConstructor_ReturnsTrue()
        {
            // Arrange
            var wrapper = new AspNetIdentityResultWrapper(IdentityResult.Success);
            bool expectedResult = true;

            // Act
            bool actualResult = wrapper.Succeeded;

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public void Succeeded_FailedIdentityResultObjectPassedIntoConstructor_ReturnsFalse()
        {
            // Arrange
            var wrapper = new AspNetIdentityResultWrapper(GetFailedIdentityResult());
            bool expectedResult = false;

            // Act
            bool actualResult = wrapper.Succeeded;

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public void Errors_SucceededIdentityResultObjectPassedIntoConstructor_IsEmptyCollection()
        {
            // Arrange
            var wrapper = new AspNetIdentityResultWrapper(IdentityResult.Success);

            // Act
            IEnumerable<string> actualResult = wrapper.Errors;

            // Assert
            actualResult.Should().BeEmpty();
        }
        
        [Fact]
        public void Errors_FailedIdentityResultObjectPassedIntoConstructor_ContainsErrorsCollection()
        {
            // Arrange
            IdentityResult identityResult = GetFailedIdentityResult();
            var identityResultWrapper = new AspNetIdentityResultWrapper(identityResult);

            IEnumerable<string> expectedResult = identityResult.Errors
                .Select(e => e.Description);

            // Act
            IEnumerable<string> actualResult = identityResultWrapper.Errors;

            // Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns an object of type <see cref="IdentityResult"/> that represents a failed operation,
        /// containing some errors.
        /// </summary>
        /// <returns></returns>
        private IdentityResult GetFailedIdentityResult()
        {
            return IdentityResult.Failed
            (
                new IdentityError {Code = "E1", Description = "Error1"},
                new IdentityError {Code = "E2", Description = "Error2"},
                new IdentityError {Code = "E3", Description = "Error3"}
            );
        }

        #endregion
    }
}