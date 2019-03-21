namespace DevConfSkopje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeInConfModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConferenceRegistrations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirsName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        IsValid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConferenceRegistrations");
        }
    }
}
