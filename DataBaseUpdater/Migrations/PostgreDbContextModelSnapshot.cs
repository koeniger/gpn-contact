﻿// <auto-generated />
using System;
using DataBaseUpdater.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataBaseUpdater.Migrations
{
    [DbContext(typeof(PostgreDbContext))]
    partial class PostgreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Models.gpn.contractor", b =>
                {
                    b.Property<int>("contractor_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("contact_info")
                        .HasColumnType("text");

                    b.Property<string>("contractor_name")
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.HasKey("contractor_id");

                    b.ToTable("fdc_contractors");
                });

            modelBuilder.Entity("Models.gpn.contractor_rate", b =>
                {
                    b.Property<int>("contractor_rate_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("contractor_id")
                        .HasColumnType("integer");

                    b.Property<int>("rate")
                        .HasColumnType("integer");

                    b.Property<DateTime>("rate_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("user_id")
                        .HasColumnType("integer");

                    b.HasKey("contractor_rate_id");

                    b.HasIndex("contractor_id");

                    b.HasIndex("user_id");

                    b.ToTable("fdc_contractors_rates");
                });

            modelBuilder.Entity("Models.gpn.contractor_response", b =>
                {
                    b.Property<int>("contractor_response_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("contractor_id")
                        .HasColumnType("integer");

                    b.Property<int?>("parent_id")
                        .HasColumnType("integer");

                    b.Property<string>("response")
                        .HasColumnType("text");

                    b.Property<DateTime>("response_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("user_id")
                        .HasColumnType("integer");

                    b.HasKey("contractor_response_id");

                    b.HasIndex("contractor_id");

                    b.HasIndex("parent_id");

                    b.HasIndex("user_id");

                    b.ToTable("fdc_contractors_responses");
                });

            modelBuilder.Entity("Models.gpn.image", b =>
                {
                    b.Property<int>("image_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("any_table_id")
                        .HasColumnType("integer");

                    b.Property<string>("any_table_name")
                        .HasColumnType("text");

                    b.Property<string>("image_path")
                        .HasColumnType("text");

                    b.Property<bool>("is_main")
                        .HasColumnType("boolean");

                    b.HasKey("image_id");

                    b.ToTable("fdc_images");
                });

            modelBuilder.Entity("Models.gpn.okei", b =>
                {
                    b.Property<int>("okei_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("okei_code")
                        .HasColumnType("integer");

                    b.Property<string>("okei_name")
                        .HasColumnType("text");

                    b.Property<string>("symbol_all")
                        .HasColumnType("text");

                    b.Property<string>("symbol_all_code")
                        .HasColumnType("text");

                    b.Property<string>("symbol_ru")
                        .HasColumnType("text");

                    b.Property<string>("symbol_ru_code")
                        .HasColumnType("text");

                    b.HasKey("okei_id");

                    b.ToTable("fdc_okei");
                });

            modelBuilder.Entity("Models.gpn.product", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("contractor_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("date_archive")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("date_change")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("description_full")
                        .HasColumnType("text");

                    b.Property<string>("description_short")
                        .HasColumnType("text");

                    b.Property<bool>("is_archive")
                        .HasColumnType("boolean");

                    b.Property<decimal>("price")
                        .HasColumnType("numeric");

                    b.Property<int?>("product_directory_id")
                        .HasColumnType("integer");

                    b.Property<string>("product_name")
                        .HasColumnType("text");

                    b.Property<int?>("product_type_id")
                        .HasColumnType("integer");

                    b.HasKey("product_id");

                    b.HasIndex("contractor_id");

                    b.HasIndex("product_directory_id");

                    b.HasIndex("product_type_id");

                    b.ToTable("fdc_products");
                });

            modelBuilder.Entity("Models.gpn.product_directory", b =>
                {
                    b.Property<int>("product_directory_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<int?>("parent_id")
                        .HasColumnType("integer");

                    b.Property<string>("product_directory_name")
                        .HasColumnType("text");

                    b.HasKey("product_directory_id");

                    b.HasIndex("parent_id");

                    b.ToTable("fdc_product_directories");
                });

            modelBuilder.Entity("Models.gpn.product_property", b =>
                {
                    b.Property<int>("product_property_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("okei_id")
                        .HasColumnType("integer");

                    b.Property<string>("product_property_type_id")
                        .HasColumnType("text");

                    b.Property<int>("product_type_id")
                        .HasColumnType("integer");

                    b.Property<string>("property_name")
                        .HasColumnType("text");

                    b.HasKey("product_property_id");

                    b.HasIndex("okei_id");

                    b.HasIndex("product_property_type_id");

                    b.HasIndex("product_type_id");

                    b.ToTable("fdc_products_properties");
                });

            modelBuilder.Entity("Models.gpn.product_property_type", b =>
                {
                    b.Property<string>("product_property_type_id")
                        .HasColumnType("text");

                    b.Property<string>("property_type_name")
                        .HasColumnType("text");

                    b.HasKey("product_property_type_id");

                    b.ToTable("fdc_products_properties_types");
                });

            modelBuilder.Entity("Models.gpn.product_question", b =>
                {
                    b.Property<int>("product_question_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("parent_id")
                        .HasColumnType("integer");

                    b.Property<int?>("product_id")
                        .HasColumnType("integer");

                    b.Property<string>("question")
                        .HasColumnType("text");

                    b.Property<DateTime>("questions_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("user_id")
                        .HasColumnType("integer");

                    b.HasKey("product_question_id");

                    b.HasIndex("parent_id");

                    b.HasIndex("product_id");

                    b.HasIndex("user_id");

                    b.ToTable("fdc_products_questions");
                });

            modelBuilder.Entity("Models.gpn.product_rate", b =>
                {
                    b.Property<int>("product_rate_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("product_id")
                        .HasColumnType("integer");

                    b.Property<int>("rate")
                        .HasColumnType("integer");

                    b.Property<DateTime>("rate_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("user_id")
                        .HasColumnType("integer");

                    b.HasKey("product_rate_id");

                    b.HasIndex("product_id");

                    b.HasIndex("user_id");

                    b.ToTable("fdc_products_rates");
                });

            modelBuilder.Entity("Models.gpn.product_response", b =>
                {
                    b.Property<int>("product_response_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("parent_id")
                        .HasColumnType("integer");

                    b.Property<int?>("product_id")
                        .HasColumnType("integer");

                    b.Property<string>("response")
                        .HasColumnType("text");

                    b.Property<DateTime>("response_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("user_id")
                        .HasColumnType("integer");

                    b.HasKey("product_response_id");

                    b.HasIndex("parent_id");

                    b.HasIndex("product_id");

                    b.HasIndex("user_id");

                    b.ToTable("fdc_products_responses");
                });

            modelBuilder.Entity("Models.gpn.product_type", b =>
                {
                    b.Property<int>("product_type_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("date_archive")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("date_change")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("is_archive")
                        .HasColumnType("boolean");

                    b.Property<string>("product_type_name")
                        .HasColumnType("text");

                    b.HasKey("product_type_id");

                    b.ToTable("fdc_products_types");
                });

            modelBuilder.Entity("Models.gpn.product_value", b =>
                {
                    b.Property<int>("product_value_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("product_id")
                        .HasColumnType("integer");

                    b.Property<int>("product_property_id")
                        .HasColumnType("integer");

                    b.Property<string>("product_value_max")
                        .HasColumnType("text");

                    b.Property<string>("product_value_min")
                        .HasColumnType("text");

                    b.HasKey("product_value_id");

                    b.HasIndex("product_id");

                    b.HasIndex("product_property_id");

                    b.ToTable("fdc_products_values");
                });

            modelBuilder.Entity("Models.secr.role", b =>
                {
                    b.Property<int>("role_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("role_name")
                        .HasColumnType("text");

                    b.HasKey("role_id");

                    b.ToTable("fdc_roles");
                });

            modelBuilder.Entity("Models.secr.user", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("contact_info")
                        .HasColumnType("text");

                    b.Property<int?>("contractor_id")
                        .HasColumnType("integer");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("position")
                        .HasColumnType("text");

                    b.Property<int?>("role_id")
                        .HasColumnType("integer");

                    b.Property<string>("user_name")
                        .HasColumnType("text");

                    b.HasKey("user_id");

                    b.HasIndex("contractor_id");

                    b.HasIndex("role_id");

                    b.ToTable("fdc_users");
                });

            modelBuilder.Entity("Models.gpn.contractor_rate", b =>
                {
                    b.HasOne("Models.gpn.contractor", "contractor")
                        .WithMany("Rates")
                        .HasForeignKey("contractor_id");

                    b.HasOne("Models.secr.user", "user")
                        .WithMany()
                        .HasForeignKey("user_id");
                });

            modelBuilder.Entity("Models.gpn.contractor_response", b =>
                {
                    b.HasOne("Models.gpn.contractor", "contractor")
                        .WithMany("Responses")
                        .HasForeignKey("contractor_id");

                    b.HasOne("Models.gpn.contractor_response", "parent")
                        .WithMany()
                        .HasForeignKey("parent_id");

                    b.HasOne("Models.secr.user", "user")
                        .WithMany()
                        .HasForeignKey("user_id");
                });

            modelBuilder.Entity("Models.gpn.product", b =>
                {
                    b.HasOne("Models.gpn.contractor", "contractor")
                        .WithMany("Products")
                        .HasForeignKey("contractor_id");

                    b.HasOne("Models.gpn.product_directory", "product_directory")
                        .WithMany("Products")
                        .HasForeignKey("product_directory_id");

                    b.HasOne("Models.gpn.product_type", "product_type")
                        .WithMany("Products")
                        .HasForeignKey("product_type_id");
                });

            modelBuilder.Entity("Models.gpn.product_directory", b =>
                {
                    b.HasOne("Models.gpn.product_directory", "parent")
                        .WithMany("Subdirectories")
                        .HasForeignKey("parent_id");
                });

            modelBuilder.Entity("Models.gpn.product_property", b =>
                {
                    b.HasOne("Models.gpn.okei", "okei")
                        .WithMany()
                        .HasForeignKey("okei_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.gpn.product_property_type", "product_property_type")
                        .WithMany()
                        .HasForeignKey("product_property_type_id");

                    b.HasOne("Models.gpn.product_type", "product_type")
                        .WithMany("ProductProperties")
                        .HasForeignKey("product_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.gpn.product_question", b =>
                {
                    b.HasOne("Models.gpn.product_question", "parent")
                        .WithMany()
                        .HasForeignKey("parent_id");

                    b.HasOne("Models.gpn.product", "product")
                        .WithMany("Questions")
                        .HasForeignKey("product_id");

                    b.HasOne("Models.secr.user", "user")
                        .WithMany()
                        .HasForeignKey("user_id");
                });

            modelBuilder.Entity("Models.gpn.product_rate", b =>
                {
                    b.HasOne("Models.gpn.product", "product")
                        .WithMany("Rates")
                        .HasForeignKey("product_id");

                    b.HasOne("Models.secr.user", "user")
                        .WithMany()
                        .HasForeignKey("user_id");
                });

            modelBuilder.Entity("Models.gpn.product_response", b =>
                {
                    b.HasOne("Models.gpn.product_response", "parent")
                        .WithMany()
                        .HasForeignKey("parent_id");

                    b.HasOne("Models.gpn.product", "product")
                        .WithMany("Responses")
                        .HasForeignKey("product_id");

                    b.HasOne("Models.secr.user", "user")
                        .WithMany()
                        .HasForeignKey("user_id");
                });

            modelBuilder.Entity("Models.gpn.product_value", b =>
                {
                    b.HasOne("Models.gpn.product", "product")
                        .WithMany("products_values")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.gpn.product_property", "product_property")
                        .WithMany()
                        .HasForeignKey("product_property_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.secr.user", b =>
                {
                    b.HasOne("Models.gpn.contractor", "contractor")
                        .WithMany()
                        .HasForeignKey("contractor_id");

                    b.HasOne("Models.secr.role", "role")
                        .WithMany("users")
                        .HasForeignKey("role_id");
                });
#pragma warning restore 612, 618
        }
    }
}
