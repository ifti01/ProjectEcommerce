namespace FinalProject.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuantityInOrderItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "Quantity");
        }
    }
}
