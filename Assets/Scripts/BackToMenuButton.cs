using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private string menuSceneName;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private LevelLoader levelLoader;

    private Button backToMenuButton;

    private void Start()
    {
        backToMenuButton = GetComponent<Button>();
        backToMenuButton.onClick.AddListener(ExitToMenu);
    }

    private void ExitToMenu()
    {
        gameManager.HideUIElements();
        audioManager.SwitchMusic(menuMusic);
        levelLoader.ResetLastDoor();
        levelLoader.LoadTargetScene(menuSceneName);
    }
}