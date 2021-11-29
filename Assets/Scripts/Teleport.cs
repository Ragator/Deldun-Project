using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private Vector3 playerSpawnOffset;
    [SerializeField] private string targetDoorName;

    public Vector3 PlayerSpawnPosition { get; private set; }

    private void Awake()
    {
        PlayerSpawnPosition = transform.position + playerSpawnOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            LevelLoader.instance.LoadTargetScene(targetScene, targetDoorName);
        }
    }
}
