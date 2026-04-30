using UnityEngine;

namespace TheLastSprout.Spirits
{
    [CreateAssetMenu(fileName = "SpiritBalanceConfig", menuName = "The Last Sprout/Config/Spirit Balance Config")]
    public class SpiritBalanceConfig : ScriptableObject
    {
        public int maxActiveCombatSpirits = 3;
        public int baseFarmSpiritLimit = 5;
        public float energyDecayPerTick = 0.1f;
        public float rechargeRateMultiplier = 1.0f;
    }
}
