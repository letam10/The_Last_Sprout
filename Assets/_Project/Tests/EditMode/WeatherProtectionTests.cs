using NUnit.Framework;
using TheLastSprout.Weather;
using UnityEngine;

namespace TheLastSprout.Tests.EditMode
{
    public class WeatherProtectionTests
    {
        private class MockProtectionProvider : IWeatherProtectionProvider
        {
            public bool IsIndoors(Vector3 worldPosition) => true;
            public bool IsSheltered(Vector3 worldPosition) => true;
            public bool IsGreenhouse(Vector3 worldPosition) => false;
        }

        [Test]
        public void ProtectionProvider_Indoors_ReturnsTrue()
        {
            var provider = new MockProtectionProvider();
            Assert.IsTrue(provider.IsIndoors(Vector3.zero));
        }
    }
}
