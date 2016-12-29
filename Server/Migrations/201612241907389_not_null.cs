namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class not_null : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeteringDAOs", "TerminalId", c => c.String(nullable: false));
            AlterColumn("dbo.SensorValueDAOs", "Value", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SensorValueDAOs", "Value", c => c.Binary());
            AlterColumn("dbo.MeteringDAOs", "TerminalId", c => c.String());
        }
    }
}
