namespace Chat_Server.Services.JWT; 

public class CheckJwtResult {
	public int UserId { get; set; }
	public bool IsFaulted { get; set; }
}