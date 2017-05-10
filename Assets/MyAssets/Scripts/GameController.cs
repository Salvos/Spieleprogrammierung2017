using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Range(-100, 0)]
    [Header("Minimalhöhe / Player death")]
    public float bottomLevel = -1;

    [Range(0, 10)]
    [Header("Respawn nach Tod")]
    public float respawnAfterDeath = 2;

    [Range(-100, 0)]
    [Header("Respawn nach Finish")]
    public float respawnAfterFinish = 5;

    private Spawner Spawner;
    private MyCamera mycamera;
    private Player player;

    private int score = 0;
    private int lifes = 3;


    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// sets gameController not to be destroyed
    /// </summary>
    void Start () {
        GameObject.DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
	}
	


    ///===================================///
    ///==========PRIVATE METHODS==========///
    ///===================================///

    /// <summary>
    /// respawns the player
    /// gets triggered by playerDeath- and playerFinish-method
    /// later by the checkpoint object(?)
    /// </summary>
    private void respawnPlayer()
    {
        // Hole den letzten Checkpoint und spawne den Spieler DORT!

        Spawner.spawnPlayer();
    }

    /// <summary>
    /// Destroys the player & notificate the camera
    /// </summary>
    private void destroyPlayer()
    {
        Destroy(player.gameObject);
        mycamera.SetPlayer(null);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Start" && scene.name != "End")
        {
            updateLifes();
            GameObject.FindGameObjectWithTag("LevelName").GetComponent<Text>().text = "\""+scene.name+"\"";
        }
    }

    private void updateLifes()
    {
        string lifeText = "";
        for(int i = 0; i < lifes; i++)
        {
            lifeText += "❤";
        }
        GameObject.FindGameObjectWithTag("Lifes").GetComponent<Text>().text = lifeText;
    }

    ///==================================///
    ///==========PUBLIC METHODS==========///
    ///==================================///

    /// <summary>
    /// register the playerobject for this script
    /// gets triggered by the Playerscript ("Start"-method)
    /// </summary>
    /// <param name="player">Playerobject</param>
    public void registerPlayer(Player player)
    {
        this.player = player;
        mycamera.SetPlayer(player);
    }

    /// <summary>
    /// register the playerobject for this script
    /// gets triggered by the Playerscript ("Start"-method)
    /// </summary>
    /// <param name="player">Playerobject</param>
    public void registerSpawner(Spawner spawner)
    {
        this.Spawner = spawner;
        this.Spawner.spawnPlayer();
    }

    /// <summary>
    /// register the playerobject for this script
    /// gets triggered by the Playerscript ("Start"-method)
    /// </summary>
    /// <param name="player">Playerobject</param>
    public void registerCamera(MyCamera mycamera)
    {
        this.mycamera = mycamera;
    }

    /// <summary>
    /// method defines what happens if the player dies
    /// - print to console
    /// - respawn the player
    /// </summary>
    public void playerDeath()
    {
        // destroy the player
        destroyPlayer();

        if(lifes > 1)
        {
            lifes--;

            updateLifes();

            // [OPTIONAL] respawn the player
            Invoke("respawnPlayer", respawnAfterDeath);
        } else
        {
            // print to console
            Debug.Log("Leider verloren");

            // TODO
            // GameOver Screen! Like in Issue
            LoadLevel("Start");
        }
    }

    /// <summary>
    /// method defines what happens if the player reach the finish
    /// - disable moving and print to console
    /// - destroy the player
    /// - respawn the player
    /// </summary>
    public void playerFinish()
    {
        // disable moving and print to console
        player.GetComponent<Player>().movingEnabled = false;
        Debug.Log("Ziel erreicht");

        // [OPTIONAL] destroy player
        destroyPlayer();

        // Enables the Canvas-Element
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().enabled = true;

        //LoadLevel("End");
    }

    /// <summary>
    /// resets score
    /// </summary>
    public void startGame()
    {
        score = 0;
        LoadLevel("Level1");
    }

    /// <summary>
    /// changes scene
    /// gets the Spawner object, trigger the animation and spawns a player
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
        lifes = 3;
    }

    /// <summary>
    /// stops game
    /// </summary>
    public void stopGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    /// <summary>
    /// returns the score as string
    /// </summary>
    /// <returns></returns>
    public string getScore()
    {
        score = Random.Range(1, 1000000);
        return score.ToString();
    }
}