using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoremIpsumLogistica.Server.Migrations
{
    /// <inheritdoc />
    public partial class migration_v001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bck");

            migrationBuilder.CreateTable(
                name: "TB_CUSTOMER",
                schema: "bck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CUSTOMER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_ADDRESSES",
                schema: "bck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Complement = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Neighborhood = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ADDRESSES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ADDRESSES_TB_CUSTOMER_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "bck",
                        principalTable: "TB_CUSTOMER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ADDRESSES_CustomerId",
                schema: "bck",
                table: "TB_ADDRESSES",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ADDRESSES",
                schema: "bck");

            migrationBuilder.DropTable(
                name: "TB_CUSTOMER",
                schema: "bck");
        }
    }
}
