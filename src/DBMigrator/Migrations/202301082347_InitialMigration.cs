namespace DBMigrator.Migrations;

[TimestampedMigration(2023, 1, 8, 23, 47)]
public sealed class InitialMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Player")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Nickname").AsString(20).NotNullable()
            .WithColumn("Mark").AsString(10).NotNullable();

        Create.Table("Board")
            .WithColumn("Id").AsInt32().PrimaryKey();

        Create.Table("Game")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Player1Id").AsInt32().NotNullable().ForeignKey("Player", "Id")
            .WithColumn("Player2Id").AsInt32().NotNullable().ForeignKey("Player", "Id")
            .WithColumn("WinnerId").AsInt32().Nullable().ForeignKey("Player", "Id")
            .WithColumn("BoardId").AsInt32().NotNullable().ForeignKey("Board", "Id");
        
        Create.Table("Cell")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("X").AsInt32().NotNullable()
            .WithColumn("Y").AsInt32().NotNullable()
            .WithColumn("PlayerId").AsInt32().Nullable().ForeignKey("Player", "Id")
            .WithColumn("BoardId").AsInt32().NotNullable().ForeignKey("Board", "Id");
    }
}