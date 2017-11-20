namespace SalaryArea3._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Salaryyearadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalaryCalculations", "CalculationYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalaryCalculations", "CalculationYear");
        }
    }
}
