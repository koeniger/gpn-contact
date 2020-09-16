using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    contractor_id = table.Column<Guid>(nullable: false),
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
                    image_id = table.Column<Guid>(nullable: false),
                    any_table_id = table.Column<Guid>(nullable: false),
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
                    okei_id = table.Column<Guid>(nullable: false),
                    okei_code = table.Column<int>(nullable: false),
                    okei_name = table.Column<string>(nullable: true),
                    symbol_ru = table.Column<string>(nullable: true),
                    symbol_all = table.Column<string>(nullable: true),
                    symbol_ru_code = table.Column<string>(nullable: true),
                    symbol_all_code = table.Column<string>(nullable: true),
                    is_used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_okei", x => x.okei_id);
                });

            migrationBuilder.CreateTable(
                name: "fdc_product_directories",
                columns: table => new
                {
                    product_directory_id = table.Column<Guid>(nullable: false),
                    product_directory_name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    parent_id = table.Column<Guid>(nullable: true)
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
                name: "fdc_products_properties_types",
                columns: table => new
                {
                    product_property_type_id = table.Column<Guid>(nullable: false),
                    property_type_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products_properties_types", x => x.product_property_type_id);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_types",
                columns: table => new
                {
                    product_type_id = table.Column<Guid>(nullable: false),
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
                name: "fdc_roles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(nullable: false),
                    role_name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(nullable: false),
                    product_name = table.Column<string>(nullable: true),
                    description_short = table.Column<string>(nullable: true),
                    description_full = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    is_archive = table.Column<bool>(nullable: false),
                    date_change = table.Column<DateTime>(nullable: false),
                    date_archive = table.Column<DateTime>(nullable: false),
                    product_directory_id = table.Column<Guid>(nullable: false),
                    contractor_id = table.Column<Guid>(nullable: false),
                    product_type_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_fdc_products_fdc_contractors_contractor_id",
                        column: x => x.contractor_id,
                        principalTable: "fdc_contractors",
                        principalColumn: "contractor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_fdc_product_directories_product_directory_id",
                        column: x => x.product_directory_id,
                        principalTable: "fdc_product_directories",
                        principalColumn: "product_directory_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_fdc_products_types_product_type_id",
                        column: x => x.product_type_id,
                        principalTable: "fdc_products_types",
                        principalColumn: "product_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_properties",
                columns: table => new
                {
                    product_property_id = table.Column<Guid>(nullable: false),
                    property_name = table.Column<string>(nullable: true),
                    product_type_id = table.Column<Guid>(nullable: false),
                    product_property_type_id = table.Column<Guid>(nullable: false),
                    okei_id = table.Column<Guid>(nullable: false)
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
                        name: "FK_fdc_products_properties_fdc_products_properties_types_produ~",
                        column: x => x.product_property_type_id,
                        principalTable: "fdc_products_properties_types",
                        principalColumn: "product_property_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_properties_fdc_products_types_product_type_id",
                        column: x => x.product_type_id,
                        principalTable: "fdc_products_types",
                        principalColumn: "product_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fdc_users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    user_name = table.Column<string>(nullable: true),
                    position = table.Column<string>(nullable: true),
                    contact_info = table.Column<string>(nullable: true),
                    role_id = table.Column<Guid>(nullable: false),
                    contractor_id = table.Column<Guid>(nullable: false),
                    Password = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_fdc_users_fdc_contractors_contractor_id",
                        column: x => x.contractor_id,
                        principalTable: "fdc_contractors",
                        principalColumn: "contractor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_users_fdc_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "fdc_roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_values",
                columns: table => new
                {
                    product_value_id = table.Column<Guid>(nullable: false),
                    product_value_min = table.Column<string>(nullable: true),
                    product_value_max = table.Column<string>(nullable: true),
                    product_id = table.Column<Guid>(nullable: false),
                    product_property_id = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "fdc_contractors_rates",
                columns: table => new
                {
                    contractor_rate_id = table.Column<Guid>(nullable: false),
                    rate = table.Column<int>(nullable: false),
                    rate_date = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    contractor_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_contractors_rates", x => x.contractor_rate_id);
                    table.ForeignKey(
                        name: "FK_fdc_contractors_rates_fdc_contractors_contractor_id",
                        column: x => x.contractor_id,
                        principalTable: "fdc_contractors",
                        principalColumn: "contractor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_contractors_rates_fdc_users_user_id",
                        column: x => x.user_id,
                        principalTable: "fdc_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fdc_contractors_responses",
                columns: table => new
                {
                    contractor_response_id = table.Column<Guid>(nullable: false),
                    response_date = table.Column<DateTime>(nullable: false),
                    response = table.Column<string>(nullable: true),
                    user_id = table.Column<Guid>(nullable: false),
                    contractor_id = table.Column<Guid>(nullable: false),
                    parent_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_contractors_responses", x => x.contractor_response_id);
                    table.ForeignKey(
                        name: "FK_fdc_contractors_responses_fdc_contractors_contractor_id",
                        column: x => x.contractor_id,
                        principalTable: "fdc_contractors",
                        principalColumn: "contractor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_contractors_responses_fdc_contractors_responses_parent_~",
                        column: x => x.parent_id,
                        principalTable: "fdc_contractors_responses",
                        principalColumn: "contractor_response_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fdc_contractors_responses_fdc_users_user_id",
                        column: x => x.user_id,
                        principalTable: "fdc_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_questions",
                columns: table => new
                {
                    product_question_id = table.Column<Guid>(nullable: false),
                    question = table.Column<string>(nullable: true),
                    questions_date = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    parent_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products_questions", x => x.product_question_id);
                    table.ForeignKey(
                        name: "FK_fdc_products_questions_fdc_products_questions_parent_id",
                        column: x => x.parent_id,
                        principalTable: "fdc_products_questions",
                        principalColumn: "product_question_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fdc_products_questions_fdc_products_product_id",
                        column: x => x.product_id,
                        principalTable: "fdc_products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_questions_fdc_users_user_id",
                        column: x => x.user_id,
                        principalTable: "fdc_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_rates",
                columns: table => new
                {
                    product_rate_id = table.Column<Guid>(nullable: false),
                    rate = table.Column<int>(nullable: false),
                    rate_date = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    product_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products_rates", x => x.product_rate_id);
                    table.ForeignKey(
                        name: "FK_fdc_products_rates_fdc_products_product_id",
                        column: x => x.product_id,
                        principalTable: "fdc_products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_rates_fdc_users_user_id",
                        column: x => x.user_id,
                        principalTable: "fdc_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fdc_products_responses",
                columns: table => new
                {
                    product_response_id = table.Column<Guid>(nullable: false),
                    response_date = table.Column<DateTime>(nullable: false),
                    response = table.Column<string>(nullable: true),
                    user_id = table.Column<Guid>(nullable: false),
                    product_id = table.Column<Guid>(nullable: false),
                    parent_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fdc_products_responses", x => x.product_response_id);
                    table.ForeignKey(
                        name: "FK_fdc_products_responses_fdc_products_responses_parent_id",
                        column: x => x.parent_id,
                        principalTable: "fdc_products_responses",
                        principalColumn: "product_response_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fdc_products_responses_fdc_products_product_id",
                        column: x => x.product_id,
                        principalTable: "fdc_products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fdc_products_responses_fdc_users_user_id",
                        column: x => x.user_id,
                        principalTable: "fdc_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fdc_contractors_rates_contractor_id",
                table: "fdc_contractors_rates",
                column: "contractor_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_contractors_rates_user_id",
                table: "fdc_contractors_rates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_contractors_responses_contractor_id",
                table: "fdc_contractors_responses",
                column: "contractor_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_contractors_responses_parent_id",
                table: "fdc_contractors_responses",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_contractors_responses_user_id",
                table: "fdc_contractors_responses",
                column: "user_id");

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
                name: "IX_fdc_products_properties_product_property_type_id",
                table: "fdc_products_properties",
                column: "product_property_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_properties_product_type_id",
                table: "fdc_products_properties",
                column: "product_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_questions_parent_id",
                table: "fdc_products_questions",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_questions_product_id",
                table: "fdc_products_questions",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_questions_user_id",
                table: "fdc_products_questions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_rates_product_id",
                table: "fdc_products_rates",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_rates_user_id",
                table: "fdc_products_rates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_responses_parent_id",
                table: "fdc_products_responses",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_responses_product_id",
                table: "fdc_products_responses",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_responses_user_id",
                table: "fdc_products_responses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_values_product_id",
                table: "fdc_products_values",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_products_values_product_property_id",
                table: "fdc_products_values",
                column: "product_property_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_users_contractor_id",
                table: "fdc_users",
                column: "contractor_id");

            migrationBuilder.CreateIndex(
                name: "IX_fdc_users_role_id",
                table: "fdc_users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fdc_contractors_rates");

            migrationBuilder.DropTable(
                name: "fdc_contractors_responses");

            migrationBuilder.DropTable(
                name: "fdc_images");

            migrationBuilder.DropTable(
                name: "fdc_products_questions");

            migrationBuilder.DropTable(
                name: "fdc_products_rates");

            migrationBuilder.DropTable(
                name: "fdc_products_responses");

            migrationBuilder.DropTable(
                name: "fdc_products_values");

            migrationBuilder.DropTable(
                name: "fdc_users");

            migrationBuilder.DropTable(
                name: "fdc_products");

            migrationBuilder.DropTable(
                name: "fdc_products_properties");

            migrationBuilder.DropTable(
                name: "fdc_roles");

            migrationBuilder.DropTable(
                name: "fdc_contractors");

            migrationBuilder.DropTable(
                name: "fdc_product_directories");

            migrationBuilder.DropTable(
                name: "fdc_okei");

            migrationBuilder.DropTable(
                name: "fdc_products_properties_types");

            migrationBuilder.DropTable(
                name: "fdc_products_types");
        }
    }
}
