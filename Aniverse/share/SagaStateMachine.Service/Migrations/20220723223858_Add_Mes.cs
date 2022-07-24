using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SagaStateMachine.Service.Migrations
{
    public partial class Add_Mes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageStateId",
                table: "PostStateInstance",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MessageState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostStateInstance_MessageStateId",
                table: "PostStateInstance",
                column: "MessageStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostStateInstance_MessageState_MessageStateId",
                table: "PostStateInstance",
                column: "MessageStateId",
                principalTable: "MessageState",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostStateInstance_MessageState_MessageStateId",
                table: "PostStateInstance");

            migrationBuilder.DropTable(
                name: "MessageState");

            migrationBuilder.DropIndex(
                name: "IX_PostStateInstance_MessageStateId",
                table: "PostStateInstance");

            migrationBuilder.DropColumn(
                name: "MessageStateId",
                table: "PostStateInstance");
        }
    }
}
