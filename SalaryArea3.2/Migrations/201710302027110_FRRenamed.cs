namespace SalaryArea3._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FRRenamed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LivingWageMins", name: "timeperiod_PeriodID", newName: "PeriodId");
            RenameIndex(table: "dbo.LivingWageMins", name: "IX_timeperiod_PeriodID", newName: "IX_PeriodId");
            DropColumn("dbo.LivingWageMins", "TimePeriodId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LivingWageMins", "TimePeriodId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.LivingWageMins", name: "IX_PeriodId", newName: "IX_timeperiod_PeriodID");
            RenameColumn(table: "dbo.LivingWageMins", name: "PeriodId", newName: "timeperiod_PeriodID");
        }
    }
}
