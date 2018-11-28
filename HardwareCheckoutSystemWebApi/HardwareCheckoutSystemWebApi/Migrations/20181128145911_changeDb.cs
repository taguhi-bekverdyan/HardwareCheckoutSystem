using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HardwareCheckoutSystemWebApi.Migrations
{
    public partial class changeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Responses_RequestId",
                table: "Responses");

            migrationBuilder.AddColumn<Guid>(
                name: "LastResponseId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_RequestId",
                table: "Responses",
                column: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Responses_RequestId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "LastResponseId",
                table: "Requests");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_RequestId",
                table: "Responses",
                column: "RequestId",
                unique: true);
        }
    }
}
