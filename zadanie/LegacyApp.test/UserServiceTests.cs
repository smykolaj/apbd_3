namespace LegacyApp.test;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ReturnsFalseWhenFirstNameIsEmpty()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            null,
            "Smith",
            "smith@example.com",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_ReturnsFalseWhenMissingAtSignAndDotInEmail()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Joe",
            "Smith",
            "smithexamplecom",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_ReturnsFalseWhenYoungerThen21YearsOld()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Joe",
            "Smith",
            "smithexamplecom",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_ReturnsTrueWhenVeryImportantClient()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Joe",
            "Smith",
            "smith@example.com",
            DateTime.Parse("2000-01-01"),
            2
        );
    
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_ThrowsExceptionWhenClientDoesNotExist()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        Action action = () => userService.AddUser(
            "Joe",
            "Smith",
            "smith@example.com",
            DateTime.Parse("2000-01-01"),
            100
        );
    
        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void AddUser_ReturnsTrueWhenImportantClient()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Joe",
            "Smith",
            "smith@example.com",
            DateTime.Parse("2000-01-01"),
            3
        );
    
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AddUser_ReturnsTrueWhenNormalClient()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Joe",
            "Doe",
            "smith@example.com",
            DateTime.Parse("2000-01-01"),
            1
        );
    
        // Assert
        Assert.True(result);
        
    }

    [Fact]
    public void AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Joe",
            "Kowalski",
            "smith@example.com",
            DateTime.Parse("2000-01-01"),
            1
        );
    
        // Assert
        Assert.False(result);
        
    }

    [Fact]
    public void AddUser_ThrowsExceptionWhenUserNoCreditLimitExistsForUser()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        Action action = () => userService.AddUser(
            "Joe",
            "Sukonnik",
            "smith@example.com",
            DateTime.Parse("2000-01-01"),
            1
        );
    
        // Assert
        Assert.Throws<ArgumentException>(action);
    }
}