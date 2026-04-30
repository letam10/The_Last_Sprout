using System.Collections.Generic;

namespace TheLastSprout.Spirits
{
    public class SpiritEnergySystem
    {
        private readonly SpiritBalanceConfig _config;

        public SpiritEnergySystem(SpiritBalanceConfig config)
        {
            _config = config;
        }

        public void DrainEnergy(SpiritRuntimeState state, float amount)
        {
            state.currentEnergy -= amount;
            if (state.currentEnergy < 0) state.currentEnergy = 0;
        }

        public void RechargeEnergy(SpiritRuntimeState state, float amount, float maxEnergy)
        {
            float rate = _config != null ? _config.rechargeRateMultiplier : 1.0f;
            state.currentEnergy += amount * rate;
            if (state.currentEnergy > maxEnergy) state.currentEnergy = maxEnergy;
        }

        public bool HasEnoughEnergy(SpiritRuntimeState state, float required)
        {
            return state.currentEnergy >= required;
        }
    }
}
