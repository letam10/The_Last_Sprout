using System.IO;
using NUnit.Framework;
using TheLastSprout.Save;

namespace TheLastSprout.Tests.EditMode
{
    public class SaveAtomicWriteTests
    {
        [Test]
        public void AtomicWrite_CreatesFileSafely()
        {
            var writer = new AtomicSaveWriter();
            string testPath = Path.Combine(UnityEngine.Application.persistentDataPath, "test_atomic.json");
            
            if (File.Exists(testPath)) File.Delete(testPath);
            if (File.Exists(testPath + ".bak")) File.Delete(testPath + ".bak");

            bool success = writer.WriteSafely(testPath, "{\"test\": 1}");
            
            Assert.IsTrue(success);
            Assert.IsTrue(File.Exists(testPath));

            File.Delete(testPath);
        }
    }
}
