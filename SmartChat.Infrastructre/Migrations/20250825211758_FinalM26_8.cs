using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartChat.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class FinalM26_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Messages_userId",
                table: "Messages",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_userId",
                table: "Messages",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_userId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_userId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Messages");
        }
    }
}
