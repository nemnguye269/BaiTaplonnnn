using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebKhoaHoc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleCustom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhoaHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhoaHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HocPhi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiangVienId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DanhMucId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoaHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhoaHoc_AspNetUsers_GiangVienId",
                        column: x => x.GiangVienId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KhoaHoc_DanhMuc_DanhMucId",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaiHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhoaHocId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaiHoc_KhoaHoc_KhoaHocId",
                        column: x => x.KhoaHocId,
                        principalTable: "KhoaHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoSao = table.Column<int>(type: "int", nullable: false),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KhoaHocId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhGia_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGia_KhoaHoc_KhoaHocId",
                        column: x => x.KhoaHocId,
                        principalTable: "KhoaHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KhoaHocId = table.Column<int>(type: "int", nullable: false),
                    NgayMua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonHang_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHang_KhoaHoc_KhoaHocId",
                        column: x => x.KhoaHocId,
                        principalTable: "KhoaHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangId = table.Column<int>(type: "int", nullable: false),
                    KhoaHocId = table.Column<int>(type: "int", nullable: false),
                    GiaMua = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_DonHang_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "DonHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_KhoaHoc_KhoaHocId",
                        column: x => x.KhoaHocId,
                        principalTable: "KhoaHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "HoTen", "LockoutEnabled", "LockoutEnd", "NgayTao", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleCustom", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "c6ed445c-f7e8-4f4e-9983-d8683bf031ad", "gv1@web.com", false, "Giảng viên 1", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5411), null, "INSTRUCTOR1", null, null, false, null, "b90dc908-41f7-4438-b2e9-89d8bdfc5f80", false, "instructor1" },
                    { "10", 0, "09413103-7a12-4305-8e5d-278089ad81de", "sv5@web.com", false, "Học viên 5", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5639), null, "STUDENT5", null, null, false, null, "80817a60-be26-48fd-85d6-e75816c01824", false, "student5" },
                    { "11", 0, "4c560139-6728-4203-bf0c-1a58e2dc4fb0", "sv6@web.com", false, "Học viên 6", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5651), null, "STUDENT6", null, null, false, null, "50f25c1e-abbd-48fa-a35c-57e94c309ae3", false, "student6" },
                    { "12", 0, "ee62b23d-8dbd-4a31-884e-30e07dd3820c", "sv7@web.com", false, "Học viên 7", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5662), null, "STUDENT7", null, null, false, null, "854fe973-fad0-418e-b7c5-3ab4192b28e0", false, "student7" },
                    { "13", 0, "079af3c2-fa03-45d6-a2b6-bb0089c79c04", "sv8@web.com", false, "Học viên 8", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5672), null, "STUDENT8", null, null, false, null, "9114626a-67e3-4ca2-a340-9ac75e16bf6a", false, "student8" },
                    { "14", 0, "9964e5f0-5546-4a82-8bf6-0b59f7515772", "sv9@web.com", false, "Học viên 9", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5683), null, "STUDENT9", null, null, false, null, "40cce070-a321-4356-bbb8-705158dde623", false, "student9" },
                    { "15", 0, "930d0506-4811-4157-89f9-8944f8faace1", "sv10@web.com", false, "Học viên 10", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5693), null, "STUDENT10", null, null, false, null, "0e459797-f72e-4341-858f-9d8fe09d63a6", false, "student10" },
                    { "16", 0, "c338bae7-23ae-4af7-be32-6efd43012b0e", "sv11@web.com", false, "Học viên 11", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5704), null, "STUDENT11", null, null, false, null, "6f972676-2e73-4183-8191-bd05cee581a9", false, "student11" },
                    { "17", 0, "65ea71dc-c8e0-4ff7-aebd-2cea77913700", "sv12@web.com", false, "Học viên 12", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5715), null, "STUDENT12", null, null, false, null, "31ee30ce-b559-423c-b4c7-a593d6ee8b53", false, "student12" },
                    { "18", 0, "349f96a6-689e-4765-8e59-b0b55894a76a", "sv13@web.com", false, "Học viên 13", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5726), null, "STUDENT13", null, null, false, null, "fd52b221-51a2-43af-8825-4b42d6b36529", false, "student13" },
                    { "19", 0, "52346563-e157-4d6f-bb74-07a4ee02d95d", "sv14@web.com", false, "Học viên 14", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5738), null, "STUDENT14", null, null, false, null, "b86983af-0e17-4177-816a-2b9e7b1dcec7", false, "student14" },
                    { "2", 0, "bce1a06f-7b35-41bf-8023-6654edc3ecb1", "gv2@web.com", false, "Giảng viên 2", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5506), null, "INSTRUCTOR2", null, null, false, null, "c3f9a7b1-93fc-4599-99f1-efa8ac2c1e0d", false, "instructor2" },
                    { "20", 0, "7b570688-5ff6-45ef-b892-39951ad5a745", "sv15@web.com", false, "Học viên 15", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5748), null, "STUDENT15", null, null, false, null, "05c51172-a2a1-4622-a1bf-d7faa4e4ca78", false, "student15" },
                    { "21", 0, "3bc4b0ef-0063-4ea6-a7a1-ebc5271adb07", "sv16@web.com", false, "Học viên 16", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5757), null, "STUDENT16", null, null, false, null, "56e2b1a1-bc42-4241-8c74-fddb5e502d49", false, "student16" },
                    { "22", 0, "8398eb1e-7df2-4fcd-b049-8c6a734f0f91", "sv17@web.com", false, "Học viên 17", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5769), null, "STUDENT17", null, null, false, null, "a380408b-4940-4258-a0c9-5ee7bec1925f", false, "student17" },
                    { "23", 0, "3d842e00-3988-4a94-9e0c-8950fe85ef39", "sv18@web.com", false, "Học viên 18", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5801), null, "STUDENT18", null, null, false, null, "c59f791c-20d7-46ba-ac7f-8bd7cf633c1a", false, "student18" },
                    { "24", 0, "5c899c4d-ab40-4e66-9c05-caefc05c5030", "sv19@web.com", false, "Học viên 19", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5811), null, "STUDENT19", null, null, false, null, "b9b46270-b50a-4144-87e4-22a4621c6880", false, "student19" },
                    { "25", 0, "764d7f81-416e-4e57-b3ed-cbafa8b5f5cc", "sv20@web.com", false, "Học viên 20", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5822), null, "STUDENT20", null, null, false, null, "1edf3068-8c5e-413d-8cfb-7abd5f01f8d6", false, "student20" },
                    { "26", 0, "798caf5c-e88d-4fc5-bcf1-4f09e673d764", "sv21@web.com", false, "Học viên 21", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5832), null, "STUDENT21", null, null, false, null, "770043f4-eede-4c15-a1a6-fd7ca9967766", false, "student21" },
                    { "27", 0, "01dc15bd-1d1d-46d6-8434-c3b8652a98dd", "sv22@web.com", false, "Học viên 22", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5844), null, "STUDENT22", null, null, false, null, "39cfca94-f687-4875-ad48-e51df0927b9d", false, "student22" },
                    { "28", 0, "53759e03-e353-4976-9ff7-3776446fe605", "sv23@web.com", false, "Học viên 23", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5853), null, "STUDENT23", null, null, false, null, "5a7577a4-2456-44b4-bd85-0ae498d41b93", false, "student23" },
                    { "29", 0, "3cfb6545-846c-4288-a77d-70eef9fa00f5", "sv24@web.com", false, "Học viên 24", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5863), null, "STUDENT24", null, null, false, null, "8be489c5-2f2d-4504-8eca-91c4885a5c8a", false, "student24" },
                    { "3", 0, "d2cfbfa9-055b-45d7-b7d3-3d9d82fe6c1e", "gv3@web.com", false, "Giảng viên 3", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5527), null, "INSTRUCTOR3", null, null, false, null, "6686195d-08fd-430d-9261-879f2d000be9", false, "instructor3" },
                    { "30", 0, "c2969c2c-5394-41b6-8574-d98624a66e67", "sv25@web.com", false, "Học viên 25", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5875), null, "STUDENT25", null, null, false, null, "8e6fc459-ceee-461f-b8fa-cd5571d74479", false, "student25" },
                    { "31", 0, "178ff10b-cca2-4b1d-94f8-53b6ea461632", "sv26@web.com", false, "Học viên 26", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5885), null, "STUDENT26", null, null, false, null, "69ef572f-7c2b-4747-93cd-d97c022c9c31", false, "student26" },
                    { "32", 0, "13dbd9c0-7212-421d-87b9-122cf8a14997", "sv27@web.com", false, "Học viên 27", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5895), null, "STUDENT27", null, null, false, null, "0aa57494-272d-4797-8119-ccf9176aa10f", false, "student27" },
                    { "33", 0, "db6444ed-2d1c-48f9-b1fe-89951611b28a", "sv28@web.com", false, "Học viên 28", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5906), null, "STUDENT28", null, null, false, null, "d75986c8-c9c0-4ebb-950e-603548d1e3d2", false, "student28" },
                    { "34", 0, "d3143b30-03f2-47c0-8f41-fb161daba1db", "sv29@web.com", false, "Học viên 29", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5916), null, "STUDENT29", null, null, false, null, "96289f1b-c2b4-4abc-884e-5d99fed55c4a", false, "student29" },
                    { "35", 0, "0c35c31f-76e3-4d9c-bf95-fbe26b021ff7", "sv30@web.com", false, "Học viên 30", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5950), null, "STUDENT30", null, null, false, null, "c511476c-e14b-48b0-80e6-8f7b78de2253", false, "student30" },
                    { "36", 0, "6d425d7b-df2f-46a5-816a-9c2f8a9cf0b8", "sv31@web.com", false, "Học viên 31", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5960), null, "STUDENT31", null, null, false, null, "2d757dd0-86a6-464b-bf25-9907abb47717", false, "student31" },
                    { "37", 0, "77093af7-e447-4437-89c5-2d811fb4cb8b", "sv32@web.com", false, "Học viên 32", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5971), null, "STUDENT32", null, null, false, null, "0d56a797-d5c0-4b93-bdca-dfbd0c8bef17", false, "student32" },
                    { "38", 0, "f1fcffff-fc5b-4442-b345-8c746d5dbe77", "sv33@web.com", false, "Học viên 33", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5983), null, "STUDENT33", null, null, false, null, "165b6b5a-9087-4d34-859a-efc9dc23e5a5", false, "student33" },
                    { "39", 0, "5e45e6cf-8b3c-4cdd-9ded-6706b7a08b70", "sv34@web.com", false, "Học viên 34", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5993), null, "STUDENT34", null, null, false, null, "619ebd82-d0b1-4132-bbb7-60d95c351e92", false, "student34" },
                    { "4", 0, "a978b1d9-7bc8-48ec-81bd-0246b8eafc20", "gv4@web.com", false, "Giảng viên 4", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5538), null, "INSTRUCTOR4", null, null, false, null, "7cecf8a9-a52a-4b44-a0bf-00333e1dae4a", false, "instructor4" },
                    { "40", 0, "cfe6044b-e00f-4999-b107-eb2f415fcd51", "sv35@web.com", false, "Học viên 35", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6003), null, "STUDENT35", null, null, false, null, "f1e2ba17-9cab-46a0-bc19-cb3d5fc4e466", false, "student35" },
                    { "41", 0, "07fe2f21-e69f-4d31-85ed-a70bb5fb46f2", "sv36@web.com", false, "Học viên 36", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6014), null, "STUDENT36", null, null, false, null, "16e5191a-b8a8-4414-aa48-438598f3edf2", false, "student36" },
                    { "42", 0, "2a553b56-0620-4d9b-8d58-98593cb60829", "sv37@web.com", false, "Học viên 37", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6024), null, "STUDENT37", null, null, false, null, "e3cfb362-2272-463c-adc0-f70e11e2a150", false, "student37" },
                    { "43", 0, "31ad6c9d-dd6d-4f45-86cd-9a5f5038f5ce", "sv38@web.com", false, "Học viên 38", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6035), null, "STUDENT38", null, null, false, null, "a49b38e8-9013-435c-964f-9ca2ca35c520", false, "student38" },
                    { "44", 0, "d8aecd3a-1240-4387-8230-e78cc4b96b45", "sv39@web.com", false, "Học viên 39", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6045), null, "STUDENT39", null, null, false, null, "a269fde5-5b6f-467a-afc3-1ae35c5ecab0", false, "student39" },
                    { "45", 0, "22de6ab3-2aaa-4fe9-a2f5-a4131bb43194", "sv40@web.com", false, "Học viên 40", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6055), null, "STUDENT40", null, null, false, null, "e38175a4-c4c6-4e47-adca-470b64a608b1", false, "student40" },
                    { "46", 0, "948be369-0e64-4cff-a3f0-b0d5efc28d7c", "sv41@web.com", false, "Học viên 41", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6066), null, "STUDENT41", null, null, false, null, "3a06c663-913e-47af-91fa-829886670512", false, "student41" },
                    { "47", 0, "4e598eb8-fcdd-4124-843d-5329ff58f940", "sv42@web.com", false, "Học viên 42", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6076), null, "STUDENT42", null, null, false, null, "fa75d770-c649-4ed7-993b-ad64c31f494c", false, "student42" },
                    { "48", 0, "f77734a2-468a-4cf6-be76-237d0914707b", "sv43@web.com", false, "Học viên 43", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6086), null, "STUDENT43", null, null, false, null, "18369f7a-ddd8-41cb-81f1-db691af8f362", false, "student43" },
                    { "49", 0, "4e68bae9-060d-4c09-96eb-85cb34c0fcfb", "sv44@web.com", false, "Học viên 44", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6120), null, "STUDENT44", null, null, false, null, "d565a66b-da25-49bf-9319-5b34c2e614ba", false, "student44" },
                    { "5", 0, "7f75ecd7-fbc3-43e6-8068-2abe577e55d8", "gv5@web.com", false, "Giảng viên 5", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5549), null, "INSTRUCTOR5", null, null, false, null, "037c8229-c7a3-4866-9907-b44090919f6b", false, "instructor5" },
                    { "50", 0, "463eb809-b9ee-4900-bba0-01f3ad0e0f71", "sv45@web.com", false, "Học viên 45", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6130), null, "STUDENT45", null, null, false, null, "d7dd53d7-df03-4ef7-afea-2a77a540738f", false, "student45" },
                    { "51", 0, "d729d82a-7281-4190-9072-3f57dccc324f", "sv46@web.com", false, "Học viên 46", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6141), null, "STUDENT46", null, null, false, null, "4d6a6665-c367-4eff-84d5-0348e1c38f23", false, "student46" },
                    { "52", 0, "36908c74-6070-44cf-9fbc-7ea75512e4f4", "sv47@web.com", false, "Học viên 47", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6151), null, "STUDENT47", null, null, false, null, "369b3742-d5dd-4f7b-9cd3-88e01adc7179", false, "student47" },
                    { "53", 0, "b6bf5bae-ab57-43f3-9d6a-854248791fc3", "sv48@web.com", false, "Học viên 48", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6161), null, "STUDENT48", null, null, false, null, "dfd03680-4b43-43fd-8c6f-741c41c9dd7b", false, "student48" },
                    { "54", 0, "79896949-1047-4270-8620-7fcf3408a262", "sv49@web.com", false, "Học viên 49", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6172), null, "STUDENT49", null, null, false, null, "4f6b7f07-30f6-484a-b7f3-8ab06f416c06", false, "student49" },
                    { "55", 0, "756e848f-d300-4c46-84a4-57237d3a1cd3", "sv50@web.com", false, "Học viên 50", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6182), null, "STUDENT50", null, null, false, null, "c01267d7-a391-4d81-84f5-4dbb2b3eabbf", false, "student50" },
                    { "6", 0, "fc5f8772-9fb2-42af-8c15-49569e30426c", "sv1@web.com", false, "Học viên 1", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5562), null, "STUDENT1", null, null, false, null, "df58e994-27e3-49d3-9f6c-3b5535d3e3e9", false, "student1" },
                    { "7", 0, "71762e25-8381-4e97-835a-e7022b064048", "sv2@web.com", false, "Học viên 2", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5574), null, "STUDENT2", null, null, false, null, "8665d22e-6714-48e9-abc8-9593b6b510f0", false, "student2" },
                    { "8", 0, "c0d1cfed-07c3-4d5c-8bcf-b6befd9ccee5", "sv3@web.com", false, "Học viên 3", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5585), null, "STUDENT3", null, null, false, null, "964f3a4c-d488-46da-879b-4a203e039d68", false, "student3" },
                    { "9", 0, "4c24715a-f9d9-49e5-b021-c4f120b4e486", "sv4@web.com", false, "Học viên 4", false, null, new DateTime(2026, 3, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(5596), null, "STUDENT4", null, null, false, null, "5694c36c-5151-4636-a143-209291a2e4c4", false, "student4" }
                });

            migrationBuilder.InsertData(
                table: "DanhMuc",
                columns: new[] { "Id", "MoTa", "TenDanhMuc" },
                values: new object[,]
                {
                    { 1, "ASP.NET Core, React, Angular", "Lập trình Web" },
                    { 2, "Android, iOS, Flutter", "Lập trình Mobile" },
                    { 3, "Python, AI, Machine Learning", "Data Science" },
                    { 4, "Photoshop, UI/UX", "Thiết kế Đồ họa" },
                    { 5, "Tiếng Anh cho lập trình viên", "Ngoại ngữ" }
                });

            migrationBuilder.InsertData(
                table: "KhoaHoc",
                columns: new[] { "Id", "DanhMucId", "HocPhi", "GiangVienId", "HinhAnh", "MoTa", "TenKhoaHoc", "VideoUrl" },
                values: new object[,]
                {
                    { 1, 5, 510000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 1.", "Khóa học chuyên sâu số 1", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 2, 4, 200000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 2.", "Khóa học chuyên sâu số 2", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 3, 3, 660000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 3.", "Khóa học chuyên sâu số 3", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 4, 2, 720000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 4.", "Khóa học chuyên sâu số 4", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 5, 3, 740000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 5.", "Khóa học chuyên sâu số 5", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 6, 5, 560000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 6.", "Khóa học chuyên sâu số 6", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 7, 3, 740000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 7.", "Khóa học chuyên sâu số 7", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 8, 1, 520000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 8.", "Khóa học chuyên sâu số 8", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 9, 4, 790000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 9.", "Khóa học chuyên sâu số 9", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 10, 5, 690000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 10.", "Khóa học chuyên sâu số 10", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 11, 1, 700000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 11.", "Khóa học chuyên sâu số 11", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 12, 3, 460000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 12.", "Khóa học chuyên sâu số 12", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 13, 2, 210000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 13.", "Khóa học chuyên sâu số 13", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 14, 5, 560000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 14.", "Khóa học chuyên sâu số 14", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 15, 5, 420000m, null, "course_default.jpg", "Mô tả chi tiết cho khóa học thứ 15.", "Khóa học chuyên sâu số 15", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" }
                });

            migrationBuilder.InsertData(
                table: "BaiHoc",
                columns: new[] { "Id", "KhoaHocId", "NoiDung", "TieuDe", "VideoUrl" },
                values: new object[,]
                {
                    { 1, 1, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 1", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 2, 1, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 1", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 3, 1, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 1", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 4, 2, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 2", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 5, 2, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 2", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 6, 2, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 2", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 7, 3, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 3", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 8, 3, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 3", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 9, 3, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 3", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 10, 3, "Nội dung bài học mẫu...", "Bài 4: Kiến thức nền tảng của khóa 3", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 11, 4, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 4", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 12, 4, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 4", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 13, 4, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 4", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 14, 4, "Nội dung bài học mẫu...", "Bài 4: Kiến thức nền tảng của khóa 4", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 15, 5, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 5", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 16, 5, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 5", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 17, 5, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 5", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 18, 6, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 6", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 19, 6, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 6", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 20, 6, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 6", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 21, 7, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 7", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 22, 7, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 7", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 23, 7, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 7", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 24, 8, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 8", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 25, 8, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 8", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 26, 8, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 8", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 27, 9, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 9", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 28, 9, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 9", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 29, 9, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 9", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 30, 9, "Nội dung bài học mẫu...", "Bài 4: Kiến thức nền tảng của khóa 9", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 31, 9, "Nội dung bài học mẫu...", "Bài 5: Kiến thức nền tảng của khóa 9", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 32, 10, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 10", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 33, 10, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 10", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 34, 10, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 10", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 35, 10, "Nội dung bài học mẫu...", "Bài 4: Kiến thức nền tảng của khóa 10", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 36, 10, "Nội dung bài học mẫu...", "Bài 5: Kiến thức nền tảng của khóa 10", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 37, 11, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 11", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 38, 11, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 11", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 39, 11, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 11", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 40, 11, "Nội dung bài học mẫu...", "Bài 4: Kiến thức nền tảng của khóa 11", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 41, 11, "Nội dung bài học mẫu...", "Bài 5: Kiến thức nền tảng của khóa 11", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 42, 12, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 12", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 43, 12, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 12", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 44, 12, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 12", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 45, 13, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 13", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 46, 13, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 13", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 47, 13, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 13", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 48, 13, "Nội dung bài học mẫu...", "Bài 4: Kiến thức nền tảng của khóa 13", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 49, 13, "Nội dung bài học mẫu...", "Bài 5: Kiến thức nền tảng của khóa 13", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 50, 14, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 14", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 51, 14, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 14", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 52, 14, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 14", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 53, 14, "Nội dung bài học mẫu...", "Bài 4: Kiến thức nền tảng của khóa 14", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 54, 14, "Nội dung bài học mẫu...", "Bài 5: Kiến thức nền tảng của khóa 14", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 55, 15, "Nội dung bài học mẫu...", "Bài 1: Kiến thức nền tảng của khóa 15", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 56, 15, "Nội dung bài học mẫu...", "Bài 2: Kiến thức nền tảng của khóa 15", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 57, 15, "Nội dung bài học mẫu...", "Bài 3: Kiến thức nền tảng của khóa 15", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 58, 15, "Nội dung bài học mẫu...", "Bài 4: Kiến thức nền tảng của khóa 15", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 59, 15, "Nội dung bài học mẫu...", "Bài 5: Kiến thức nền tảng của khóa 15", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" }
                });

            migrationBuilder.InsertData(
                table: "DonHang",
                columns: new[] { "Id", "KhoaHocId", "NgayMua", "SoTien", "TrangThai", "UserId" },
                values: new object[,]
                {
                    { 1, 7, new DateTime(2026, 3, 1, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6433), 400000m, true, "39" },
                    { 2, 2, new DateTime(2026, 3, 8, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6448), 400000m, true, "21" },
                    { 3, 15, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6449), 400000m, true, "7" },
                    { 4, 1, new DateTime(2026, 2, 22, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6450), 400000m, true, "26" },
                    { 5, 1, new DateTime(2026, 2, 18, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6451), 400000m, true, "15" },
                    { 6, 7, new DateTime(2026, 3, 12, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6454), 400000m, true, "51" },
                    { 7, 3, new DateTime(2026, 3, 2, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6470), 400000m, true, "39" },
                    { 8, 1, new DateTime(2026, 2, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6471), 400000m, true, "33" },
                    { 9, 4, new DateTime(2026, 3, 10, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6473), 400000m, true, "32" },
                    { 10, 8, new DateTime(2026, 3, 10, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6475), 400000m, true, "11" },
                    { 11, 11, new DateTime(2026, 2, 23, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6476), 400000m, true, "15" },
                    { 12, 2, new DateTime(2026, 2, 25, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6478), 400000m, true, "12" },
                    { 13, 1, new DateTime(2026, 3, 3, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6479), 400000m, true, "23" },
                    { 14, 8, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6480), 400000m, true, "38" },
                    { 15, 6, new DateTime(2026, 3, 10, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6481), 400000m, true, "55" },
                    { 16, 14, new DateTime(2026, 3, 7, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6482), 400000m, true, "55" },
                    { 17, 5, new DateTime(2026, 2, 18, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6483), 400000m, true, "14" },
                    { 18, 5, new DateTime(2026, 3, 12, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6485), 400000m, true, "35" },
                    { 19, 13, new DateTime(2026, 2, 13, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6486), 400000m, true, "15" },
                    { 20, 12, new DateTime(2026, 3, 2, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6487), 400000m, true, "18" },
                    { 21, 2, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6488), 400000m, true, "24" },
                    { 22, 1, new DateTime(2026, 2, 25, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6490), 400000m, true, "37" },
                    { 23, 15, new DateTime(2026, 2, 17, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6491), 400000m, true, "54" },
                    { 24, 8, new DateTime(2026, 2, 22, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6492), 400000m, true, "22" },
                    { 25, 6, new DateTime(2026, 2, 26, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6493), 400000m, true, "44" },
                    { 26, 6, new DateTime(2026, 3, 3, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6494), 400000m, true, "34" },
                    { 27, 3, new DateTime(2026, 2, 22, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6495), 400000m, true, "48" },
                    { 28, 1, new DateTime(2026, 2, 16, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6496), 400000m, true, "32" },
                    { 29, 11, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6497), 400000m, true, "41" },
                    { 30, 15, new DateTime(2026, 3, 1, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6498), 400000m, true, "23" },
                    { 31, 6, new DateTime(2026, 2, 15, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6499), 400000m, true, "31" },
                    { 32, 5, new DateTime(2026, 2, 20, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6501), 400000m, true, "40" },
                    { 33, 11, new DateTime(2026, 2, 21, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6502), 400000m, true, "44" },
                    { 34, 11, new DateTime(2026, 3, 9, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6504), 400000m, true, "22" },
                    { 35, 2, new DateTime(2026, 2, 16, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6505), 400000m, true, "10" },
                    { 36, 6, new DateTime(2026, 2, 23, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6506), 400000m, true, "52" },
                    { 37, 15, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6507), 400000m, true, "30" },
                    { 38, 5, new DateTime(2026, 2, 18, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6508), 400000m, true, "7" },
                    { 39, 10, new DateTime(2026, 3, 7, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6509), 400000m, true, "8" },
                    { 40, 3, new DateTime(2026, 2, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6537), 400000m, true, "18" },
                    { 41, 3, new DateTime(2026, 3, 13, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6539), 400000m, true, "10" },
                    { 42, 11, new DateTime(2026, 3, 12, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6540), 400000m, true, "23" },
                    { 43, 6, new DateTime(2026, 2, 24, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6541), 400000m, true, "17" },
                    { 44, 10, new DateTime(2026, 2, 22, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6542), 400000m, true, "9" },
                    { 45, 9, new DateTime(2026, 3, 8, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6543), 400000m, true, "40" },
                    { 46, 7, new DateTime(2026, 3, 13, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6544), 400000m, true, "40" },
                    { 47, 6, new DateTime(2026, 2, 13, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6546), 400000m, true, "17" },
                    { 48, 11, new DateTime(2026, 3, 6, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6547), 400000m, true, "35" },
                    { 49, 2, new DateTime(2026, 2, 20, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6548), 400000m, true, "19" },
                    { 50, 2, new DateTime(2026, 3, 10, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6549), 400000m, true, "18" },
                    { 51, 1, new DateTime(2026, 3, 10, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6550), 400000m, true, "45" },
                    { 52, 11, new DateTime(2026, 3, 11, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6551), 400000m, true, "31" },
                    { 53, 9, new DateTime(2026, 2, 19, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6552), 400000m, true, "13" },
                    { 54, 12, new DateTime(2026, 3, 11, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6553), 400000m, true, "48" },
                    { 55, 8, new DateTime(2026, 2, 24, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6554), 400000m, true, "54" },
                    { 56, 9, new DateTime(2026, 3, 6, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6555), 400000m, true, "54" },
                    { 57, 11, new DateTime(2026, 2, 25, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6556), 400000m, true, "30" },
                    { 58, 1, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6557), 400000m, true, "43" },
                    { 59, 9, new DateTime(2026, 2, 23, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6558), 400000m, true, "6" },
                    { 60, 7, new DateTime(2026, 2, 25, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6559), 400000m, true, "54" },
                    { 61, 4, new DateTime(2026, 3, 10, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6560), 400000m, true, "42" },
                    { 62, 11, new DateTime(2026, 3, 11, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6561), 400000m, true, "42" },
                    { 63, 13, new DateTime(2026, 2, 18, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6563), 400000m, true, "6" },
                    { 64, 8, new DateTime(2026, 2, 20, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6564), 400000m, true, "11" },
                    { 65, 3, new DateTime(2026, 3, 9, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6565), 400000m, true, "13" },
                    { 66, 9, new DateTime(2026, 3, 12, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6567), 400000m, true, "51" },
                    { 67, 14, new DateTime(2026, 2, 15, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6568), 400000m, true, "22" },
                    { 68, 7, new DateTime(2026, 3, 2, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6570), 400000m, true, "21" },
                    { 69, 9, new DateTime(2026, 3, 9, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6571), 400000m, true, "35" },
                    { 70, 6, new DateTime(2026, 3, 11, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6572), 400000m, true, "35" },
                    { 71, 11, new DateTime(2026, 2, 15, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6573), 400000m, true, "29" },
                    { 72, 9, new DateTime(2026, 2, 20, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6574), 400000m, true, "14" },
                    { 73, 8, new DateTime(2026, 2, 21, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6607), 400000m, true, "39" },
                    { 74, 3, new DateTime(2026, 2, 24, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6609), 400000m, true, "17" },
                    { 75, 9, new DateTime(2026, 2, 19, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6610), 400000m, true, "40" },
                    { 76, 2, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6611), 400000m, true, "43" },
                    { 77, 11, new DateTime(2026, 3, 7, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6612), 400000m, true, "14" },
                    { 78, 1, new DateTime(2026, 3, 5, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6613), 400000m, true, "36" },
                    { 79, 7, new DateTime(2026, 2, 21, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6614), 400000m, true, "11" },
                    { 80, 7, new DateTime(2026, 2, 27, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6615), 400000m, true, "34" },
                    { 81, 11, new DateTime(2026, 2, 19, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6616), 400000m, true, "35" },
                    { 82, 8, new DateTime(2026, 2, 21, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6618), 400000m, true, "25" },
                    { 83, 1, new DateTime(2026, 3, 13, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6619), 400000m, true, "14" },
                    { 84, 5, new DateTime(2026, 3, 9, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6620), 400000m, true, "23" },
                    { 85, 12, new DateTime(2026, 2, 15, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6621), 400000m, true, "40" },
                    { 86, 7, new DateTime(2026, 3, 9, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6622), 400000m, true, "54" },
                    { 87, 3, new DateTime(2026, 2, 27, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6623), 400000m, true, "8" },
                    { 88, 3, new DateTime(2026, 3, 11, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6624), 400000m, true, "20" },
                    { 89, 3, new DateTime(2026, 2, 16, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6625), 400000m, true, "49" },
                    { 90, 9, new DateTime(2026, 2, 16, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6626), 400000m, true, "48" },
                    { 91, 9, new DateTime(2026, 2, 18, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6627), 400000m, true, "22" },
                    { 92, 7, new DateTime(2026, 3, 1, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6628), 400000m, true, "22" },
                    { 93, 2, new DateTime(2026, 2, 13, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6629), 400000m, true, "31" },
                    { 94, 12, new DateTime(2026, 2, 17, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6630), 400000m, true, "49" },
                    { 95, 13, new DateTime(2026, 2, 15, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6631), 400000m, true, "12" },
                    { 96, 11, new DateTime(2026, 3, 8, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6632), 400000m, true, "20" },
                    { 97, 15, new DateTime(2026, 3, 5, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6633), 400000m, true, "53" },
                    { 98, 15, new DateTime(2026, 2, 21, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6634), 400000m, true, "41" },
                    { 99, 9, new DateTime(2026, 3, 12, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6636), 400000m, true, "11" },
                    { 100, 11, new DateTime(2026, 2, 21, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6637), 400000m, true, "44" },
                    { 101, 9, new DateTime(2026, 2, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6638), 400000m, true, "12" },
                    { 102, 13, new DateTime(2026, 3, 2, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6639), 400000m, true, "43" },
                    { 103, 6, new DateTime(2026, 3, 6, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6640), 400000m, true, "46" },
                    { 104, 2, new DateTime(2026, 2, 27, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6641), 400000m, true, "40" },
                    { 105, 2, new DateTime(2026, 2, 22, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6642), 400000m, true, "50" },
                    { 106, 12, new DateTime(2026, 3, 5, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6643), 400000m, true, "22" },
                    { 107, 3, new DateTime(2026, 3, 12, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6644), 400000m, true, "28" },
                    { 108, 14, new DateTime(2026, 3, 8, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6645), 400000m, true, "28" },
                    { 109, 9, new DateTime(2026, 3, 1, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6646), 400000m, true, "20" },
                    { 110, 12, new DateTime(2026, 2, 13, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6647), 400000m, true, "47" },
                    { 111, 4, new DateTime(2026, 3, 7, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6648), 400000m, true, "27" },
                    { 112, 5, new DateTime(2026, 3, 13, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6649), 400000m, true, "21" },
                    { 113, 15, new DateTime(2026, 2, 14, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6650), 400000m, true, "8" },
                    { 114, 7, new DateTime(2026, 3, 3, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6651), 400000m, true, "35" },
                    { 115, 12, new DateTime(2026, 3, 10, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6652), 400000m, true, "53" },
                    { 116, 13, new DateTime(2026, 3, 11, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6653), 400000m, true, "52" },
                    { 117, 2, new DateTime(2026, 3, 3, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6655), 400000m, true, "32" },
                    { 118, 2, new DateTime(2026, 3, 4, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6681), 400000m, true, "13" },
                    { 119, 5, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6682), 400000m, true, "43" },
                    { 120, 13, new DateTime(2026, 3, 2, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6684), 400000m, true, "27" },
                    { 121, 2, new DateTime(2026, 3, 8, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6685), 400000m, true, "37" },
                    { 122, 3, new DateTime(2026, 2, 26, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6686), 400000m, true, "39" },
                    { 123, 11, new DateTime(2026, 3, 3, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6687), 400000m, true, "30" },
                    { 124, 12, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6688), 400000m, true, "17" },
                    { 125, 6, new DateTime(2026, 3, 12, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6689), 400000m, true, "35" },
                    { 126, 8, new DateTime(2026, 2, 16, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6690), 400000m, true, "43" },
                    { 127, 5, new DateTime(2026, 2, 21, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6691), 400000m, true, "33" },
                    { 128, 11, new DateTime(2026, 2, 19, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6692), 400000m, true, "37" },
                    { 129, 4, new DateTime(2026, 2, 21, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6693), 400000m, true, "31" },
                    { 130, 13, new DateTime(2026, 2, 20, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6696), 400000m, true, "30" },
                    { 131, 11, new DateTime(2026, 3, 5, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6697), 400000m, true, "20" },
                    { 132, 1, new DateTime(2026, 3, 10, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6698), 400000m, true, "55" },
                    { 133, 11, new DateTime(2026, 2, 24, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6699), 400000m, true, "50" },
                    { 134, 9, new DateTime(2026, 2, 15, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6700), 400000m, true, "34" },
                    { 135, 4, new DateTime(2026, 3, 9, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6701), 400000m, true, "21" },
                    { 136, 4, new DateTime(2026, 2, 24, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6702), 400000m, true, "41" },
                    { 137, 7, new DateTime(2026, 2, 28, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6703), 400000m, true, "33" },
                    { 138, 11, new DateTime(2026, 3, 4, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6704), 400000m, true, "17" },
                    { 139, 3, new DateTime(2026, 2, 23, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6705), 400000m, true, "33" },
                    { 140, 7, new DateTime(2026, 2, 24, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6729), 400000m, true, "8" },
                    { 141, 7, new DateTime(2026, 2, 23, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6730), 400000m, true, "26" },
                    { 142, 8, new DateTime(2026, 3, 5, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6731), 400000m, true, "49" },
                    { 143, 13, new DateTime(2026, 3, 8, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6732), 400000m, true, "30" },
                    { 144, 2, new DateTime(2026, 2, 17, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6734), 400000m, true, "25" },
                    { 145, 14, new DateTime(2026, 3, 6, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6735), 400000m, true, "11" },
                    { 146, 2, new DateTime(2026, 2, 26, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6736), 400000m, true, "51" },
                    { 147, 5, new DateTime(2026, 2, 23, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6737), 400000m, true, "55" },
                    { 148, 1, new DateTime(2026, 2, 23, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6738), 400000m, true, "12" },
                    { 149, 14, new DateTime(2026, 2, 24, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6739), 400000m, true, "40" },
                    { 150, 15, new DateTime(2026, 2, 20, 1, 39, 22, 646, DateTimeKind.Local).AddTicks(6740), 400000m, true, "30" }
                });

            migrationBuilder.InsertData(
                table: "ChiTietDonHangs",
                columns: new[] { "Id", "DonHangId", "GiaMua", "KhoaHocId" },
                values: new object[,]
                {
                    { 1, 1, 400000m, 7 },
                    { 2, 2, 400000m, 2 },
                    { 3, 3, 400000m, 15 },
                    { 4, 4, 400000m, 1 },
                    { 5, 5, 400000m, 1 },
                    { 6, 6, 400000m, 7 },
                    { 7, 7, 400000m, 3 },
                    { 8, 8, 400000m, 1 },
                    { 9, 9, 400000m, 4 },
                    { 10, 10, 400000m, 8 },
                    { 11, 11, 400000m, 11 },
                    { 12, 12, 400000m, 2 },
                    { 13, 13, 400000m, 1 },
                    { 14, 14, 400000m, 8 },
                    { 15, 15, 400000m, 6 },
                    { 16, 16, 400000m, 14 },
                    { 17, 17, 400000m, 5 },
                    { 18, 18, 400000m, 5 },
                    { 19, 19, 400000m, 13 },
                    { 20, 20, 400000m, 12 },
                    { 21, 21, 400000m, 2 },
                    { 22, 22, 400000m, 1 },
                    { 23, 23, 400000m, 15 },
                    { 24, 24, 400000m, 8 },
                    { 25, 25, 400000m, 6 },
                    { 26, 26, 400000m, 6 },
                    { 27, 27, 400000m, 3 },
                    { 28, 28, 400000m, 1 },
                    { 29, 29, 400000m, 11 },
                    { 30, 30, 400000m, 15 },
                    { 31, 31, 400000m, 6 },
                    { 32, 32, 400000m, 5 },
                    { 33, 33, 400000m, 11 },
                    { 34, 34, 400000m, 11 },
                    { 35, 35, 400000m, 2 },
                    { 36, 36, 400000m, 6 },
                    { 37, 37, 400000m, 15 },
                    { 38, 38, 400000m, 5 },
                    { 39, 39, 400000m, 10 },
                    { 40, 40, 400000m, 3 },
                    { 41, 41, 400000m, 3 },
                    { 42, 42, 400000m, 11 },
                    { 43, 43, 400000m, 6 },
                    { 44, 44, 400000m, 10 },
                    { 45, 45, 400000m, 9 },
                    { 46, 46, 400000m, 7 },
                    { 47, 47, 400000m, 6 },
                    { 48, 48, 400000m, 11 },
                    { 49, 49, 400000m, 2 },
                    { 50, 50, 400000m, 2 },
                    { 51, 51, 400000m, 1 },
                    { 52, 52, 400000m, 11 },
                    { 53, 53, 400000m, 9 },
                    { 54, 54, 400000m, 12 },
                    { 55, 55, 400000m, 8 },
                    { 56, 56, 400000m, 9 },
                    { 57, 57, 400000m, 11 },
                    { 58, 58, 400000m, 1 },
                    { 59, 59, 400000m, 9 },
                    { 60, 60, 400000m, 7 },
                    { 61, 61, 400000m, 4 },
                    { 62, 62, 400000m, 11 },
                    { 63, 63, 400000m, 13 },
                    { 64, 64, 400000m, 8 },
                    { 65, 65, 400000m, 3 },
                    { 66, 66, 400000m, 9 },
                    { 67, 67, 400000m, 14 },
                    { 68, 68, 400000m, 7 },
                    { 69, 69, 400000m, 9 },
                    { 70, 70, 400000m, 6 },
                    { 71, 71, 400000m, 11 },
                    { 72, 72, 400000m, 9 },
                    { 73, 73, 400000m, 8 },
                    { 74, 74, 400000m, 3 },
                    { 75, 75, 400000m, 9 },
                    { 76, 76, 400000m, 2 },
                    { 77, 77, 400000m, 11 },
                    { 78, 78, 400000m, 1 },
                    { 79, 79, 400000m, 7 },
                    { 80, 80, 400000m, 7 },
                    { 81, 81, 400000m, 11 },
                    { 82, 82, 400000m, 8 },
                    { 83, 83, 400000m, 1 },
                    { 84, 84, 400000m, 5 },
                    { 85, 85, 400000m, 12 },
                    { 86, 86, 400000m, 7 },
                    { 87, 87, 400000m, 3 },
                    { 88, 88, 400000m, 3 },
                    { 89, 89, 400000m, 3 },
                    { 90, 90, 400000m, 9 },
                    { 91, 91, 400000m, 9 },
                    { 92, 92, 400000m, 7 },
                    { 93, 93, 400000m, 2 },
                    { 94, 94, 400000m, 12 },
                    { 95, 95, 400000m, 13 },
                    { 96, 96, 400000m, 11 },
                    { 97, 97, 400000m, 15 },
                    { 98, 98, 400000m, 15 },
                    { 99, 99, 400000m, 9 },
                    { 100, 100, 400000m, 11 },
                    { 101, 101, 400000m, 9 },
                    { 102, 102, 400000m, 13 },
                    { 103, 103, 400000m, 6 },
                    { 104, 104, 400000m, 2 },
                    { 105, 105, 400000m, 2 },
                    { 106, 106, 400000m, 12 },
                    { 107, 107, 400000m, 3 },
                    { 108, 108, 400000m, 14 },
                    { 109, 109, 400000m, 9 },
                    { 110, 110, 400000m, 12 },
                    { 111, 111, 400000m, 4 },
                    { 112, 112, 400000m, 5 },
                    { 113, 113, 400000m, 15 },
                    { 114, 114, 400000m, 7 },
                    { 115, 115, 400000m, 12 },
                    { 116, 116, 400000m, 13 },
                    { 117, 117, 400000m, 2 },
                    { 118, 118, 400000m, 2 },
                    { 119, 119, 400000m, 5 },
                    { 120, 120, 400000m, 13 },
                    { 121, 121, 400000m, 2 },
                    { 122, 122, 400000m, 3 },
                    { 123, 123, 400000m, 11 },
                    { 124, 124, 400000m, 12 },
                    { 125, 125, 400000m, 6 },
                    { 126, 126, 400000m, 8 },
                    { 127, 127, 400000m, 5 },
                    { 128, 128, 400000m, 11 },
                    { 129, 129, 400000m, 4 },
                    { 130, 130, 400000m, 13 },
                    { 131, 131, 400000m, 11 },
                    { 132, 132, 400000m, 1 },
                    { 133, 133, 400000m, 11 },
                    { 134, 134, 400000m, 9 },
                    { 135, 135, 400000m, 4 },
                    { 136, 136, 400000m, 4 },
                    { 137, 137, 400000m, 7 },
                    { 138, 138, 400000m, 11 },
                    { 139, 139, 400000m, 3 },
                    { 140, 140, 400000m, 7 },
                    { 141, 141, 400000m, 7 },
                    { 142, 142, 400000m, 8 },
                    { 143, 143, 400000m, 13 },
                    { 144, 144, 400000m, 2 },
                    { 145, 145, 400000m, 14 },
                    { 146, 146, 400000m, 2 },
                    { 147, 147, 400000m, 5 },
                    { 148, 148, 400000m, 1 },
                    { 149, 149, 400000m, 14 },
                    { 150, 150, 400000m, 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BaiHoc_KhoaHocId",
                table: "BaiHoc",
                column: "KhoaHocId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_DonHangId",
                table: "ChiTietDonHangs",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_KhoaHocId",
                table: "ChiTietDonHangs",
                column: "KhoaHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_KhoaHocId",
                table: "DanhGia",
                column: "KhoaHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_UserId",
                table: "DanhGia",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_KhoaHocId",
                table: "DonHang",
                column: "KhoaHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_UserId",
                table: "DonHang",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KhoaHoc_DanhMucId",
                table: "KhoaHoc",
                column: "DanhMucId");

            migrationBuilder.CreateIndex(
                name: "IX_KhoaHoc_GiangVienId",
                table: "KhoaHoc",
                column: "GiangVienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BaiHoc");

            migrationBuilder.DropTable(
                name: "ChiTietDonHangs");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "KhoaHoc");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DanhMuc");
        }
    }
}
