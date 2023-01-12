namespace DBMigrator.Migrations;

[TimestampedMigration(2023, 1, 12, 22, 09)]
public sealed class InitialMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("User")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Nickname").AsString(100).NotNullable();

        Create.Table("Player")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("User", "Id")
            .WithColumn("Mark").AsString(20).NotNullable();

        Create.Table("Game")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("NoughtsPlayerId").AsInt32().NotNullable().ForeignKey("Player", "Id")
            .WithColumn("CrossesPlayerId").AsInt32().NotNullable().ForeignKey("Player", "Id")
            .WithColumn("State").AsString(20).NotNullable()
            .WithColumn("Board").AsString(255).NotNullable();
    }
}