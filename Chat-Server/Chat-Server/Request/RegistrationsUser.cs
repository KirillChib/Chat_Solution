﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Request
{
	public class RegistrationsUser
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
	}
}
