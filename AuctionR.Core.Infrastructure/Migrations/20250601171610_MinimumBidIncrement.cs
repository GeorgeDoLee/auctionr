using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionR.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MinimumBidIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HighestBidAmount",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumBidIncrement",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumBidIncrement",
                table: "Auctions");

            migrationBuilder.AlterColumn<decimal>(
                name: "HighestBidAmount",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
