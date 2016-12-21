namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class terminal_id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeteringDAOs", "TerminalId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeteringDAOs", "TerminalId");
        }
    }
}
