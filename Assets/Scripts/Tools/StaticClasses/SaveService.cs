using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;


[System.Serializable]
public class StringListWrapper
{
    public List<string> strings;
}

[System.Serializable]
public class SaveData
{
    public string startedQuestsString;
    public string finishedQuestsString;
    public string obviatedQuestsString;
    public float timePlayed;
}


public static class SaveService
{

    private static string SaveFilePath => Path.Combine(Application.persistentDataPath, "save.json");

    // API //

    public static bool SaveExists()
    {
        return File.Exists(SaveFilePath);
    }

    public static void Save(GameStateData inData)
    {
        SaveData savedData = ConvertGameDataToSaveData(inData);
        string json = JsonUtility.ToJson(savedData,true);
        File.WriteAllText(SaveFilePath, json);
    }
    public static GameStateData Load()
    {
        if (!File.Exists(SaveFilePath))
        {
            return null;
        }
        string json = File.ReadAllText(SaveFilePath);
        SaveData savedData = JsonUtility.FromJson<SaveData>(json);
        return ConvertSavedDataToGameData(savedData);
    }

    // Conversion Tools //
    private static SaveData ConvertGameDataToSaveData(GameStateData inData)
    {
        SaveData outData = new SaveData();

        List<string> startedQuestIDs = new List<string>();
        List<string> finishedQuestIDs = new List<string>();
        List<string> obviatedQuestIDs = new List<string>();


        foreach (string _id in inData.questStatus.Keys)
        {
            switch (inData.questStatus[_id])
            {
                case EnumQuestStatus.STARTED:
                    if (!startedQuestIDs.Contains(_id)) { startedQuestIDs.Add(_id); }
                    break;
                case EnumQuestStatus.FINISHED:
                    if (!finishedQuestIDs.Contains(_id)) { finishedQuestIDs.Add(_id); }
                    break;
                case EnumQuestStatus.OBVIATED:
                    if (!obviatedQuestIDs.Contains(_id)) { obviatedQuestIDs.Add(_id); }
                    break;
                default: break;
            }
        }


        StringListWrapper startedQuestsWrapper = new StringListWrapper();
        startedQuestsWrapper.strings = startedQuestIDs;
        string startedQuestsString = JsonUtility.ToJson(startedQuestsWrapper);

        StringListWrapper finishedQuestsWrapper = new StringListWrapper();
        finishedQuestsWrapper.strings = finishedQuestIDs;
        string finishedQuestsString = JsonUtility.ToJson(finishedQuestsWrapper);

        StringListWrapper obviatedQuestsWrapper = new StringListWrapper();
        obviatedQuestsWrapper.strings = obviatedQuestIDs;
        string obviatedQuestsString = JsonUtility.ToJson(obviatedQuestsWrapper);

        outData.startedQuestsString = startedQuestsString;
        outData.finishedQuestsString = finishedQuestsString;
        outData.obviatedQuestsString= obviatedQuestsString;
        outData.timePlayed = inData.timePlayedSeconds;

        return outData;
    }

    private static GameStateData ConvertSavedDataToGameData(SaveData inData)
    {
        GameStateData outData = new GameStateData();

        Dictionary<string, EnumQuestStatus> questsStatus = new Dictionary<string, EnumQuestStatus>();
        List<string> startedList = GetStringListFromJson(inData.startedQuestsString);
        List<string> finishedList = GetStringListFromJson(inData.finishedQuestsString);
        List<string> obviatedList = GetStringListFromJson(inData.obviatedQuestsString);

        foreach(string questID in startedList) { questsStatus[questID] = EnumQuestStatus.STARTED; }
        foreach(string questID in finishedList) { questsStatus[questID] = EnumQuestStatus.FINISHED; }
        foreach(string questID in obviatedList) { questsStatus[questID] = EnumQuestStatus.OBVIATED; }

        outData.questStatus = questsStatus;
        outData.timePlayedSeconds = inData.timePlayed;
        return outData;
    }


    private static List<string> GetStringListFromJson(string inString)
    {
        List<string> outList = new List<string>();

        if (string.IsNullOrEmpty(inString)) { return outList; }

        StringListWrapper wrapper = JsonUtility.FromJson<StringListWrapper>(inString);

        outList = wrapper.strings;

        return outList;
    }



}
