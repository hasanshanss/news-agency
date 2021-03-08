using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAgency.DAL.Migrations
{
    public partial class NewsSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "[Title] + ' - ' + convert(nvarchar, CreatedAt, 101)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "[Title] + ' - ' + convert(nvarchar, CreatedAt, 101)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "News");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "[Title] + ' - ' + convert(nvarchar, CreatedAt, 101)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComputedColumnSql: "[Title] + ' - ' + convert(nvarchar, CreatedAt, 101)");
        }
    }
}
