using NUnit.Framework;
using TheLastSprout.Spirits;

namespace TheLastSprout.Tests.EditMode
{
    public class SpiritEnergyTests
    {
        [Test]
        public void SpiritEnergy_DrainsCorrectly()
        {
            var config = UnityEngine.ScriptableObject.CreateInstance<SpiritBalanceConfig>();
            var energySystem = new SpiritEnergySystem(config);
            
            var state = new SpiritRuntimeState { currentEnergy = 100f };
            
            Assert.IsTrue(energySystem.HasEnoughEnergy(state, 10f));
            energySystem.DrainEnergy(state, 10f);
            Assert.AreEqual(90f, state.currentEnergy);
        }

        [Test]
        public void SpiritEnergy_DoesNotGoBelowZero()
        {
            var config = UnityEngine.ScriptableObject.CreateInstance<SpiritBalanceConfig>();
            var energySystem = new SpiritEnergySystem(config);
            
            var state = new SpiritRuntimeState { currentEnergy = 5f };
            
            energySystem.DrainEnergy(state, 10f);
            Assert.AreEqual(0f, state.currentEnergy);
        }
    }
}
