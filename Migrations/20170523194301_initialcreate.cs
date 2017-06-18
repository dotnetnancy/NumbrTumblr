using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VSNumberTumbler.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumberSets",
                columns: table => new
                {
                    NumberSetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsApplicationNumberPool = table.Column<bool>(nullable: false),
                    NumberSetDescription = table.Column<string>(nullable: true),
                    NumberSetMax = table.Column<int>(nullable: false),
                    NumberSetMin = table.Column<int>(nullable: false),
                    NumberSetName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberSets", x => x.NumberSetID);
                });

            migrationBuilder.CreateTable(
                name: "NumberSetNumbers",
                columns: table => new
                {
                    NumberSetNumberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    NumberSetID = table.Column<int>(nullable: false),
                    SelectedNumber = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberSetNumbers", x => x.NumberSetNumberID);
                    table.ForeignKey(
                        name: "FK_NumberSetNumbers_NumberSets_NumberSetID",
                        column: x => x.NumberSetID,
                        principalTable: "NumberSets",
                        principalColumn: "NumberSetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shuffles",
                columns: table => new
                {
                    ShuffleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumberSetID = table.Column<int>(nullable: false),
                    ShuffleDateTime = table.Column<DateTime>(nullable: false),
                    ShuffleDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shuffles", x => x.ShuffleID);
                    table.ForeignKey(
                        name: "FK_Shuffles_NumberSets_NumberSetID",
                        column: x => x.NumberSetID,
                        principalTable: "NumberSets",
                        principalColumn: "NumberSetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShuffleNumbers",
                columns: table => new
                {
                    ShuffleNumberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SelectedNumber = table.Column<bool>(nullable: false),
                    ShuffleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShuffleNumbers", x => x.ShuffleNumberID);
                    table.ForeignKey(
                        name: "FK_ShuffleNumbers_Shuffles_ShuffleID",
                        column: x => x.ShuffleID,
                        principalTable: "Shuffles",
                        principalColumn: "ShuffleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumberSetNumbers_NumberSetID_Number",
                table: "NumberSetNumbers",
                columns: new[] { "NumberSetID", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shuffles_NumberSetID",
                table: "Shuffles",
                column: "NumberSetID");

            migrationBuilder.CreateIndex(
                name: "IX_ShuffleNumbers_ShuffleID_Number",
                table: "ShuffleNumbers",
                columns: new[] { "ShuffleID", "Number" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberSetNumbers");

            migrationBuilder.DropTable(
                name: "ShuffleNumbers");

            migrationBuilder.DropTable(
                name: "Shuffles");

            migrationBuilder.DropTable(
                name: "NumberSets");
        }
    }
}
