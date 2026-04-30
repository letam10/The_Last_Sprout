namespace TheLastSprout.Save
{
    public interface ISaveMigration
    {
        bool CanMigrate(string fromVersion, string toVersion);
        void Migrate(SaveGameData data);
    }
}
