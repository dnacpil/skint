using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skint.Migrations
{
    /// <inheritdoc />
    public partial class Budget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Summary",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Income",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Expenses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Debt",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Summary_UserId",
                table: "Summary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Income_UserId",
                table: "Income",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Debt_UserId",
                table: "Debt",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debt_AspNetUsers_UserId",
                table: "Debt",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_AspNetUsers_UserId",
                table: "Income",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Summary_AspNetUsers_UserId",
                table: "Summary",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debt_AspNetUsers_UserId",
                table: "Debt");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_UserId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_AspNetUsers_UserId",
                table: "Income");

            migrationBuilder.DropForeignKey(
                name: "FK_Summary_AspNetUsers_UserId",
                table: "Summary");

            migrationBuilder.DropIndex(
                name: "IX_Summary_UserId",
                table: "Summary");

            migrationBuilder.DropIndex(
                name: "IX_Income_UserId",
                table: "Income");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Debt_UserId",
                table: "Debt");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Summary");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Debt");
        }
    }
}
