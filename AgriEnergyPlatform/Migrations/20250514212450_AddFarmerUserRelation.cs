﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergyPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddFarmerUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Farmers_FarmerID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FarmerID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Farmers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Farmers_UserId",
                table: "Farmers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Farmers_AspNetUsers_UserId",
                table: "Farmers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Farmers_AspNetUsers_UserId",
                table: "Farmers");

            migrationBuilder.DropIndex(
                name: "IX_Farmers_UserId",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Farmers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FarmerID",
                table: "AspNetUsers",
                column: "FarmerID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Farmers_FarmerID",
                table: "AspNetUsers",
                column: "FarmerID",
                principalTable: "Farmers",
                principalColumn: "FarmerID");
        }
    }
}
