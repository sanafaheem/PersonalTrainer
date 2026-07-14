using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainer.API.Migrations
{
    /// <inheritdoc />
    public partial class ExerciseTemplateEnumRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FocusArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinDurationSeconds = table.Column<int>(type: "int", nullable: false),
                    MaxDurationSeconds = table.Column<int>(type: "int", nullable: false),
                    MinReps = table.Column<int>(type: "int", nullable: true),
                    MaxReps = table.Column<int>(type: "int", nullable: true),
                    MinSets = table.Column<int>(type: "int", nullable: true),
                    MaxSets = table.Column<int>(type: "int", nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Contraindications = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseTemplateId = table.Column<int>(type: "int", nullable: false),
                    EquipmentType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseEquipment_ExerciseTemplates_ExerciseTemplateId",
                        column: x => x.ExerciseTemplateId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseMuscleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseTemplateId = table.Column<int>(type: "int", nullable: false),
                    MuscleGroup = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMuscleGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseMuscleGroups_ExerciseTemplates_ExerciseTemplateId",
                        column: x => x.ExerciseTemplateId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseEquipment_ExerciseTemplateId",
                table: "ExerciseEquipment",
                column: "ExerciseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMuscleGroups_ExerciseTemplateId",
                table: "ExerciseMuscleGroups",
                column: "ExerciseTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseEquipment");

            migrationBuilder.DropTable(
                name: "ExerciseMuscleGroups");

            migrationBuilder.DropTable(
                name: "ExerciseTemplates");
        }
    }
}
