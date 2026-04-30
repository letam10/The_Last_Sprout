using System;
using System.Collections.Generic;

namespace TheLastSprout.Save
{
    [Serializable]
    public class SaveGameData
    {
        public SaveMetadata metadata;
        
        // Using string keys for simple JSON serialization with Unity's JsonUtility if wrapped, 
        // or a custom JSON parser like Newtonsoft.Json. 
        // For standard Unity JsonUtility, dictionaries aren't supported natively, so we'd use List<KeyValuePairWrapper>.
        // To keep the architecture clean and generic, we use List of wrappers.
        public List<StringObjectPair> worldState = new List<StringObjectPair>();
        public List<StringObjectPair> weatherState = new List<StringObjectPair>();
        
        // Future placeholders
        public List<StringObjectPair> inventory = new List<StringObjectPair>();
        public List<StringObjectPair> quests = new List<StringObjectPair>();
        public List<StringObjectPair> npcRelations = new List<StringObjectPair>();
        public List<StringObjectPair> cropPlots = new List<StringObjectPair>();
        public List<StringObjectPair> townProjects = new List<StringObjectPair>();

        public SaveGameData()
        {
            metadata = new SaveMetadata();
        }
    }

    [Serializable]
    public struct StringObjectPair
    {
        public string key;
        public string jsonValue; // Store complex objects as JSON string internally
    }
}
