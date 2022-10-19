using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopeeFood.Migrations
{
    public partial class AddDetailProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("Detail", "products", "varchar(255)", unicode: false, maxLength: 255, nullable: true);
        }
    }
}
