using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // This is an instance of itself that can be accessed from anywhere in our code (it is also static)


    // Resources for the game, can contain player sprites, weapon sprites, gold amount, exp amount, progress, etc.
    // public List<int> xpTable;
    // publib List<Sprite> playerSprites;

    // References -> Can be the player, the weapon they have, potential upgrades, etc.
    public Player player;

    public FloatingTextManager floatingTextManager;

    // Logic that will be preserved in the save state
    public int numArtifacts; // starts at 0! Goes to 2!
    public int experience;

    // We can only have ONE instance of GameManager
    private void Awake()
    {   
        if (GameManager.instance != null) // ensuring that this remains a singleton
        {
            Destroy(gameObject);
            return;
        }

        // PlayerPrefs.DeleteAll(); This function deletes all the save data

        instance = this;
        numArtifacts = 0;
        // SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject); // make sure that a single instance can persist between scene changes
    }

    // Call for floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration, int font)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration, font);
    }

    // Saving the game should happen before room-change and when we exit the game
    /*
    * INT numArtifacts
    * INT areasUnlocked
    * INT experience
    * INT charLevel
    
    public void SaveState()
    {
        string s = "";
        
        s += numArtifacts.ToString() + "|";
        s += areasUnlocked.ToString() + "|";
        s += experience.ToString() + "|";
        s += charLevel.ToString() + "|";

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("SaveState");
    }

    // We only want to call load state once a GameManager is created which is when we re-open the game
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if(!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // "1|2|20|3" -> ["1","2","20","3"]
        numArtifacts = int.Parse(data[0]);
        areasUnlocked = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        charLevel = int.Parse(data[3]);

        Debug.Log("LoadState");
    }
    */
}
