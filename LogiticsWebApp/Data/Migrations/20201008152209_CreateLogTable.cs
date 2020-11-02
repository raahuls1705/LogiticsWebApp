
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LogiticsWebApp.Data.Migrations
{
    public partial class CreateLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            // name: nameof(ErrorLog),
            // columns: table => new
            // {
            //     Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //     LogLevelId = table.Column<int>(nullable: true),
            //     ShortMessage = table.Column<string>(nullable: true),
            //     FullMessage = table.Column<string>(nullable: true),
            //     IpAddress = table.Column<string>(nullable: true),
            //     LoggedUserId = table.Column<string>(nullable : false),
            //     PageUrl = table.Column<string>(nullable : true),
            //     AddedDate = table.Column<DateTime>(nullable: false),
            //     ModifiedDate = table.Column<DateTime>(nullable: true),
            // },
            // constraints: table =>
            // {
            //     table.PrimaryKey("PK_ErrorLog", x => x.Id);
            // });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(name: nameof(ErrorLog));
        }
    }
}
