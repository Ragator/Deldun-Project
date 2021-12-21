using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private AudioClip firstSceneMusic;
    [SerializeField] private string firstSceneName;

    private GameManager gameManager;
    private AudioManager audioManager;
    private LevelLoader levelLoader;

    private Button newGameButton;

    private void Start()
    {
        newGameButton = GetComponent<Button>();
        newGameButton.onClick.AddListener(StartNewGame);

        gameManager = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<GameManager>();
        audioManager = GameObject.FindWithTag(DeldunProject.Tags.audioManager).GetComponent<AudioManager>();
        levelLoader = GameObject.FindWithTag(DeldunProject.Tags.levelLoader).GetComponent<LevelLoader>();
    }

    private void StartNewGame()
    {
        gameManager.ShowUIElements();
        audioManager.SwitchMusic(firstSceneMusic);
        levelLoader.LoadTargetScene(firstSceneName);
    }
}
