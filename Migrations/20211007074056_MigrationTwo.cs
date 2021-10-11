using Microsoft.EntityFrameworkCore.Migrations;

namespace todolistReactAsp.Migrations
{
    public partial class MigrationTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("Color", "Tag", "varchar(255)",
            unicode: false, maxLength: 255, nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
