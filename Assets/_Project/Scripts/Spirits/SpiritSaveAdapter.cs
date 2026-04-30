using TheLastSprout.Save;

namespace TheLastSprout.Spirits
{
    public class SpiritSaveAdapter : ISaveable
    {
        private readonly AutomationSpiritManager _manager;
        public string SaveId => "AutomationSpiritManager";

        public SpiritSaveAdapter(AutomationSpiritManager manager)
        {
            _manager = manager;
        }

        public void CaptureState(SaveGameData data)
        {
            _manager.CaptureStateInternal(data);
        }

        public void RestoreState(SaveGameData data)
        {
            _manager.RestoreStateInternal(data);
        }
    }
}
