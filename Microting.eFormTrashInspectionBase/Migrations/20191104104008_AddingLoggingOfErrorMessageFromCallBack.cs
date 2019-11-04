﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microting.eFormTrashInspectionBase.Migrations
{
    public partial class AddingLoggingOfErrorMessageFromCallBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Setup for SQL Server Provider

            var autoIDGenStrategy = "SqlServer:ValueGenerationStrategy";
            object autoIDGenStrategyValue = SqlServerValueGenerationStrategy.IdentityColumn;

            // Setup for MySQL Provider
            if (migrationBuilder.ActiveProvider == "Pomelo.EntityFrameworkCore.MySql")
            {
                DbConfig.IsMySQL = true;
                autoIDGenStrategy = "MySql:ValueGenerationStrategy";
                autoIDGenStrategyValue = MySqlValueGenerationStrategy.IdentityColumn;
            }
            migrationBuilder.AddColumn<string>(
                name: "ErrorFromCallBack",
                table: "TrashInspectionVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrorFromCallBack",
                table: "TrashInspections",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "PluginGroupPermissions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PluginGroupPermissionVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    PluginGroupPermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginGroupPermissionVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PluginGroupPermissionVersions_PluginPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PluginPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PluginGroupPermissionVersions_PluginGroupPermissions_PluginGroupPermissionId",
                        column: x => x.PluginGroupPermissionId,
                        principalTable: "PluginGroupPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PluginGroupPermissionVersions_PermissionId",
                table: "PluginGroupPermissionVersions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginGroupPermissionVersions_PluginGroupPermissionId",
                table: "PluginGroupPermissionVersions",
                column: "PluginGroupPermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PluginGroupPermissionVersions");

            migrationBuilder.DropColumn(
                name: "ErrorFromCallBack",
                table: "TrashInspectionVersions");

            migrationBuilder.DropColumn(
                name: "ErrorFromCallBack",
                table: "TrashInspections");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "PluginGroupPermissions");
        }
    }
}
