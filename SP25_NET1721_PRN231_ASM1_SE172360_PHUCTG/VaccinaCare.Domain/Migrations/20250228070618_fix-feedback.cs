using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinaCare.Domain.Migrations
{
    /// <inheritdoc />
    public partial class fixfeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_FeedbackType_FeedbackTypeId",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackType",
                table: "FeedbackType");

            migrationBuilder.RenameTable(
                name: "FeedbackType",
                newName: "FeedbackTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackTypes",
                table: "FeedbackTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_FeedbackTypes_FeedbackTypeId",
                table: "Feedback",
                column: "FeedbackTypeId",
                principalTable: "FeedbackTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_FeedbackTypes_FeedbackTypeId",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackTypes",
                table: "FeedbackTypes");

            migrationBuilder.RenameTable(
                name: "FeedbackTypes",
                newName: "FeedbackType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackType",
                table: "FeedbackType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_FeedbackType_FeedbackTypeId",
                table: "Feedback",
                column: "FeedbackTypeId",
                principalTable: "FeedbackType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
