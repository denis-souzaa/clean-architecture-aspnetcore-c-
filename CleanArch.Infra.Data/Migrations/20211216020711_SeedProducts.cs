using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArch.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql(@"INSERT INTO Products (Name, Description,  Price, Stock,Image, CategoryId)
            VALUES ('Caderno Espiral', 'Caderno espiral 100 folhas', 7.45, 50,'caderno1.jpg',1)");
            
            mb.Sql(@"INSERT INTO Products (Name, Description,  Price, Stock,Image, CategoryId)
            VALUES ('Estorno escolar', 'estojo escolar cinza', 7.45,70,'estojo1.jpg',1)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
