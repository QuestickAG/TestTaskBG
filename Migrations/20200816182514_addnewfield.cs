using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskBarsGroup.Migrations
{
    public partial class addnewfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Office_Officeid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Office",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Officeid",
                table: "Employees",
                newName: "OfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Officeid",
                table: "Employees",
                newName: "IX_Employees_OfficeId");

            migrationBuilder.AddColumn<decimal>(
                name: "Payment",
                table: "Employees",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Office_OfficeId",
                table: "Employees",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Office_OfficeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Office",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OfficeId",
                table: "Employees",
                newName: "Officeid");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_OfficeId",
                table: "Employees",
                newName: "IX_Employees_Officeid");

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Office_Officeid",
                table: "Employees",
                column: "Officeid",
                principalTable: "Office",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
