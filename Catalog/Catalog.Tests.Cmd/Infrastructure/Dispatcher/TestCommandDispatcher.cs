using Catalog.Cmd.Command.CommandHandlers;
using Catalog.Cmd.Command.Commands;
using Catalog.Cmd.Infrastructure.Dispatcher;
using CQRS.Core.Commands;
using Moq;
using NUnit.Framework;

namespace Catalog.Tests.Cmd.Infrastructure.Dispatcher
{
    public class TestCommandDispatcher
    {
        [Test]
        public void TestRegisterHandlerOK()
        {
            var dispatcher = new CommandDispatcher();
            var commandHandlerMock = new Mock<ICatalogCommandHandler>();
            commandHandlerMock.Setup(c => c.HandleAsync(new ProductCreateCommand())).Returns(Task.CompletedTask);
            dispatcher.RegisterHandler<ProductCreateCommand>(commandHandlerMock.Object.HandleAsync);
            Assert.IsTrue(true);
        }

        [Test]
        public void TestRegisterHandlerKO()
        {
            var dispatcher = new CommandDispatcher();
            var commandHandlerMock = new Mock<ICatalogCommandHandler>();
            commandHandlerMock.Setup(c => c.HandleAsync(new ProductCreateCommand())).Returns(Task.CompletedTask);
            dispatcher.RegisterHandler<ProductCreateCommand>(commandHandlerMock.Object.HandleAsync);
            Assert.Throws(typeof(IndexOutOfRangeException), () => dispatcher.RegisterHandler<ProductCreateCommand>(commandHandlerMock.Object.HandleAsync));
        }

        [Test]
        public async Task TestSendAsyncOk()
        {
            var dispatcher = new CommandDispatcher();
            var commandHandlerMock = new Mock<ICatalogCommandHandler>();
            commandHandlerMock.Setup(c => c.HandleAsync(new ProductCreateCommand())).Returns(Task.CompletedTask);
            dispatcher.RegisterHandler<ProductCreateCommand>(commandHandlerMock.Object.HandleAsync);
            var productCreateCommand = new ProductCreateCommand();
            await dispatcher.SendAsync(productCreateCommand);
            Assert.IsTrue(true);
        }

        [Test]
        public Task TestSendAsyncKo()
        {
            var dispatcher = new CommandDispatcher();
            var productCreateCommand = new ProductCreateCommand();
            Assert.ThrowsAsync<ArgumentNullException>(() => dispatcher.SendAsync(productCreateCommand));
            Assert.IsTrue(true);
            return Task.CompletedTask;
        }
    }
}
