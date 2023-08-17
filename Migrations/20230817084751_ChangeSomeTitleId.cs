using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebChatApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSomeTitleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chats_ChatsChatID",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatID",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ChatID",
                table: "Messages",
                newName: "ChatId");

            migrationBuilder.RenameColumn(
                name: "MessageID",
                table: "Messages",
                newName: "MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatID",
                table: "Messages",
                newName: "IX_Messages_ChatId");

            migrationBuilder.RenameColumn(
                name: "ChatsChatID",
                table: "ChatUser",
                newName: "ChatsChatId");

            migrationBuilder.RenameColumn(
                name: "ChatID",
                table: "Chats",
                newName: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chats_ChatsChatId",
                table: "ChatUser",
                column: "ChatsChatId",
                principalTable: "Chats",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chats_ChatsChatId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Messages",
                newName: "ChatID");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Messages",
                newName: "MessageID");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                newName: "IX_Messages_ChatID");

            migrationBuilder.RenameColumn(
                name: "ChatsChatId",
                table: "ChatUser",
                newName: "ChatsChatID");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Chats",
                newName: "ChatID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chats_ChatsChatID",
                table: "ChatUser",
                column: "ChatsChatID",
                principalTable: "Chats",
                principalColumn: "ChatID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatID",
                table: "Messages",
                column: "ChatID",
                principalTable: "Chats",
                principalColumn: "ChatID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
