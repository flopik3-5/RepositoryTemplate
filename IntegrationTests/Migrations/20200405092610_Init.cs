using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationTests.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StringProperty = table.Column<string>(nullable: true),
                    IntegerProperty = table.Column<int>(nullable: false),
                    FloatProperty = table.Column<float>(nullable: false),
                    DoubleProperty = table.Column<double>(nullable: false),
                    DecimalProperty = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestEntities");
        }
    }
}
