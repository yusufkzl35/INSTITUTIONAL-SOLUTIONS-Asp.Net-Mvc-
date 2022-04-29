namespace Erenbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ve1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arizalars", "ArizaAciklama", c => c.String());
            DropColumn("dbo.Arizalars", "ArizaAiklama");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arizalars", "ArizaAiklama", c => c.String());
            DropColumn("dbo.Arizalars", "ArizaAciklama");
        }
    }
}
