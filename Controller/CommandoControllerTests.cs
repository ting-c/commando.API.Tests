using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CommandoAPI.Models;
using CommandoAPI.Services;
using Xunit;
using Moq;
using CommandoAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CommandoAPI.Tests
{
    public class CommandoControllerTests
    {
        private readonly Mock<ICommandItemService> _mockRepo;
        private readonly CommandoController _controller;

        public CommandoControllerTests()
        {
            _mockRepo = new Mock<ICommandItemService>();
            _controller = new CommandoController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnsAnOkObject_WithAListOfCommandItems()
        {
            var result = await _controller.GetAsync();
            Assert.IsType<ActionResult<List<CommandItem>>>(result);
        }

        [Fact]
        public async Task PostAsync_ReturnsACreatedAtActionObject_withACommandItem()
        {
            var commandItem = new CommandItem
            {
                Command = "command1",
                Description = "description1"
            };
            var result = await _controller.PostAsync(commandItem);
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(commandItem, actionResult.Value);
        }

        [Fact]
        public async Task PutAsync_WithAnInvalidItem_ReturnsNotFoundResult()
        {
            var commandItem = new CommandItem
            {
                Command = "command2",
                Description = "description2"
            };
            var result = await _controller.PutAsync(commandItem.Id, commandItem);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_WithAnInvalidItem_ReturnsNotFoundResult()
        {
            var result = await _controller.DeleteAsync(new Guid {});

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
