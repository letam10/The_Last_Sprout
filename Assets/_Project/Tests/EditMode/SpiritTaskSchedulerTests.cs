using NUnit.Framework;
using TheLastSprout.Spirits;
using UnityEngine;

namespace TheLastSprout.Tests.EditMode
{
    public class SpiritTaskSchedulerTests
    {
        private class MockProvider : ISpiritTaskProvider
        {
            public float PriorityToReturn = 10f;
            public SpiritTaskRequest? GetNextTask(AutomationSpiritRole role, Vector3 spiritPosition, int spiritLevel)
            {
                return new SpiritTaskRequest { priority = PriorityToReturn, taskType = SpiritTaskType.WaterArea3x3 };
            }
        }

        [Test]
        public void Scheduler_ReturnsBestTask()
        {
            var scheduler = new SpiritTaskScheduler();
            var provider1 = new MockProvider { PriorityToReturn = 5f };
            var provider2 = new MockProvider { PriorityToReturn = 10f };

            scheduler.RegisterProvider(AutomationSpiritRole.Watering, provider1);
            scheduler.RegisterProvider(AutomationSpiritRole.Watering, provider2);

            var task = scheduler.GetBestTask(AutomationSpiritRole.Watering, Vector3.zero, 1);
            
            Assert.IsTrue(task.HasValue);
            Assert.AreEqual(10f, task.Value.priority);
        }
    }
}
