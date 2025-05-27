using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Release_Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Overview = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Popularity = table.Column<double>(type: "REAL", precision: 10, scale: 3, nullable: false),
                    Vote_Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Vote_Average = table.Column<double>(type: "REAL", precision: 3, scale: 1, nullable: false),
                    Original_Language = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Genre = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Poster_Url = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
