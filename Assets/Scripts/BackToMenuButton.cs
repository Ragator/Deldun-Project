using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenuButton : UIButton
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private string menuSceneName;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private MenuManager menuManager;

    protected override void ButtonPressed()
    {
        if (levelLoader.TransitionHappening) return;

        menuManager.CloseAllMenus();
        gameManager.HideUIElements();
        audioManager.SwitchMusic(menuMusic);
        levelLoader.ResetLastDoor();
        levelLoader.LoadTargetScene(menuSceneName);
    }
}