namespace DBMigrator.Migrations;

[TimestampedMigration(2023, 1, 8, 23, 47)]
public sealed class InitialMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("TestTable");
    }
}