using System;
using System.Collections.Generic;

namespace TheLastSprout.Save
{
    [Serializable]
    public class SaveGameData
    {
        public SaveMetadata metadata;
        
        // Modules
        public List<StringObjectPair> worldState = new List<StringObjectPair>();
        public List<StringObjectPair> weatherState = new List<StringObjectPair>();
        public List<StringObjectPair> inventory = new List<StringObjectPair>();
        public List<StringObjectPair> quests = new List<StringObjectPair>();
        public List<StringObjectPair> npcRelations = new List<StringObjectPair>();
        public List<StringObjectPair> cropPlots = new List<StringObjectPair>();
        public List<StringObjectPair> townProjects = new List<StringObjectPair>();
        public List<StringObjectPair> automationSpirits = new List<StringObjectPair>();
        public List<StringObjectPair> machines = new List<StringObjectPair>();

        public SaveGameData()
        {
            metadata = new SaveMetadata();
        }
    }

    [Serializable]
    public struct StringObjectPair
    {
        public string key;
        public string jsonValue;
    }
}
