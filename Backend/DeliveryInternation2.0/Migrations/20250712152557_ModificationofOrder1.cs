using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryInternation2._0.Migrations
{
    /// <inheritdoc />
    public partial class ModificationofOrder1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishInOrder_Dishes_DishId",
                table: "DishInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_DishInOrder_Orders_Orderid",
                table: "DishInOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishInOrder",
                table: "DishInOrder");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "DishInOrder",
                newName: "DishInOrders");

            migrationBuilder.RenameIndex(
                name: "IX_DishInOrder_Orderid",
                table: "DishInOrders",
                newName: "IX_DishInOrders_Orderid");

            migrationBuilder.RenameIndex(
                name: "IX_DishInOrder_DishId",
                table: "DishInOrders",
                newName: "IX_DishInOrders_DishId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishInOrders",
                table: "DishInOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishInOrders_Dishes_DishId",
                table: "DishInOrders",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishInOrders_Orders_Orderid",
                table: "DishInOrders",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishInOrders_Dishes_DishId",
                table: "DishInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DishInOrders_Orders_Orderid",
                table: "DishInOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishInOrders",
                table: "DishInOrders");

            migrationBuilder.RenameTable(
                name: "DishInOrders",
                newName: "DishInOrder");

            migrationBuilder.RenameIndex(
                name: "IX_DishInOrders_Orderid",
                table: "DishInOrder",
                newName: "IX_DishInOrder_Orderid");

            migrationBuilder.RenameIndex(
                name: "IX_DishInOrders_DishId",
                table: "DishInOrder",
                newName: "IX_DishInOrder_DishId");

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishInOrder",
                table: "DishInOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishInOrder_Dishes_DishId",
                table: "DishInOrder",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishInOrder_Orders_Orderid",
                table: "DishInOrder",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
