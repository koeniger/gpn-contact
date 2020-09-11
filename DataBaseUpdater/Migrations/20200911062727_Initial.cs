using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataBaseUpdater.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fdc_contractors",
                columns: table => new
                {
                    contractor_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contractor_name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    contact_info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_contractors", x => x.contractor_id);
                });

            migrationBuilder.CreateTable(
                name: "fdc_images",
                columns: table => new
                {
                    image_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    any_table_id = table.Column<int>(nullable: false),
                    any_table_name = table.Column<string>(nullable: true),
                    image_path = table.Column<string>(nullable: true),
                    is_main = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_images", x => x.image_id);
                });

            migrationBuilder.CreateTable(
                name: "fdc_okei",
                columns: table => new
                {
                    okei_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_short = table.Column<string>(nullable: true),
                    name_full = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_okei", x => x.okei_id);
                });

            migrationBuilder.CreateTable(
                name: "fdc_product_directories",
                columns: table => new
                {
                    product_directory_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_directory_name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    parent_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_product_directories", x => x.product_directory_id);
                    table.ForeignKey(
                        name: "FK_fdc_product_directories_fdc_product_directories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "fdc_product_directories",
                        principalColumn: "product_directory_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_types",
                columns: table => new
                {
                    product_type_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_type_name = table.Column<string>(nullable: true),
                    is_archive = table.Column<bool>(nullable: false),
                    date_change = table.Column<DateTime>(nullable: false),
                    date_archive = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products_types", x => x.product_type_id);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_name = table.Column<string>(nullable: true),
                    description_short = table.Column<string>(nullable: true),
                    description_full = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    is_archive = table.Column<bool>(nullable: false),
                    date_change = table.Column<DateTime>(nullable: false),
                    date_archive = table.Column<DateTime>(nullable: false),
                    product_directory_id = table.Column<int>(nullable: true),
                    contractor_id = table.Column<int>(nullable: true),
                    product_type_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_fdc_products_fdc_contractors_contractor_id",
                        column: x => x.contractor_id,
                        principalTable: "fdc_contractors",
                        principalColumn: "contractor_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fdc_products_fdc_product_directories_product_directory_id",
                        column: x => x.product_directory_id,
                        principalTable: "fdc_product_directories",
                        principalColumn: "product_directory_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fdc_products_fdc_products_types_product_type_id",
                        column: x => x.product_type_id,
                        principalTable: "fdc_products_types",
                        principalColumn: "product_type_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_properties",
                columns: table => new
                {
                    product_property_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    property_name = table.Column<string>(nullable: true),
                    value_type = table.Column<string>(nullable: true),
                    product_type_id = table.Column<int>(nullable: false),
                    product_type_id1 = table.Column<int>(nullable: true),
                    okei_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products_properties", x => x.product_property_id);
                    table.ForeignKey(
                        name: "FK_fdc_products_properties_fdc_okei_okei_id",
                        column: x => x.okei_id,
                        principalTable: "fdc_okei",
                        principalColumn: "okei_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_properties_fdc_products_types_product_type_id1",
                        column: x => x.product_type_id1,
                        principalTable: "fdc_products_types",
                        principalColumn: "product_type_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_types_properties",
                columns: table => new
                {
                    product_type_property_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_type_id = table.Column<int>(nullable: false),
                    property = table.Column<string>(nullable: true),
                    value = table.Column<string>(nullable: true),
                    value_type = table.Column<string>(nullable: true),
                    value_okei_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products_types_properties", x => x.product_type_property_id);
                    table.ForeignKey(
                        name: "FK_fdc_products_types_properties_fdc_products_types_product_ty~",
                        column: x => x.product_type_id,
                        principalTable: "fdc_products_types",
                        principalColumn: "product_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_types_properties_fdc_okei_value_okei_id",
                        column: x => x.value_okei_id,
                        principalTable: "fdc_okei",
                        principalColumn: "okei_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_values",
                columns: table => new
                {
                    product_value_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_value = table.Column<string>(nullable: true),
                    product_id = table.Column<int>(nullable: false),
                    product_property_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products_values", x => x.product_value_id);
                    table.ForeignKey(
                        name: "FK_fdc_products_values_fdc_products_product_id",
                        column: x => x.product_id,
                        principalTable: "fdc_products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_values_fdc_products_properties_product_propert~",
                        column: x => x.product_property_id,
                        principalTable: "fdc_products_properties",
                        principalColumn: "product_property_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fdc_product_directories_parent_id",
                table: "fdc_product_directories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_contractor_id",
                table: "fdc_products",
                column: "contractor_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_product_directory_id",
                table: "fdc_products",
                column: "product_directory_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_product_type_id",
                table: "fdc_products",
                column: "product_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_properties_okei_id",
                table: "fdc_products_properties",
                column: "okei_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_properties_product_type_id1",
                table: "fdc_products_properties",
                column: "product_type_id1");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_types_properties_product_type_id",
                table: "fdc_products_types_properties",
                column: "product_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_types_properties_value_okei_id",
                table: "fdc_products_types_properties",
                column: "value_okei_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_values_product_id",
                table: "fdc_products_values",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_values_product_property_id",
                table: "fdc_products_values",
                column: "product_property_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fdc_images");

            migrationBuilder.DropTable(
                name: "fdc_products_types_properties");

            migrationBuilder.DropTable(
                name: "fdc_products_values");

            migrationBuilder.DropTable(
                name: "fdc_products");

            migrationBuilder.DropTable(
                name: "fdc_products_properties");

            migrationBuilder.DropTable(
                name: "fdc_contractors");

            migrationBuilder.DropTable(
                name: "fdc_product_directories");

            migrationBuilder.DropTable(
                name: "fdc_okei");

            migrationBuilder.DropTable(
                name: "fdc_products_types");
        }
    }
}
