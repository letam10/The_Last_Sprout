using NUnit.Framework;
using TheLastSprout.Save;

namespace TheLastSprout.Tests.EditMode
{
    public class SaveChecksumTests
    {
        [Test]
        public void CalculateChecksum_ReturnsValidHash()
        {
            string json = "{\"test\": 123}";
            string hash1 = SaveChecksumUtility.CalculateChecksum(json);
            string hash2 = SaveChecksumUtility.CalculateChecksum(json);
            
            Assert.IsNotEmpty(hash1);
            Assert.AreEqual(hash1, hash2); // Deterministic
        }
    }
}
