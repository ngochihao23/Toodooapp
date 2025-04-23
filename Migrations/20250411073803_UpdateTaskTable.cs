using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todoapp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreateById",
                table: "Tasks",
                column: "CreateById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_CreateById",
                table: "Tasks",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_CreateById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreateById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Tasks");
        }
    }
}
