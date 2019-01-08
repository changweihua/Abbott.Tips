using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abbott.Tips.EntityFrameworkCore.Migrations
{
    public partial class Update_TEvent_C1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EventTimestamp",
                table: "T_Event",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "T_Event",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EventTimestamp",
                table: "T_Event",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "EventId",
                table: "T_Event",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
