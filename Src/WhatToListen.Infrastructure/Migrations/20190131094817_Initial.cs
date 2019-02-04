using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WhatToListen.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "vk");

            migrationBuilder.CreateTable(
                name: "Attachments",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    InstanceId = table.Column<long>(nullable: true),
                    Raw = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Liks = table.Column<int>(nullable: false),
                    RepostsCount = table.Column<int>(nullable: false),
                    CommentsCount = table.Column<int>(nullable: false),
                    ViewsCount = table.Column<int>(nullable: false),
                    PostType = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VkAlbumId = table.Column<long>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    PostId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Audios",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Artist = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    GenreId = table.Column<int>(nullable: true),
                    PostId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audios_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: true),
                    Extentions = table.Column<string>(nullable: true),
                    Uri = table.Column<string>(nullable: true),
                    PostId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Uri = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PostId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Views = table.Column<long>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ViewUrl = table.Column<string>(nullable: true),
                    PostId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PostId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Created = table.Column<DateTime>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    Votes = table.Column<int>(nullable: true),
                    PostId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polls_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostPosts",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ParentPostId = table.Column<long>(nullable: true),
                    ChildPostId = table.Column<long>(nullable: true),
                    UsersCount = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostPosts_Posts_ChildPostId",
                        column: x => x.ChildPostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostPosts_Posts_ParentPostId",
                        column: x => x.ParentPostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: true),
                    PostId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostUsers",
                schema: "vk",
                columns: table => new
                {
                    PostId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUsers", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PostUsers_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "vk",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUsers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "vk",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoSize",
                schema: "vk",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Url = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    AlbumId = table.Column<long>(nullable: true),
                    LinkId = table.Column<long>(nullable: true),
                    PhotoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoSize_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalSchema: "vk",
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoSize_Links_LinkId",
                        column: x => x.LinkId,
                        principalSchema: "vk",
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoSize_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalSchema: "vk",
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_PostId",
                schema: "vk",
                table: "Albums",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Audios_PostId",
                schema: "vk",
                table: "Audios",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PostId",
                schema: "vk",
                table: "Documents",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_PostId",
                schema: "vk",
                table: "Links",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PostId",
                schema: "vk",
                table: "Pages",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PostId",
                schema: "vk",
                table: "Photos",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSize_AlbumId",
                schema: "vk",
                table: "PhotoSize",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSize_LinkId",
                schema: "vk",
                table: "PhotoSize",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSize_PhotoId",
                schema: "vk",
                table: "PhotoSize",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_PostId",
                schema: "vk",
                table: "Polls",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPosts_ChildPostId",
                schema: "vk",
                table: "PostPosts",
                column: "ChildPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPosts_ParentPostId",
                schema: "vk",
                table: "PostPosts",
                column: "ParentPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUsers_UserId",
                schema: "vk",
                table: "PostUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_PostId",
                schema: "vk",
                table: "Videos",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Audios",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Pages",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "PhotoSize",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Polls",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "PostPosts",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "PostUsers",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Videos",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Albums",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Links",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Photos",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "vk");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "vk");
        }
    }
}
