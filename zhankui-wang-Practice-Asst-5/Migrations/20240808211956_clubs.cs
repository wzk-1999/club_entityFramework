using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zhankui_wang_Practice_Asst_5.Migrations
{
    /// <inheritdoc />
    public partial class clubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Provinces",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Province__3214EC2736CD3DBF", x => x.ID);
            //        table.UniqueConstraint("AK_Provinces_Code", x => x.Code);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Cities",
            //    columns: table => new
            //    {
            //        Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        ID = table.Column<int>(type: "int", nullable: false),
            //        ProvCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Cities__737584F6A3497E13", x => x.Name);
            //        table.ForeignKey(
            //            name: "FK_Cities_Provinces",
            //            column: x => x.ProvCode,
            //            principalTable: "Provinces",
            //            principalColumn: "Code",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    ClubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BridgeClubs__3214EC27", x => x.ClubID);
                    table.ForeignKey(
                        name: "FK_Clubs_Cities",
                        column: x => x.CityName,
                        principalTable: "Cities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        PostalCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Users__3214EC275723A073", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_Users_Cities",
            //            column: x => x.CityName,
            //            principalTable: "Cities",
            //            principalColumn: "Name",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "UserClub",
                columns: table => new
                {
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClub", x => new { x.ClubId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserClub_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "ClubID"
                        /*,onDelete: ReferentialAction.Cascade*/);
                    table.ForeignKey(
                        name: "FK_UserClub_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID"
                        /*,onDelete: ReferentialAction.Cascade*/);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Cities_ProvCode",
            //    table: "Cities",
            //    column: "ProvCode");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_CityName",
                table: "Clubs",
                column: "CityName");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Provinces_Code",
            //    table: "Provinces",
            //    column: "Code",
            //    unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClub_UserId",
                table: "UserClub",
                column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_CityName",
            //    table: "Users",
            //    column: "CityName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClub");

            migrationBuilder.DropTable(
                name: "Clubs");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "Cities");

            //migrationBuilder.DropTable(
            //    name: "Provinces");
        }
    }
}
