using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaInfrastructure.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    Telefono = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Publicacion",
                columns: table => new
                {
                    IdPublicacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    Imagen = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicacion", x => x.IdPublicacion);
                    table.ForeignKey(
                        name: "FK_Publicacion_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    IdComentario = table.Column<int>(nullable: false),
                    idPublicacion = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK_Comentario_Publicacion",
                        column: x => x.idPublicacion,
                        principalTable: "Publicacion",
                        principalColumn: "IdPublicacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentario_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_idPublicacion",
                table: "Comentario",
                column: "idPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_IdUsuario",
                table: "Comentario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacion_IdUsuario",
                table: "Publicacion",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Publicacion");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
