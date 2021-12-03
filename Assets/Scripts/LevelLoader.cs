using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator crossfade;
    [SerializeField] private float crossfadeDuration = 1f;
    [SerializeField] private string doorParentName = "/Doorways/";

    private string targetDoor;
    private bool transitionHappening = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadTargetScene(string targetScene, string targetDoorName)
    {
        if (transitionHappening) return;

        transitionHappening = true;
        targetDoor = targetDoorName;
        StartCoroutine(LoadLevel(targetScene));
    }

    public void LoadTargetScene(string targetScene)
    {
        if (transitionHappening) return;

        transitionHappening = true;
        StartCoroutine(LoadLevel(targetScene));
    }

    IEnumerator LoadLevel(string targetScene)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(crossfadeDuration);

        SceneManager.LoadScene(targetScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transitionHappening = false;

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