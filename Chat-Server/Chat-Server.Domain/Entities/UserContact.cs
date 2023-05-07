namespace Chat_Server.Domain.Entities;

public class UserContact {
	public int UserId { get; set; }
	public int ContactUserId { get; set; }

	public User User { get; set; }
	public User ContactUser { get; set; }
}