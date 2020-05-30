using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reponsitory.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article_ReadHistory",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    gmt_create = table.Column<DateTime>(nullable: false),
                    gmt_modified = table.Column<DateTime>(nullable: false),
                    is_delete = table.Column<int>(nullable: false),
                    article_id = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article_ReadHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Article_Record",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    gmt_create = table.Column<DateTime>(nullable: false),
                    gmt_modified = table.Column<DateTime>(nullable: false),
                    is_delete = table.Column<int>(nullable: false),
                    article_url = table.Column<string>(maxLength: 500, nullable: false),
                    article_title_auto = table.Column<string>(maxLength: 40, nullable: false),
                    article_title_self = table.Column<string>(maxLength: 40, nullable: true),
                    article_cdnurl_auto = table.Column<string>(maxLength: 500, nullable: false),
                    article_cdnurl_self = table.Column<string>(maxLength: 500, nullable: true),
                    article_userid = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article_Record", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Info",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    gmt_create = table.Column<DateTime>(nullable: false),
                    gmt_modified = table.Column<DateTime>(nullable: false),
                    is_delete = table.Column<int>(nullable: false),
                    wechat_code = table.Column<string>(maxLength: 200, nullable: true),
                    profile_pic = table.Column<string>(maxLength: 500, nullable: true),
                    user_nickname = table.Column<string>(maxLength: 100, nullable: true),
                    user_phone = table.Column<string>(maxLength: 11, nullable: true),
                    fatiecard_nm = table.Column<int>(nullable: false),
                    invite_code = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Article_Status",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    gmt_create = table.Column<DateTime>(nullable: false),
                    gmt_modified = table.Column<DateTime>(nullable: false),
                    is_delete = table.Column<int>(nullable: false),
                    article_id = table.Column<string>(maxLength: 50, nullable: true),
                    article_status = table.Column<int>(nullable: false),
                    article_read_number = table.Column<int>(nullable: false),
                    article_read_max = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Status_Article_Record_article_id",
                        column: x => x.article_id,
                        principalTable: "Article_Record",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fatiecard_Detail",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    gmt_create = table.Column<DateTime>(nullable: false),
                    gmt_modified = table.Column<DateTime>(nullable: false),
                    is_delete = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(maxLength: 50, nullable: true),
                    fatiecard_acquire_spend = table.Column<int>(nullable: false),
                    fatiecard_num = table.Column<int>(nullable: false),
                    fatiecard_channel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatiecard_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fatiecard_Detail_User_Info_user_id",
                        column: x => x.user_id,
                        principalTable: "User_Info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_Status_article_id",
                table: "Article_Status",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fatiecard_Detail_user_id",
                table: "Fatiecard_Detail",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article_ReadHistory");

            migrationBuilder.DropTable(
                name: "Article_Status");

            migrationBuilder.DropTable(
                name: "Fatiecard_Detail");

            migrationBuilder.DropTable(
                name: "Article_Record");

            migrationBuilder.DropTable(
                name: "User_Info");
        }
    }
}
