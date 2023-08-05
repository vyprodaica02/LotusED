using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaiThucTaplan1.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "roll",
                table: "rolllNumbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roll",
                table: "rolllNumbers");
        }
    }
}
