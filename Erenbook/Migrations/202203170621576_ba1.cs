namespace Erenbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ba1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arizalars", "ArizaBildiren", c => c.String());
            AddColumn("dbo.Arizalars", "ArizaAiklama", c => c.String());
            AddColumn("dbo.Arizalars", "KullanilanMalzeme", c => c.String());
            AddColumn("dbo.Arizalars", "Malbeklemesuresi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Arizalars", "Malbeklemesuresi");
            DropColumn("dbo.Arizalars", "KullanilanMalzeme");
            DropColumn("dbo.Arizalars", "ArizaAiklama");
            DropColumn("dbo.Arizalars", "ArizaBildiren");
        }
    }
}
