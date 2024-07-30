using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenDoctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalSpecialtyId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MedicalSpecialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalSpecialties", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_MedicalSpecialtyId",
                table: "Doctors",
                column: "MedicalSpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_MedicalSpecialties_MedicalSpecialtyId",
                table: "Doctors",
                column: "MedicalSpecialtyId",
                principalTable: "MedicalSpecialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_MedicalSpecialties_MedicalSpecialtyId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "MedicalSpecialties");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_MedicalSpecialtyId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "MedicalSpecialtyId",
                table: "Doctors");
        }
    }
}
