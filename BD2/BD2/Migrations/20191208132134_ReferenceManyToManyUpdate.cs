using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2.Migrations
{
    public partial class ReferenceManyToManyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Atributes_AtributeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_GlobalAtributes_GlobalAtributeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Groups_GroupId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_AtributeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_GlobalAtributeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_GroupId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AtributeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GlobalAtributeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "ItemAtributes",
                columns: table => new
                {
                    AtributeId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAtributes", x => new { x.ItemId, x.AtributeId });
                    table.ForeignKey(
                        name: "FK_ItemAtributes_Atributes_AtributeId",
                        column: x => x.AtributeId,
                        principalTable: "Atributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemAtributes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemGlobalAtributes",
                columns: table => new
                {
                    GlobalAtributeId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGlobalAtributes", x => new { x.ItemId, x.GlobalAtributeId });
                    table.ForeignKey(
                        name: "FK_ItemGlobalAtributes_GlobalAtributes_GlobalAtributeId",
                        column: x => x.GlobalAtributeId,
                        principalTable: "GlobalAtributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemGlobalAtributes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemGroups",
                columns: table => new
                {
                    GroupId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroups", x => new { x.ItemId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ItemGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemGroups_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemAtributes_AtributeId",
                table: "ItemAtributes",
                column: "AtributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGlobalAtributes_GlobalAtributeId",
                table: "ItemGlobalAtributes",
                column: "GlobalAtributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroups_GroupId",
                table: "ItemGroups",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemAtributes");

            migrationBuilder.DropTable(
                name: "ItemGlobalAtributes");

            migrationBuilder.DropTable(
                name: "ItemGroups");

            migrationBuilder.AddColumn<long>(
                name: "AtributeId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GlobalAtributeId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_AtributeId",
                table: "Items",
                column: "AtributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_GlobalAtributeId",
                table: "Items",
                column: "GlobalAtributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_GroupId",
                table: "Items",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Atributes_AtributeId",
                table: "Items",
                column: "AtributeId",
                principalTable: "Atributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_GlobalAtributes_GlobalAtributeId",
                table: "Items",
                column: "GlobalAtributeId",
                principalTable: "GlobalAtributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Groups_GroupId",
                table: "Items",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
