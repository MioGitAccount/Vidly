namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0daa7702-c32d-41d3-98f7-0891f004ec19', N'admin@vidly.com', 0, N'AHyL3F/0bfp2LV+YwpxvKWQ24+E5N5ohyGv9NybTIN0LdwrRRmEVUUfyfe2dpw0YGw==', N'139bc021-ae09-45ba-8568-c260ebc54a72', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'acfe9f50-da77-4040-9de2-278fc1f933fe', N'guest@vidly.com', 0, N'AIm0FZtOIKw8qdS3L4pTvPO1uQuVgBmlKcViyWeY7u1oxY9ydVIeMgRq1igL4gctVQ==', N'0f05f2df-456e-477c-b566-a8a95fb4b8a1', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a1f5eaf9-4844-4679-945b-e45199bd28b6', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0daa7702-c32d-41d3-98f7-0891f004ec19', N'a1f5eaf9-4844-4679-945b-e45199bd28b6')

");
        }
        
        public override void Down()
        {
        }
    }
}
