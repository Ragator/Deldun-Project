using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private Vector3 playerSpawnOffset;
    [SerializeField] private string targetDoorName;

    private LevelLoader levelLoader;
    public Vector3 PlayerSpawnPosition { get; private set; }

    private void Awake()
    {
        PlayerSpawnPosition = transform.position + playerSpawnOffset;
    }

    private void Start()
    {
        levelLoader = GameObject.FindWithTag(DeldunProject.Tags.levelLoader).GetComponent<LevelLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(DeldunProject.Tags.player))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            levelLoader.LoadTargetScene(targetScene, targetDoorName);
        }
    }
}
