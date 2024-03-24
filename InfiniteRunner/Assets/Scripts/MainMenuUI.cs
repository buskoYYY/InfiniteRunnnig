using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] UISwitcher menuSwitcher;
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform howToPlayMenu;
    [SerializeField] Transform leaderBoardMenu;
    [SerializeField] Transform createPlayerProfile;
    [SerializeField] TMP_InputField newPlayerNameField;
    [SerializeField] TMP_Dropdown playerList;

    private void Start()
    {
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        SaveDataManager.GetSavedPlayerProfile(out List<string> players);
        playerList.ClearOptions();
        playerList.AddOptions(players);
    }

    public void StartGame()
    {
        GamePlayStatic.GetGameMode().LoadFirstLevel();
    }

    public void BackToMainMenu()
    {
        menuSwitcher.SetActiveUI(mainMenu);
    }

    public void GoToHowToPlayMenu()
    {
        menuSwitcher.SetActiveUI(howToPlayMenu);
    }
    public void GoToLeaderBoardMenu()
    {
        menuSwitcher.SetActiveUI(leaderBoardMenu);
    }

    public void QuitGame()
    {
        GamePlayStatic.GetGameMode().QuitGame();
    }

    public void SwitchToPlayerProfileMenu()
    {
        menuSwitcher.SetActiveUI(createPlayerProfile);
    }

    public void AddPlayerProfile()
    {
        string newPlayerName = newPlayerNameField.text;
        SaveDataManager.SavePlayerProfile(newPlayerName);
        UpdatePlayerList();
        BackToMainMenu();
    }

    public void DeleteSelectedPlayerProfile()
    {
        if (playerList.options.Count != 0)
        {
            string playerName = playerList.options[playerList.value].text;
            SaveDataManager.DeletePlayerProfile(playerName);
            UpdatePlayerList();
        }
    }
}
