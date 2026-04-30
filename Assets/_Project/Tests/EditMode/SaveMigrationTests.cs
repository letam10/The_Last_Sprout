using NUnit.Framework;
using TheLastSprout.Save;

namespace TheLastSprout.Tests.EditMode
{
    public class SaveMigrationTests
    {
        [Test]
        public void Migration_v010_to_v020_UpdatesVersion()
        {
            var migration = new SaveMigration_v0_1_0_to_v0_2_0();
            var data = new SaveGameData();
            data.metadata.saveVersion = "0.1.0";
            data.metadata.playtimeSeconds = -10;

            Assert.IsTrue(migration.CanMigrate(data.metadata.saveVersion, "0.2.0"));
            
            migration.Migrate(data);
            
            Assert.AreEqual("0.2.0", data.metadata.saveVersion);
            Assert.AreEqual(0, data.metadata.playtimeSeconds);
        }
    }
}
