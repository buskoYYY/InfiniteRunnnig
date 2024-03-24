using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveDataManager 
{
    [Serializable]

    class PlayerProfilesData
    {
        public List<string> playerNames; // лист из имен игроков,тип string

        public PlayerProfilesData(List<string> names) // создали конструктор
        {
            playerNames = names;
        }
    }
    static string GetSaveDir() // функци€ возвращает путь (тип string) где хран€тьс€ данные
    {
        return Application.persistentDataPath;
    }

    static string GetPlayerProfileFileName() // функци€ возвращает строку. »м€ под которым будет хранитьс€ файл
    {
        return "players.json";
    }

    static string GetPlayerProfileSaveDir() // функци€возвращает путь и название файла
    {
        return GetSaveDir() + "/" + GetPlayerProfileFileName();
    }

    public static void SavePlayerProfile(string playerName)
    {
        GetSavedPlayerProfile(out List<string> players);
        if (players.Contains(playerName))
        {
            return;
        }

        players.Insert(0, playerName);

        SavePlayerProfilesFromList(players);
    }

    private static void SavePlayerProfilesFromList(List<string> players)
    {
        PlayerProfilesData data = new PlayerProfilesData(players);  // создаетс€ экземпл€р класса
        string dataJSON = JsonUtility.ToJson(data, true); // передаетс€ экземпл€р класса типа List
        File.WriteAllText(GetPlayerProfileSaveDir(), dataJSON);
    }

    public static bool GetSavedPlayerProfile(out List<string> data)
    {
        if(File.Exists(GetPlayerProfileSaveDir()))
        {
            string dataJSON = File.ReadAllText(GetPlayerProfileSaveDir());
            PlayerProfilesData loadedData = JsonUtility.FromJson<PlayerProfilesData>(dataJSON);
            data = loadedData.playerNames;
            return true;
        }

        data = new List<string>();
        return false;
    }

    public static void DeletePlayerProfile(string playerName)
    {
        GetSavedPlayerProfile(out List<string> players);
        players.Remove(playerName);

        SavePlayerProfilesFromList(players) ;
    }
}
