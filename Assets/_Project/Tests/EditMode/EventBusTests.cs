using NUnit.Framework;
using TheLastSprout.Core;

namespace TheLastSprout.Tests.EditMode
{
    public class EventBusTests
    {
        private struct TestEvent { public int Value; }

        [Test]
        public void SubscribeAndPublish_TriggersHandler()
        {
            var eventBus = new EventBus();
            int receivedValue = 0;

            eventBus.Subscribe<TestEvent>(e => receivedValue = e.Value);
            eventBus.Publish(new TestEvent { Value = 42 });

            Assert.AreEqual(42, receivedValue);
        }

        [Test]
        public void Unsubscribe_StopsTriggeringHandler()
        {
            var eventBus = new EventBus();
            int receivedValue = 0;

            System.Action<TestEvent> handler = e => receivedValue = e.Value;
            
            eventBus.Subscribe(handler);
            eventBus.Unsubscribe(handler);
            
            eventBus.Publish(new TestEvent { Value = 42 });

            Assert.AreEqual(0, receivedValue); // Should not have changed
        }
    }
}
