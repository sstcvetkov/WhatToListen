using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatToListen.Infrastructure.Migrations
{
    public partial class PostPostsOptimisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostPosts_Posts_ChildPostId",
                schema: "vk",
                table: "PostPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostPosts_Posts_ParentPostId",
                schema: "vk",
                table: "PostPosts");

            migrationBuilder.DropIndex(
                name: "IX_PostPosts_ChildPostId",
                schema: "vk",
                table: "PostPosts");

            migrationBuilder.DropColumn(
                name: "ChildPostId",
                schema: "vk",
                table: "PostPosts");

            migrationBuilder.RenameColumn(
                name: "ParentPostId",
                schema: "vk",
                table: "PostPosts",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostPosts_ParentPostId",
                schema: "vk",
                table: "PostPosts",
                newName: "IX_PostPosts_PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostPosts_Posts_PostId",
                schema: "vk",
                table: "PostPosts",
                column: "PostId",
                principalSchema: "vk",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostPosts_Posts_PostId",
                schema: "vk",
                table: "PostPosts");

            migrationBuilder.RenameColumn(
                name: "PostId",
                schema: "vk",
                table: "PostPosts",
                newName: "ParentPostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostPosts_PostId",
                schema: "vk",
                table: "PostPosts",
                newName: "IX_PostPosts_ParentPostId");

            migrationBuilder.AddColumn<long>(
                name: "ChildPostId",
                schema: "vk",
                table: "PostPosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostPosts_ChildPostId",
                schema: "vk",
                table: "PostPosts",
                column: "ChildPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostPosts_Posts_ChildPostId",
                schema: "vk",
                table: "PostPosts",
                column: "ChildPostId",
                principalSchema: "vk",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostPosts_Posts_ParentPostId",
                schema: "vk",
                table: "PostPosts",
                column: "ParentPostId",
                principalSchema: "vk",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
