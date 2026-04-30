using System.Collections.Generic;
using UnityEngine;

namespace TheLastSprout.Core
{
    public enum TickGroup
    {
        Simulation,
        AI,
        Weather,
        NPC,
        Machines
    }

    public interface ITickable
    {
        void Tick(float deltaTime);
    }

    public class TickManager : MonoBehaviour
    {
        private readonly Dictionary<TickGroup, List<ITickable>> _tickables = new Dictionary<TickGroup, List<ITickable>>();
        private readonly Dictionary<TickGroup, float> _tickIntervals = new Dictionary<TickGroup, float>();
        private readonly Dictionary<TickGroup, float> _tickTimers = new Dictionary<TickGroup, float>();

        public void Initialize()
        {
            // Default intervals
            _tickIntervals[TickGroup.Simulation] = 0.1f;
            _tickIntervals[TickGroup.AI] = 0.2f;
            _tickIntervals[TickGroup.Weather] = 1.0f; // Weather updates less frequently
            _tickIntervals[TickGroup.NPC] = 0.5f;
            _tickIntervals[TickGroup.Machines] = 0.5f;

            foreach (TickGroup group in System.Enum.GetValues(typeof(TickGroup)))
            {
                _tickables[group] = new List<ITickable>();
                _tickTimers[group] = 0f;
            }
        }

        public void Register(ITickable tickable, TickGroup group)
        {
            if (!_tickables[group].Contains(tickable))
            {
                _tickables[group].Add(tickable);
            }
        }

        public void Unregister(ITickable tickable, TickGroup group)
        {
            if (_tickables.ContainsKey(group))
            {
                _tickables[group].Remove(tickable);
            }
        }

        private void Update()
        {
            float dt = Time.deltaTime;

            // Iterate over an array to avoid allocation/enumeration issues if modified during tick
            // In a real optimized scenario, we'd use custom arrays. Here we keep it clean.
            foreach (var kvp in _tickIntervals)
            {
                TickGroup group = kvp.Key;
                _tickTimers[group] += dt;

                if (_tickTimers[group] >= kvp.Value)
                {
                    float groupDt = _tickTimers[group];
                    _tickTimers[group] = 0f;
                    
                    var list = _tickables[group];
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        list[i].Tick(groupDt);
                    }
                }
            }
        }
    }
}
