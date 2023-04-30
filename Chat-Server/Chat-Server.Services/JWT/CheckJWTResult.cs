using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Services.JWT
{
	public class CheckJWTResult
	{
		public int UserId { get; set; }
		public bool IsFaulted { get; set; }
	}
}
