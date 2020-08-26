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
        public async Task Get_ReturnsAnOkObject_WithAListOfCommandItems()
        {
            var result = await _controller.Get();
            Assert.IsType<ActionResult<List<CommandItem>>>(result);
        }
    }
}
