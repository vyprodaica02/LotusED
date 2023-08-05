using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaiThucTaplan1.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chuas",
                columns: table => new
                {
                    chuaid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    capnhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaythanhlap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tenchua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trutri = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chuas", x => x.chuaid);
                });

            migrationBuilder.CreateTable(
                name: "daoTrangs",
                columns: table => new
                {
                    daotrangid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    daketthuc = table.Column<bool>(type: "bit", nullable: true),
                    noidung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    noitochuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sothanhvienthamgia = table.Column<int>(type: "int", nullable: true),
                    thoigiantochuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nguoitrutri = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_daoTrangs", x => x.daotrangid);
                });

            migrationBuilder.CreateTable(
                name: "Kieuthanhviens",
                columns: table => new
                {
                    kieuthanhvienid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tenkieu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kieuthanhviens", x => x.kieuthanhvienid);
                });

            migrationBuilder.CreateTable(
                name: "Phantus",
                columns: table => new
                {
                    phantuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anhchup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dahoantuc = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gioitinh = table.Column<int>(type: "int", nullable: true),
                    ho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaycapnhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayketthuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngaysinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayxuatgia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phapdanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sodienthoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tendem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    roll = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chuaid = table.Column<int>(type: "int", nullable: true),
                    chuaschuaid = table.Column<int>(type: "int", nullable: true),
                    kieuthanhvienid = table.Column<int>(type: "int", nullable: true),
                    kieuthanhvienskieuthanhvienid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phantus", x => x.phantuid);
                    table.ForeignKey(
                        name: "FK_Phantus_Chuas_chuaschuaid",
                        column: x => x.chuaschuaid,
                        principalTable: "Chuas",
                        principalColumn: "chuaid");
                    table.ForeignKey(
                        name: "FK_Phantus_Kieuthanhviens_kieuthanhvienskieuthanhvienid",
                        column: x => x.kieuthanhvienskieuthanhvienid,
                        principalTable: "Kieuthanhviens",
                        principalColumn: "kieuthanhvienid");
                });

            migrationBuilder.CreateTable(
                name: "Dondangkys",
                columns: table => new
                {
                    dondangkyid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ngayguidon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayxuly = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nguoixuly = table.Column<int>(type: "int", nullable: true),
                    trangthaidon = table.Column<int>(type: "int", nullable: true),
                    daotrangid = table.Column<int>(type: "int", nullable: true),
                    phantuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dondangkys", x => x.dondangkyid);
                    table.ForeignKey(
                        name: "FK_Dondangkys_daoTrangs_daotrangid",
                        column: x => x.daotrangid,
                        principalTable: "daoTrangs",
                        principalColumn: "daotrangid");
                    table.ForeignKey(
                        name: "FK_Dondangkys_Phantus_phantuid",
                        column: x => x.phantuid,
                        principalTable: "Phantus",
                        principalColumn: "phantuid");
                });

            migrationBuilder.CreateTable(
                name: "Phantudaotrangs",
                columns: table => new
                {
                    phantudaotrangid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dathamgia = table.Column<bool>(type: "bit", nullable: true),
                    lydokhongthamgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    daotrangid = table.Column<int>(type: "int", nullable: true),
                    phantuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phantudaotrangs", x => x.phantudaotrangid);
                    table.ForeignKey(
                        name: "FK_Phantudaotrangs_daoTrangs_daotrangid",
                        column: x => x.daotrangid,
                        principalTable: "daoTrangs",
                        principalColumn: "daotrangid");
                    table.ForeignKey(
                        name: "FK_Phantudaotrangs_Phantus_phantuid",
                        column: x => x.phantuid,
                        principalTable: "Phantus",
                        principalColumn: "phantuid");
                });

            migrationBuilder.CreateTable(
                name: "rolllNumbers",
                columns: table => new
                {
                    rollnumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phantuid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolllNumbers", x => x.rollnumber);
                    table.ForeignKey(
                        name: "FK_rolllNumbers_Phantus_phantuid",
                        column: x => x.phantuid,
                        principalTable: "Phantus",
                        principalColumn: "phantuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dondangkys_daotrangid",
                table: "Dondangkys",
                column: "daotrangid");

            migrationBuilder.CreateIndex(
                name: "IX_Dondangkys_phantuid",
                table: "Dondangkys",
                column: "phantuid");

            migrationBuilder.CreateIndex(
                name: "IX_Phantudaotrangs_daotrangid",
                table: "Phantudaotrangs",
                column: "daotrangid");

            migrationBuilder.CreateIndex(
                name: "IX_Phantudaotrangs_phantuid",
                table: "Phantudaotrangs",
                column: "phantuid");

            migrationBuilder.CreateIndex(
                name: "IX_Phantus_chuaschuaid",
                table: "Phantus",
                column: "chuaschuaid");

            migrationBuilder.CreateIndex(
                name: "IX_Phantus_kieuthanhvienskieuthanhvienid",
                table: "Phantus",
                column: "kieuthanhvienskieuthanhvienid");

            migrationBuilder.CreateIndex(
                name: "IX_rolllNumbers_phantuid",
                table: "rolllNumbers",
                column: "phantuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dondangkys");

            migrationBuilder.DropTable(
                name: "Phantudaotrangs");

            migrationBuilder.DropTable(
                name: "rolllNumbers");

            migrationBuilder.DropTable(
                name: "daoTrangs");

            migrationBuilder.DropTable(
                name: "Phantus");

            migrationBuilder.DropTable(
                name: "Chuas");

            migrationBuilder.DropTable(
                name: "Kieuthanhviens");
        }
    }
}
