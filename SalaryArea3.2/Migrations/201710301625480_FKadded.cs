namespace SalaryArea3._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKadded : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.LivingWageMins");
            DropColumn("dbo.LivingWageMins", "WageID");

            AddColumn("dbo.LivingWageMins", "LivingWageMinID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.LivingWageMins", "LivingWageMinID");
                  }
        
        public override void Down()
        {
            AddColumn("dbo.LivingWageMins", "WageID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.LivingWageMins");
            DropColumn("dbo.LivingWageMins", "LivingWageMinID");
            AddPrimaryKey("dbo.LivingWageMins", "WageID");
        }
    }
}
