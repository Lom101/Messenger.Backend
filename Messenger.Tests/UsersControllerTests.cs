using Moq;

namespace Messenger.Tests;

public class UsersControllerTests
{

    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _controller = new UsersController(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public void Test1()
    {
    }
}