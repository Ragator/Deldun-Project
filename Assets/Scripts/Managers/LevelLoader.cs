using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator crossfade;
    [SerializeField] private float crossfadeDuration = 1f;
    [SerializeField] private string doorParentName = "/Doorways/";
    [SerializeField] private GameManager myGameManager;

    private string targetDoor;
    //private bool transitionHappening = false;
    public bool TransitionHappening { get; private set; } = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadTargetScene(string targetScene, string targetDoorName)
    {
        if (TransitionHappening) return;

        myGameManager.isInputEnabled = false;
        TransitionHappening = true;
        targetDoor = targetDoorName;
        StartCoroutine(LoadLevel(targetScene));
    }

    public void LoadTargetScene(string targetScene)
    {
        if (TransitionHappening) return;

        myGameManager.isInputEnabled = false;
        TransitionHappening = true;
        StartCoroutine(LoadLevel(targetScene));
    }

    private IEnumerator LoadLevel(string targetScene)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(crossfadeDuration);

        SceneManager.LoadScene(targetScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        myGameManager.isInputEnabled = true;
        TransitionHappening = false;

        crossfade.SetTrigger("Start");

        if (targetDoor == null)
        {
            targetDoor = GameObject.FindWithTag(DeldunProject.Tags.defaultDoor).name;
        }

        if (targetDoor != null)
        {
            MovePlayer(targetDoor);
        }
    }

    private void MovePlayer(string targetDoor)
    {
        GameObject entryDoor = GameObject.Find(doorParentName + targetDoor);

        Transform playerTransform = GameObject.FindWithTag(DeldunProject.Tags.player).transform;

        playerTransform.position = entryDoor.GetComponent<Teleport>().PlayerSpawnPosition;
    }

    // TEMPORARY
    public void ResetLastDoor()
    {
        targetDoor = null;
    }
}