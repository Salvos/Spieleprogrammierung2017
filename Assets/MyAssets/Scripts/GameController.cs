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

    private Transform deathCanvas;
    private Transform finishCanvas;
    private Transform ingameUI;
    private Transform startMenue;
    private Transform endMenue;

    private int score = 0;
    private int lifes = 3;
    private int sceneNumber = 0;



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// sets gameController not to be destroyed
    /// and add callback function for "onSceneLoaded"
    /// </summary>
    void Start () {
        GameObject.DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        deathCanvas     = transform.Find("DeathCanvas");
        finishCanvas    = transform.Find("FinishCanvas");
        ingameUI        = transform.Find("IngameUI");
        startMenue      = transform.Find("StartMenue");
        endMenue        = transform.Find("EndMenue");

        SceneManager.LoadScene("Start");
    }



    ///===================================///
    ///==========PRIVATE METHODS==========///
    ///===================================///

    /// <summary>
    /// triggers if the scene is loaded
    /// gets used for temporary objects
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneNumber = scene.buildIndex;

        if (scene.name != "Start" && scene.name != "End" && scene.name != "Init")
        {
            finishCanvas.gameObject.SetActive(false);
            updateLifes();
            ingameUI.gameObject.SetActive(true);
            ingameUI.Find("LevelName").GetComponent<Text>().text = "\"" + scene.name + "\"";
        } else if(scene.name == "Start")
        {
            endMenue.gameObject.SetActive(false);
            startMenue.gameObject.SetActive(true);
            ingameUI.gameObject.SetActive(false);
        } else if(scene.name == "End")
        {
            endMenue.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// respawns the player
    /// gets triggered by playerDeath- and playerFinish-method
    /// later by the checkpoint object(?)
    /// </summary>
    public void respawnPlayer()
    {
        deathCanvas.gameObject.SetActive(false);

        // Hole den letzten Checkpoint und spawne den Spieler DORT!
        this.Spawner.spawnPlayer();
    }

    /// <summary>
    /// Destroys the player & notificate the camera
    /// </summary>
    private void destroyPlayer()
    {
        mycamera.SetPlayer(null);
        Destroy(player.gameObject);
    }

    private void updateLifes()
    {
        string lifeText = "";
        for(int i = 0; i < lifes; i++)
        {
            lifeText += "❤";
        }

        ingameUI.Find("LebenScore").GetComponent<Text>().text = lifeText;
        deathCanvas.Find("LebenScore").GetComponent<Text>().text = lifeText;

        // deactivate respawn
        if (lifes < 1)
        {
            deathCanvas.Find("RespawnButton").gameObject.SetActive(false);
        }
    }

    public void nextLevel()
    {
        deathCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
        lifes = 3;
        SceneManager.LoadScene(++sceneNumber);
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
        deathCanvas.gameObject.SetActive(true);

        // destroy the player
        destroyPlayer();



        if(lifes > 0)
        {
            lifes--;
        }

        updateLifes();
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
        finishCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// resets score
    /// </summary>
    public void startGame()
    {
        score = 0;
        LoadLevel("Level1");
        startMenue.gameObject.SetActive(false);
    }

    /// <summary>
    /// changes scene
    /// gets the Spawner object, trigger the animation and spawns a player
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(string level)
    {
        deathCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
        lifes = 3;
        SceneManager.LoadScene(level);
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