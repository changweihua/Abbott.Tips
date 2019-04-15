using Microsoft.EntityFrameworkCore.Migrations;

namespace Abbott.Tips.EntityFrameworkCore.Migrations
{
    public partial class UpdateBaseScafford : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditMode",
                table: "T_Configuration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EditMode",
                table: "T_Configuration",
                nullable: false,
                defaultValue: 0);
        }
    }
}
