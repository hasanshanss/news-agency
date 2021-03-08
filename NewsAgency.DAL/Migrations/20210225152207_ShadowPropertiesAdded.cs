using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAgency.DAL.Migrations
{
    public partial class ShadowPropertiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_NewsCategories_NewsCategoryId",
                table: "News");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "NewsCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "NewsCategories",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_News_NewsCategories_NewsCategoryId",
                table: "News",
                column: "NewsCategoryId",
                principalTable: "NewsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_NewsCategories_NewsCategoryId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "NewsCategories");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "NewsCategories");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "News",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_News_NewsCategories_NewsCategoryId",
                table: "News",
                column: "NewsCategoryId",
                principalTable: "NewsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
