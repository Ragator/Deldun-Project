using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField] private Animator crossfade;
    [SerializeField] private float crossfadeDuration = 1f;
    [SerializeField] private string doorParentName = "/Doorways/";

    private string targetDoor;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadTargetScene(string targetScene, string targetDoorName)
    {
        targetDoor = targetDoorName;
        StartCoroutine(LoadLevel(targetScene));
    }

    // TEMPORARY
    public void MenuLoadGame(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string targetScene)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(crossfadeDuration);

        SceneManager.LoadScene(targetScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (targetDoor != null)
        {
            MovePlayer(targetDoor);
        }
    }

    private void MovePlayer(string targetDoor)
    {
        GameObject entryDoor = GameObject.Find(doorParentName + targetDoor);

        Transform playerTransform = GameObject.FindWithTag("Player").transform;

        playerTransform.position = entryDoor.GetComponent<Teleport>().PlayerSpawnPosition;
    }
}