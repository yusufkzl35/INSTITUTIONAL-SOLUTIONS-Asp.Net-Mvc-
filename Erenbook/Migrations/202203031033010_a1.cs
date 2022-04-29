namespace Erenbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arizalar2", "MudahaleTipi", c => c.String());
            AddColumn("dbo.Arizalar2", "MudahaleSinifi", c => c.String());
            DropColumn("dbo.Arizalar2", "ArizaNedeni");
            DropColumn("dbo.Arizalar2", "YapilanMudahale");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arizalar2", "YapilanMudahale", c => c.String());
            AddColumn("dbo.Arizalar2", "ArizaNedeni", c => c.String());
            DropColumn("dbo.Arizalar2", "MudahaleSinifi");
            DropColumn("dbo.Arizalar2", "MudahaleTipi");
        }
    }
}
