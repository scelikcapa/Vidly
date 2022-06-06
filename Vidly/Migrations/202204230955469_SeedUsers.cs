namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c73a8659-8032-4d2a-8d59-bbbe1f9ae660', N'guest@vidly.com', 0, N'AKQ78P309Euj+AXTrWwfiqbNtIW5pylimqHEmIn9gMwr/LQIpBO1lc1G3aca3Ab+ww==', N'5534113b-f25b-4bdd-bf08-18798521866e', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f94bc386-1112-4dec-98d7-ea69202f0cb5', N'admin@vidly.com', 0, N'AOmHbKNi/V6uFOUFTNJwslNiSEXDVcfDv8yzMYLmPX48Wd0oJLumj5sgW8UsxvfvgQ==', N'c601606d-ea39-4cd3-9792-498d143a9d2f', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e63cc4d1-e66d-4b35-97c2-38dc78062f4c', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f94bc386-1112-4dec-98d7-ea69202f0cb5', N'e63cc4d1-e66d-4b35-97c2-38dc78062f4c')

");
        }
        
        public override void Down()
        {
        }
    }
}
