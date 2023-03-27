using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "ISBN", "Name", "Genre", "Description", "Author", "BorrowTime", "ReturnTime" },
                values: new object[,]
                {
                    { "69ab9ae4-1739-4dd8-852b-afb01175d2e1", "1248752418865", "Book 1", "Genre 1", "Good description",
                      "Me", DateTime.Now, DateTime.Now.AddDays(2) },

                    { "8e2072db-b2da-4870-9917-b6fabf2a5361", "2249752213464", "Book 2", "Genre 2", "Bad description",
                      "Not Me", DateTime.Now, DateTime.Now.AddHours(15) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username", "Password"},
                values: new object[,]
                {
                    { "f995c967-c6f7-4c43-be28-70b875b5ef35", "admin", "AMy2I6XPMALmMhR3fplQJvOiGINjqV58h4nur2xfMDA2GiGUfUEY1aoyvVP/HdKP2g=="}
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
