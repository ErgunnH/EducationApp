using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationApp.Migrations
{
    /// <inheritdoc />
    public partial class first7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentRequests_Students_StudentId",
                table: "EnrollmentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentRequests_Trainings_TrainingId",
                table: "EnrollmentRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentRequests",
                table: "EnrollmentRequests");

            migrationBuilder.RenameTable(
                name: "EnrollmentRequests",
                newName: "EnrollmentRequest");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentRequests_TrainingId",
                table: "EnrollmentRequest",
                newName: "IX_EnrollmentRequest_TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentRequests_StudentId",
                table: "EnrollmentRequest",
                newName: "IX_EnrollmentRequest_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentRequest",
                table: "EnrollmentRequest",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentRequest_Students_StudentId",
                table: "EnrollmentRequest",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentRequest_Trainings_TrainingId",
                table: "EnrollmentRequest",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "TrainingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentRequest_Students_StudentId",
                table: "EnrollmentRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentRequest_Trainings_TrainingId",
                table: "EnrollmentRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentRequest",
                table: "EnrollmentRequest");

            migrationBuilder.RenameTable(
                name: "EnrollmentRequest",
                newName: "EnrollmentRequests");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentRequest_TrainingId",
                table: "EnrollmentRequests",
                newName: "IX_EnrollmentRequests_TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentRequest_StudentId",
                table: "EnrollmentRequests",
                newName: "IX_EnrollmentRequests_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentRequests",
                table: "EnrollmentRequests",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentRequests_Students_StudentId",
                table: "EnrollmentRequests",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentRequests_Trainings_TrainingId",
                table: "EnrollmentRequests",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "TrainingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
