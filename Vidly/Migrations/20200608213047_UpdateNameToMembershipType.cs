using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class UpdateNameToMembershipType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE dbo.MembershipType SET Name = 'Pay as You Go' WHERE Id = 1");
            migrationBuilder.Sql("UPDATE dbo.MembershipType SET Name = 'Monthly' WHERE Id = 2");
            migrationBuilder.Sql("UPDATE dbo.MembershipType SET Name = 'Quarterly' WHERE Id = 3");
            migrationBuilder.Sql("UPDATE dbo.MembershipType SET Name = 'Yearly' WHERE Id = 4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
