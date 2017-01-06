namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeteringDAOs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TerminalId = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeteringSensorValueRelationDAOs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MeteringId = c.Long(nullable: false),
                        SensorValueId = c.Long(nullable: false),
                        PropertyName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeteringDAOs", t => t.MeteringId, cascadeDelete: true)
                .ForeignKey("dbo.SensorValueDAOs", t => t.SensorValueId, cascadeDelete: true)
                .Index(t => t.MeteringId)
                .Index(t => t.SensorValueId);
            
            CreateTable(
                "dbo.SensorValueDAOs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Value = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeteringSensorValueRelationDAOs", "SensorValueId", "dbo.SensorValueDAOs");
            DropForeignKey("dbo.MeteringSensorValueRelationDAOs", "MeteringId", "dbo.MeteringDAOs");
            DropIndex("dbo.MeteringSensorValueRelationDAOs", new[] { "SensorValueId" });
            DropIndex("dbo.MeteringSensorValueRelationDAOs", new[] { "MeteringId" });
            DropTable("dbo.SensorValueDAOs");
            DropTable("dbo.MeteringSensorValueRelationDAOs");
            DropTable("dbo.MeteringDAOs");
        }
    }
}
