using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class loginAndRegister_in_cliente_and_designer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "Portfolio",
                table: "Designers");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Designers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Designers",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "Designers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Designers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Designers_Email",
                table: "Designers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Designers_Username",
                table: "Designers",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Designers_Email",
                table: "Designers");

            migrationBuilder.DropIndex(
                name: "IX_Designers_Username",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Designers");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Designers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Designers",
                newName: "Specialization");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Designers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Portfolio",
                table: "Designers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
