namespace DBMigrator.Migrations;

[TimestampedMigration(2023, 1, 8, 23, 47)]
public sealed class InitialMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("User")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Nickname").AsString(20).NotNullable();

        Create.Table("Player")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("UserId").AsInt32().ForeignKey("User", "Id")
            .WithColumn("Mark").AsString(10).NotNullable();

        Create.Table("Game")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Player1Id").AsInt32().NotNullable().ForeignKey("Player", "Id")
            .WithColumn("Player2Id").AsInt32().NotNullable().ForeignKey("Player", "Id")
            .WithColumn("State").AsString(20).NotNullable()
            .WithColumn("Board_Cells").AsString().NotNullable();
    }
}