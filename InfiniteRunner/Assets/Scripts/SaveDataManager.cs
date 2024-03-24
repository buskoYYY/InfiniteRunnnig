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
        public List<string> playerNames; // ���� �� ���� �������,��� string

        public PlayerProfilesData(List<string> names) // ������� �����������
        {
            playerNames = names;
        }
    }
    static string GetSaveDir() // ������� ���������� ���� (��� string) ��� ��������� ������
    {
        return Application.persistentDataPath;
    }

    static string GetPlayerProfileFileName() // ������� ���������� ������. ��� ��� ������� ����� ��������� ����
    {
        return "players.json";
    }

    static string GetPlayerProfileSaveDir() // ����������������� ���� � �������� �����
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
        PlayerProfilesData data = new PlayerProfilesData(players);  // ��������� ��������� ������
        string dataJSON = JsonUtility.ToJson(data, true); // ���������� ��������� ������ ���� List
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
