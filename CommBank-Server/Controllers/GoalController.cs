using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CommBank.Controllers;
using CommBank.Models;
using CommBank.Services;

namespace CommBank.Tests
{
    public class GoalControllerTests
    {
        [Fact]
        public async Task GetForUser_ReturnsListOfGoals_WhenUserExists()
        {
            // 1. Arrange: Mock the backend data services
            var mockGoalsService = new Mock<IGoalsService>();
            var mockUsersService = new Mock<IUsersService>();
            
            var fakeUserId = "64b0f924e13d8a11a24b6789"; // Standard 24-character hex ID string
            var fakeGoals = new List<Goal>
            {
                new Goal { Id = "64b0f924e13d8a11a24b6111", Name = "Holiday Fund", TargetAmount = 2000, Icon = "✈️" },
                new Goal { Id = "64b0f924e13d8a11a24b6222", Name = "New Laptop", TargetAmount = 1500, Icon = "💻" }
            };

            // Set up mock to return fake database entries when the target route queries it
            mockGoalsService
                .Setup(service => service.GetForUserAsync(fakeUserId))
                .ReturnsAsync(fakeGoals);

            // Instantiate the controller passing our mocks
            var controller = new GoalController(mockGoalsService.Object, mockUsersService.Object);

            // 2. Act: Call the specific target endpoint route
            var result = await controller.GetForUser(fakeUserId);

            // 3. Assert: Verify the test outcome matches expectations
            var returnedGoals = Assert.IsType<List<Goal>>(result);
            Assert.NotNull(returnedGoals);
            Assert.Equal(2, returnedGoals.Count);
            Assert.Equal("Holiday Fund", returnedGoals[0].Name);
            Assert.Equal("✈️", returnedGoals[0].Icon);
        }
    }
}
