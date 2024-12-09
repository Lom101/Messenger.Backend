using Messenger.Backend.Controller;
using Messenger.Backend.Repository.Common.Interfaces;
using Moq;

namespace Messenger.Tests;

public class UserControllerTests
{

    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _controller = new UserController(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public void Test1()
    {
    }
}