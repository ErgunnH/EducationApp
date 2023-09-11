using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationApp.Migrations
{
    /// <inheritdoc />
    public partial class first5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "RequestStatus",
                table: "EnrollmentRequests",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestStatus",
                table: "EnrollmentRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
