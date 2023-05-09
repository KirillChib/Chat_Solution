namespace Chat_Server.Domain.Entities; 

public class Blocking {
	public int UserId { get; set; }
	public int BlockingUserId { get; set; }

	public User User { get; set; }
	public User BlockingUser { get; set; }
}