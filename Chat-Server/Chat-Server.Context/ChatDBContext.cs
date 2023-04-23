using Chat_Server.Domain.Entities;
using System.Data.Entity;

namespace Chat_Server.Context
{
	public class ChatDBContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public ChatDBContext() : base("DbConnection")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().Property(u => u.Login)
																.IsRequired()
																.IsUnicode();
			modelBuilder.Entity<User>().Property(u => u.Name)
																.IsRequired()
																.IsUnicode();

			base.OnModelCreating(modelBuilder);
		}
	}
}