using System.Collections.Generic;

namespace TheLastSprout.Save
{
    public class SaveRegistry
    {
        private readonly List<ISaveable> _saveables = new List<ISaveable>();

        public void Register(ISaveable saveable)
        {
            if (!_saveables.Contains(saveable))
            {
                _saveables.Add(saveable);
            }
        }

        public void Unregister(ISaveable saveable)
        {
            _saveables.Remove(saveable);
        }

        public void CaptureAll(SaveGameData data)
        {
            foreach (var saveable in _saveables)
            {
                saveable.CaptureState(data);
            }
        }

        public void RestoreAll(SaveGameData data)
        {
            foreach (var saveable in _saveables)
            {
                saveable.RestoreState(data);
            }
        }
    }
}
