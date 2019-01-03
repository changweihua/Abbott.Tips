using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abbott.Tips.EntityFrameworkCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ConfigType = table.Column<int>(nullable: false),
                    ConfigValue = table.Column<string>(nullable: true),
                    ConfigName = table.Column<string>(nullable: true),
                    ConfigDescription = table.Column<string>(nullable: true),
                    EditMode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Configuration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    GroupDescription = table.Column<string>(nullable: true),
                    IsInherited = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    ParentGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Group_T_Group_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalTable: "T_Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MenuName = table.Column<string>(nullable: true),
                    MenuPermission = table.Column<string>(nullable: true),
                    MenuLink = table.Column<string>(nullable: true),
                    MenuController = table.Column<string>(nullable: true),
                    MenuAction = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    ParentMenuId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Menu_T_Menu_ParentMenuId",
                        column: x => x.ParentMenuId,
                        principalTable: "T_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_OperationLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OperationType = table.Column<int>(nullable: false),
                    OpertationTable = table.Column<string>(nullable: true),
                    OperationName = table.Column<string>(nullable: true),
                    OperationField = table.Column<string>(nullable: true),
                    UserADName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OperationLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Organization",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true),
                    OrganizationStatus = table.Column<int>(nullable: false),
                    OrganizationLevel = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    ParentOrganizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Organization_T_Organization_ParentOrganizationId",
                        column: x => x.ParentOrganizationId,
                        principalTable: "T_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Region",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RegionName = table.Column<string>(nullable: true),
                    RegionStatus = table.Column<int>(nullable: false),
                    RegionLevel = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    ParentRegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Region_T_Region_ParentRegionId",
                        column: x => x.ParentRegionId,
                        principalTable: "T_Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    RoleDescription = table.Column<string>(nullable: true),
                    IsInherited = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    ParentRoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Role_T_Role_ParentRoleId",
                        column: x => x.ParentRoleId,
                        principalTable: "T_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoginName = table.Column<string>(nullable: true),
                    LoginPwd = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_RoleMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MenuId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_RoleMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_RoleMenu_T_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "T_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_RoleMenu_T_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "T_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_UserGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UserGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_UserGroup_T_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "T_Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_UserGroup_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_UserRole_T_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "T_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_UserRole_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Group_ParentGroupId",
                table: "T_Group",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Menu_ParentMenuId",
                table: "T_Menu",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Organization_ParentOrganizationId",
                table: "T_Organization",
                column: "ParentOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Region_ParentRegionId",
                table: "T_Region",
                column: "ParentRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Role_ParentRoleId",
                table: "T_Role",
                column: "ParentRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_RoleMenu_MenuId",
                table: "T_RoleMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_T_RoleMenu_RoleId",
                table: "T_RoleMenu",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_UserGroup_GroupId",
                table: "T_UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_T_UserGroup_UserId",
                table: "T_UserGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_T_UserRole_RoleId",
                table: "T_UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_UserRole_UserId",
                table: "T_UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Configuration");

            migrationBuilder.DropTable(
                name: "T_OperationLog");

            migrationBuilder.DropTable(
                name: "T_Organization");

            migrationBuilder.DropTable(
                name: "T_Region");

            migrationBuilder.DropTable(
                name: "T_RoleMenu");

            migrationBuilder.DropTable(
                name: "T_UserGroup");

            migrationBuilder.DropTable(
                name: "T_UserRole");

            migrationBuilder.DropTable(
                name: "T_Menu");

            migrationBuilder.DropTable(
                name: "T_Group");

            migrationBuilder.DropTable(
                name: "T_Role");

            migrationBuilder.DropTable(
                name: "T_User");
        }
    }
}
