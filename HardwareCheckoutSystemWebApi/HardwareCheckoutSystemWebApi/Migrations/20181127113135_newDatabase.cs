using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HardwareCheckoutSystemWebApi.Migrations
{
    public partial class newDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Responses_LastResponseId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Requests_RequestId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_RequestId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Requests_LastResponseId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "LastResponseId",
                table: "Requests");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "Responses",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_RequestId",
                table: "Responses",
                column: "RequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Requests_RequestId",
                table: "Responses",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Requests_RequestId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_RequestId",
                table: "Responses");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "Responses",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "LastResponseId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_RequestId",
                table: "Responses",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_LastResponseId",
                table: "Requests",
                column: "LastResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Responses_LastResponseId",
                table: "Requests",
                column: "LastResponseId",
                principalTable: "Responses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Requests_RequestId",
                table: "Responses",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
