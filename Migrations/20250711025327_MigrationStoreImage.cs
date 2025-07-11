using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Shop_Online.Migrations
{
    /// <inheritdoc />
    public partial class MigrationStoreImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Stores");
        }
    }
}
