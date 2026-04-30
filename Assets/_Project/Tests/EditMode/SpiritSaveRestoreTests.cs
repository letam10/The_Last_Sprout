using NUnit.Framework;
using TheLastSprout.Core;
using TheLastSprout.Core.Data;
using TheLastSprout.Save;
using TheLastSprout.Spirits;
using UnityEngine;

namespace TheLastSprout.Tests.EditMode
{
    public class SpiritSaveRestoreTests
    {
        [Test]
        public void SpiritManager_SavesAndRestoresCorrectly()
        {
            var registry = new ServiceRegistry();
            var eventBus = new EventBus();
            registry.Register(eventBus);

            var goTick = new GameObject();
            registry.Register(goTick.AddComponent<TickManager>());
            var saveManager = new SaveManager();
            saveManager.Initialize(eventBus);
            registry.Register(saveManager);

            var go = new GameObject();
            var manager = go.AddComponent<AutomationSpiritManager>();
            manager.Initialize(registry);

            var def = ScriptableObject.CreateInstance<AutomationSpiritDefinition>();
            def.role = AutomationSpiritRole.Watering;
            def.maxEnergy = 100f;

            manager.SpawnSpirit(def, "spirit_water_1", Vector3.zero);
            var state = manager.GetSpiritState("spirit_water_1");
            state.currentEnergy = 25f;

            var saveData = new SaveGameData();
            var adapter = new SpiritSaveAdapter(manager);
            adapter.CaptureState(saveData);

            // Destroy and recreate to simulate load
            Object.DestroyImmediate(go);
            
            var go2 = new GameObject();
            var manager2 = go2.AddComponent<AutomationSpiritManager>();
            manager2.Initialize(registry);
            var adapter2 = new SpiritSaveAdapter(manager2);
            
            adapter2.RestoreState(saveData);

            var restoredState = manager2.GetSpiritState("spirit_water_1");
            Assert.IsNotNull(restoredState);
            Assert.AreEqual(25f, restoredState.currentEnergy);

            Object.DestroyImmediate(goTick);
            Object.DestroyImmediate(go2);
        }
    }
}
