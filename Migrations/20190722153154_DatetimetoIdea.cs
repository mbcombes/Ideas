using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpExam.Migrations
{
    public partial class DatetimetoIdea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirm",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ideas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Ideas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Ideas");

            migrationBuilder.AddColumn<string>(
                name: "Confirm",
                table: "Users",
                nullable: true);
        }
    }
}
