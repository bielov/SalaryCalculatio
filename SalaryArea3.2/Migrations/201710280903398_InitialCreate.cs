namespace SalaryArea3._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        ErdpoyCode = c.String(),
                        CompanyAdress = c.String(),
                        RegistrationAdress = c.String(),
                        InformationCode = c.String(),
                        RegistrationDate = c.String(),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.DayTypes",
                c => new
                    {
                        DayTypeID = c.Int(nullable: false, identity: true),
                        NameDayType = c.String(),
                    })
                .PrimaryKey(t => t.DayTypeID);
            
            CreateTable(
                "dbo.SpecialDays",
                c => new
                    {
                        SpecialDayID = c.Int(nullable: false, identity: true),
                        SpecialDayYear = c.Int(nullable: false),
                        DayValue = c.Int(nullable: false),
                        DayTypeId = c.Int(nullable: false),
                        PeriodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpecialDayID)
                .ForeignKey("dbo.DayTypes", t => t.DayTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TimePeriods", t => t.PeriodId, cascadeDelete: true)
                .Index(t => t.DayTypeId)
                .Index(t => t.PeriodId);
            
            CreateTable(
                "dbo.TimePeriods",
                c => new
                    {
                        PeriodID = c.Int(nullable: false, identity: true),
                        PeriodName = c.String(),
                    })
                .PrimaryKey(t => t.PeriodID);
            
            CreateTable(
                "dbo.SalaryCalculations",
                c => new
                    {
                        SalaryCalculationID = c.Int(nullable: false, identity: true),
                        RealWorkTime = c.Single(nullable: false),
                        BasicSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VacationAccure = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IllnessMonth = c.Int(nullable: false),
                        IllnessDay = c.Int(nullable: false),
                        IllnessSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Indexation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumAccure = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PSP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ESV = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeductMilitaryTex = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeductPDFO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeductSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Advance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AnotherDeduct = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeID = c.Int(nullable: false),
                        TimePeriodID = c.Int(nullable: false),
                        timeperiod_PeriodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalaryCalculationID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.TimePeriods", t => t.timeperiod_PeriodID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.timeperiod_PeriodID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        PositionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.People", t => t.EmployeeID)
                .ForeignKey("dbo.Positions", t => t.PositionID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.PositionID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        FirstName = c.String(),
                        MidleName = c.String(),
                        IndentificalCode = c.String(maxLength: 10),
                        PasportCode = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                        gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                    })
                .PrimaryKey(t => t.PositionId);
            
            CreateTable(
                "dbo.LivingWageMins",
                c => new
                    {
                        WageID = c.Int(nullable: false, identity: true),
                        WageYear = c.Int(nullable: false),
                        WageValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimePeriodId = c.Int(nullable: false),
                        timeperiod_PeriodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WageID)
                .ForeignKey("dbo.TimePeriods", t => t.timeperiod_PeriodID, cascadeDelete: true)
                .Index(t => t.timeperiod_PeriodID);
            
            CreateTable(
                "dbo.Taxes",
                c => new
                    {
                        TaxID = c.Int(nullable: false, identity: true),
                        TaxName = c.String(),
                        TaxPresentage = c.Decimal(nullable: false, precision: 4, scale: 2),
                    })
                .PrimaryKey(t => t.TaxID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecialDays", "PeriodId", "dbo.TimePeriods");
            DropForeignKey("dbo.LivingWageMins", "timeperiod_PeriodID", "dbo.TimePeriods");
            DropForeignKey("dbo.SalaryCalculations", "timeperiod_PeriodID", "dbo.TimePeriods");
            DropForeignKey("dbo.SalaryCalculations", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "PositionID", "dbo.Positions");
            DropForeignKey("dbo.Employees", "EmployeeID", "dbo.People");
            DropForeignKey("dbo.SpecialDays", "DayTypeId", "dbo.DayTypes");
            DropIndex("dbo.LivingWageMins", new[] { "timeperiod_PeriodID" });
            DropIndex("dbo.Employees", new[] { "PositionID" });
            DropIndex("dbo.Employees", new[] { "EmployeeID" });
            DropIndex("dbo.SalaryCalculations", new[] { "timeperiod_PeriodID" });
            DropIndex("dbo.SalaryCalculations", new[] { "EmployeeID" });
            DropIndex("dbo.SpecialDays", new[] { "PeriodId" });
            DropIndex("dbo.SpecialDays", new[] { "DayTypeId" });
            DropTable("dbo.Taxes");
            DropTable("dbo.LivingWageMins");
            DropTable("dbo.Positions");
            DropTable("dbo.People");
            DropTable("dbo.Employees");
            DropTable("dbo.SalaryCalculations");
            DropTable("dbo.TimePeriods");
            DropTable("dbo.SpecialDays");
            DropTable("dbo.DayTypes");
            DropTable("dbo.Companies");
        }
    }
}
