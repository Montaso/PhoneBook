using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    nazwa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.nazwa);
                });

            migrationBuilder.CreateTable(
                name: "subcategories",
                columns: table => new
                {
                    nazwa = table.Column<string>(type: "text", nullable: false),
                    kategoria = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subcategories", x => x.nazwa);
                    table.ForeignKey(
                        name: "FK_subcategories_categories_kategoria",
                        column: x => x.kategoria,
                        principalTable: "categories",
                        principalColumn: "nazwa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    imie = table.Column<string>(type: "text", nullable: false),
                    nazwisko = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    haslo = table.Column<string>(type: "text", nullable: false),
                    telefon = table.Column<string>(type: "text", nullable: false),
                    data_urodzenia = table.Column<DateOnly>(type: "date", nullable: false),
                    podkategoria = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_contacts_subcategories_podkategoria",
                        column: x => x.podkategoria,
                        principalTable: "subcategories",
                        principalColumn: "nazwa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contacts_email",
                table: "contacts",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contacts_podkategoria",
                table: "contacts",
                column: "podkategoria");

            migrationBuilder.CreateIndex(
                name: "IX_subcategories_kategoria",
                table: "subcategories",
                column: "kategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "subcategories");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
