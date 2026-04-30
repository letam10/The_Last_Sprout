using System;

namespace TheLastSprout.Save
{
    [Serializable]
    public class SaveMetadata
    {
        public int slotIndex;
        public string saveVersion;
        public long timestampUtc;
        public double playtimeSeconds;
        public int inGameDay;
        public string currentRegionId;
        public string checksum;
        public string thumbnailPath;
    }
}
