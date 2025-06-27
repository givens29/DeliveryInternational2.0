using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryInternation2._0.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_DishesInCart_Carts_CartId",
                table: "DishesInCart");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_StorageUsersTokens_TokenId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TokenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "StorageUsersTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "DishesInCart",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageUsersTokens_UserId",
                table: "StorageUsersTokens",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishesInCart_Carts_CartId",
                table: "DishesInCart",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageUsersTokens_Users_UserId",
                table: "StorageUsersTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_DishesInCart_Carts_CartId",
                table: "DishesInCart");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageUsersTokens_Users_UserId",
                table: "StorageUsersTokens");

            migrationBuilder.DropIndex(
                name: "IX_StorageUsersTokens_UserId",
                table: "StorageUsersTokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StorageUsersTokens");

            migrationBuilder.AddColumn<Guid>(
                name: "TokenId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "DishesInCart",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TokenId",
                table: "Users",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesInCart_Carts_CartId",
                table: "DishesInCart",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_StorageUsersTokens_TokenId",
                table: "Users",
                column: "TokenId",
                principalTable: "StorageUsersTokens",
                principalColumn: "Id");
        }
    }
}
