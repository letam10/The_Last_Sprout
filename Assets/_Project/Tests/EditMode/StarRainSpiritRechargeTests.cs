using NUnit.Framework;
using TheLastSprout.Core;
using TheLastSprout.Core.Data;
using TheLastSprout.Spirits;
using UnityEngine;

namespace TheLastSprout.Tests.EditMode
{
    public class StarRainSpiritRechargeTests
    {
        [Test]
        public void StarRain_RechargesStarRainSpirits()
        {
            var registry = new ServiceRegistry();
            var eventBus = new EventBus();
            registry.Register(eventBus);
            
            var goTick = new GameObject();
            registry.Register(goTick.AddComponent<TickManager>());
            var saveManager = new TheLastSprout.Save.SaveManager();
            saveManager.Initialize(eventBus);
            registry.Register(saveManager);

            var go = new GameObject();
            var manager = go.AddComponent<AutomationSpiritManager>();
            manager.Initialize(registry);

            var def = ScriptableObject.CreateInstance<AutomationSpiritDefinition>();
            def.role = AutomationSpiritRole.CombatAssist;
            def.preferredEnergySource = EnergySourceType.StarRain;
            def.maxEnergy = 100f;

            manager.SpawnSpirit(def, "spirit_1", Vector3.zero);
            var state = manager.GetSpiritState("spirit_1");
            state.currentEnergy = 10f; // drained

            eventBus.Publish(new TheLastSprout.Weather.SpiritRechargeWeatherEvent());

            // 50% max energy recharge => +50 => 60
            Assert.AreEqual(60f, state.currentEnergy);

            Object.DestroyImmediate(goTick);
            Object.DestroyImmediate(go);
        }
    }
}
