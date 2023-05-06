using Chat_Server.Domain.Entities;
using System.Data.Entity;

namespace Chat_Server.Context
{
		public class ChatDbContext : DbContext	{
		public DbSet<User> Users { get; set; }
		public DbSet<Channel> Channels { get; set; }
		public DbSet<UserMessage> UserMessages { get; set; }
		public DbSet<ChannelMessage> ChannelMessages { get; set; }
		public DbSet<ChannelUser> ChannelsUsers { get; set; }

		public ChatDbContext() : base("DbConnection")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// todo(v): нужен ли IsUnicode?
			modelBuilder.Entity<User>()
				.Property(u => u.Login)
				.IsRequired()
				.IsUnicode();

			modelBuilder.Entity<User>()
				.Property(u => u.Name)
				.IsRequired()
				.IsUnicode();

			modelBuilder.Entity<Channel>()
				.Property(c => c.Name)
				.IsRequired()
				.IsUnicode();

			modelBuilder.Entity<UserMessage>()
				.HasRequired(um => um.UserFrom)
				.WithMany(u => u.UserMessagesFrom)
				.HasForeignKey(um => um.UserFromId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<UserMessage>()
				.HasRequired(um => um.UserTo)
				.WithMany(u => u.UserMessagesTo)
				.HasForeignKey(um => um.UserToId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<UserMessage>()
				.Property(um => um.CreatedAt)
				.HasColumnType("date");

			modelBuilder.Entity<ChannelMessage>()
				.HasRequired(cm => cm.UserFrom)
				.WithMany(u => u.ChannelMessages)
				.HasForeignKey(cm => cm.UserFromId);

			modelBuilder.Entity<ChannelMessage>()
				.HasRequired(cm => cm.Channel)
				.WithMany(c => c.ChannelMessages)
				.HasForeignKey(cm => cm.ChannelId);

			modelBuilder.Entity<ChannelMessage>()
				.Property(cm => cm.CreatedAt)
				.HasColumnType("date");

			modelBuilder.Entity<ChannelUser>()
				.HasKey(cu => new { cu.ChannelId, cu.UserId });

			modelBuilder.Entity<ChannelUser>()
				.HasRequired(cu => cu.Channel)
				.WithMany(c => c.ChannelUsers)
				.HasForeignKey(cu => cu.ChannelId);

			modelBuilder.Entity<ChannelUser>()
				.HasRequired(cu => cu.User)
				.WithMany(u => u.ChannelsUser)
				.HasForeignKey(cu => cu.UserId);

			base.OnModelCreating(modelBuilder);
		}
	}
}