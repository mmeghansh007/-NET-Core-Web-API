using System.ComponentModel.DataAnnotations;

public enum UserRole { Admin, User }

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public UserRole Role { get; set; }
    public string Password { get; set; } 
}

public class TaskItems
{
    [Key]
    public int Id { get; set; } 
    public string Description { get; set; }

    public int AssignedUserId { get; set; }
    public User AssignedUser { get; set; }
}



public class TaskComments
{
    
    public int Id { get; set; }
    public string Content { get; set; }
    public int TaskItemId { get; set; }
    public TaskItems TaskItem { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
