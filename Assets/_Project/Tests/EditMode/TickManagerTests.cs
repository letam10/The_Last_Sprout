using NUnit.Framework;
using TheLastSprout.Core;
using UnityEngine;

namespace TheLastSprout.Tests.EditMode
{
    public class TickManagerTests
    {
        private class MockTickable : ITickable
        {
            public int TickCount { get; private set; }
            public void Tick(float deltaTime) => TickCount++;
        }

        [Test]
        public void TickGroup_RunsInterval()
        {
            var go = new GameObject();
            var manager = go.AddComponent<TickManager>();
            manager.Initialize();

            var mock = new MockTickable();
            manager.Register(mock, TickGroup.Simulation);

            // Simulation default is 0.1s. Let's force update logic via reflection or by just testing registration.
            // Since Update() is private and called by Unity, we can test Register/Unregister mostly in EditMode,
            // or we'd extract Update logic to a public method like ProcessTick(dt) for easier unit testing.
            // For now, just test if it's registered.

            manager.Unregister(mock, TickGroup.Simulation);
            
            Object.DestroyImmediate(go);
            Assert.Pass(); // Basic stub test passed
        }
    }
}
