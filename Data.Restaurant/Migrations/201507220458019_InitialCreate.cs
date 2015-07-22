namespace Infrastructure.Data.Restaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 125, storeType: "nvarchar"),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(unicode: false),
                        ModifiedOn = c.DateTime(precision: 0),
                        Name = c.String(nullable: false, maxLength: 125, storeType: "nvarchar"),
                        ReservationDateTime = c.DateTime(nullable: false, precision: 0),
                        GuestsCount = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reservations");
        }
    }
}
