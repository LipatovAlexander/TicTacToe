namespace DBMigrator.Migrations;

[TimestampedMigration(2023, 1, 12, 22, 09)]
public sealed class InitialMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("User")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserName").AsString(256).Nullable().Unique()
            .WithColumn("PasswordHash").AsString(200).Nullable()
            .WithColumn("NormalizedUserName").AsString(256).Nullable().Unique()
            .WithColumn("NormalizedEmail").AsString(256).Nullable().Unique()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("Email").AsString(256).Nullable()
            .WithColumn("AccessFailedCount").AsInt32().Nullable()
            .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
            .WithColumn("LockoutEnabled").AsBoolean().Nullable()
            .WithColumn("LockoutEnd").AsDateTime().Nullable()
            .WithColumn("PhoneNumber").AsString(20).Nullable()
            .WithColumn("SecurityStamp").AsString().Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
            .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable();

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