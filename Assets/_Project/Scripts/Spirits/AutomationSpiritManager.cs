using System.Collections.Generic;
using TheLastSprout.Core;
using TheLastSprout.Core.Data;
using TheLastSprout.Save;
using TheLastSprout.Weather;
using UnityEngine;

namespace TheLastSprout.Spirits
{
    public class AutomationSpiritManager : MonoBehaviour, ITickable
    {
        [SerializeField] private SpiritBalanceConfig balanceConfig;

        private ServiceRegistry _registry;
        private EventBus _eventBus;
        private SpiritTaskScheduler _scheduler;
        private SpiritEnergySystem _energySystem;
        private SpiritSaveAdapter _saveAdapter;

        // Runtime data
        private Dictionary<string, SpiritRuntimeState> _activeSpirits = new Dictionary<string, SpiritRuntimeState>();
        private Dictionary<string, AutomationSpiritDefinition> _spiritDefinitions = new Dictionary<string, AutomationSpiritDefinition>(); // Should be loaded via Addressables or a DataManager

        private int _currentCombatSpirits = 0;

        public void Initialize(ServiceRegistry registry)
        {
            _registry = registry;
            _eventBus = _registry.Get<EventBus>();
            _scheduler = new SpiritTaskScheduler();
            _energySystem = new SpiritEnergySystem(balanceConfig);
            _saveAdapter = new SpiritSaveAdapter(this);

            _registry.Get<TickManager>().Register(this, TickGroup.Simulation);
            _registry.Get<SaveManager>().RegisterSaveable(_saveAdapter);

            // Subscribe to weather events for energy recharge
            _eventBus.Subscribe<SpiritRechargeWeatherEvent>(OnStarRainRecharge);
        }

        private void OnDestroy()
        {
            if (_registry != null)
            {
                _registry.Get<TickManager>()?.Unregister(this, TickGroup.Simulation);
                _registry.Get<SaveManager>()?.UnregisterSaveable(_saveAdapter);
                _eventBus?.Unsubscribe<SpiritRechargeWeatherEvent>(OnStarRainRecharge);
            }
        }

        public void SpawnSpirit(AutomationSpiritDefinition def, string instanceId, Vector3 position)
        {
            if (def.role == AutomationSpiritRole.CombatAssist && _currentCombatSpirits >= (balanceConfig != null ? balanceConfig.maxActiveCombatSpirits : 3))
            {
                Debug.LogWarning("Max combat spirits reached.");
                return;
            }

            var state = new SpiritRuntimeState
            {
                instanceId = instanceId,
                definitionId = def.StableId,
                level = 1,
                currentXp = 0,
                currentEnergy = def.maxEnergy,
                isActive = true,
                cooldownTimer = 0f
            };

            _activeSpirits[instanceId] = state;
            _spiritDefinitions[def.StableId] = def;

            if (def.role == AutomationSpiritRole.CombatAssist) _currentCombatSpirits++;

            _eventBus.Publish(new SpiritSpawnEvent { SpiritId = instanceId });
        }

        public void Tick(float deltaTime)
        {
            foreach (var kvp in _activeSpirits)
            {
                var state = kvp.Value;
                if (!state.isActive) continue;

                if (state.cooldownTimer > 0)
                {
                    state.cooldownTimer -= deltaTime;
                    continue;
                }

                if (_spiritDefinitions.TryGetValue(state.definitionId, out var def))
                {
                    // Placeholder for actual execution.
                    // Request task from scheduler
                    var task = _scheduler.GetBestTask(def.role, Vector3.zero, state.level); // position mock
                    if (task.HasValue)
                    {
                        // Check energy
                        float cost = 5f; // mock
                        if (_energySystem.HasEnoughEnergy(state, cost))
                        {
                            _energySystem.DrainEnergy(state, cost);
                            state.cooldownTimer = 2.0f; // mock base cooldown
                            _eventBus.Publish(new SpiritTaskCompletedEvent { SpiritId = state.instanceId, TaskType = task.Value.taskType, Result = SpiritTaskResult.Success });
                        }
                        else
                        {
                            state.cooldownTimer = 5.0f; // wait before retrying
                        }
                    }
                }
            }
        }

        private void OnStarRainRecharge(SpiritRechargeWeatherEvent e)
        {
            foreach (var state in _activeSpirits.Values)
            {
                if (_spiritDefinitions.TryGetValue(state.definitionId, out var def))
                {
                    if (def.preferredEnergySource == EnergySourceType.StarRain)
                    {
                        _energySystem.RechargeEnergy(state, def.maxEnergy * 0.5f, def.maxEnergy);
                    }
                }
            }
        }

        internal void CaptureStateInternal(SaveGameData data)
        {
            foreach (var kvp in _activeSpirits)
            {
                string json = JsonUtility.ToJson(kvp.Value);
                data.automationSpirits.Add(new StringObjectPair { key = kvp.Key, jsonValue = json });
            }
        }

        internal void RestoreStateInternal(SaveGameData data)
        {
            _activeSpirits.Clear();
            _currentCombatSpirits = 0;

            foreach (var pair in data.automationSpirits)
            {
                if (!string.IsNullOrEmpty(pair.jsonValue))
                {
                    var state = JsonUtility.FromJson<SpiritRuntimeState>(pair.jsonValue);
                    _activeSpirits[state.instanceId] = state;
                    
                    // In a real system, we'd look up the definition to recalculate _currentCombatSpirits.
                }
            }
        }

        // For unit testing
        public SpiritRuntimeState GetSpiritState(string id) => _activeSpirits.TryGetValue(id, out var s) ? s : null;
        public SpiritTaskScheduler GetScheduler() => _scheduler;
    }
}
