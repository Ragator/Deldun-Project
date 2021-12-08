using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DeldunProject
{
    public static class Tags
    {
        public const string player = "Player";
        public const string chest = "Chest";
        public const string sword = "Sword";
        public const string enemy = "Enemy";
        public const string gameManager = "Game Manager";
        public const string levelLoader = "Level Loader";
        public const string audioManager = "Audio Manager";
    }
}

public class DataManager : MonoBehaviour
{
    private string playerSaveFileName = "/playerInfo.dat";

    public void SaveGame()
    {
        BinaryFormatter myBinaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + playerSaveFileName, FileMode.Open);
    }

    public void LoadGame()
    {

    }
}

[Serializable]
class PlayerData
{
    public float health;
    public float currency;
}
