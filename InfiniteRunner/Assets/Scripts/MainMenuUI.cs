using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] UISwitcher menuSwitcher;
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform howToPlayMenu;
    [SerializeField] Transform leaderBoardMenu;

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
}
