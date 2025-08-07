using System;

public class DictionaryModels
{
    [Serializable]
    public class RootWrapper
    {
        public DictionaryEntry[] items;
    }

    [Serializable]
    public class DictionaryEntry
    {
        public string word;
        public string phonetic;
        public Meaning[] meanings;
    }

    [Serializable]
    public class Meaning
    {
        public string partOfSpeech;
        public Definition[] definitions;
    }

    [Serializable]
    public class Definition
    {
        public string definition;
        public string example;
        public string[] synonyms;
        public string[] antonyms;
    }
}
