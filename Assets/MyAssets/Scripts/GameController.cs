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

    private Spawner Spawner;
    private MyCamera mycamera;
    private Player player;

    private Transform deathCanvas;
    private Transform finishCanvas;
    private Transform ingameUI;

    private int lifes = 3;
    private int sceneNumber = 0;

    private float startTime;
    private float storedTime;
    private bool pausedTimer = true;
    private float score = 0;



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// sets gameController not to be destroyed
    /// and add callback function for "onSceneLoaded"
    /// get all panels
    /// and load 'Start'-scene
    /// </summary>
    void Start () {
        GameObject.DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        deathCanvas     = transform.Find("DeathCanvas");
        finishCanvas    = transform.Find("FinishCanvas");
        ingameUI        = transform.Find("IngameUI");

        // Load Start-Scene
        SceneManager.LoadScene("Start");
    }

    /// <summary>
    /// used to update the timer if ingame
    /// </summary>
    void Update()
    {
        updateTimer();
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
        // get Build-Index Number (to select next level)
        sceneNumber = scene.buildIndex;

        if (scene.name == "Start")
        {
            // disable panels
            ingameUI.gameObject.SetActive(false);

            score = 0;
            storedTime = 0;

        }
        else if (scene.name == "End")
        {
            // disable panels
            ingameUI.gameObject.SetActive(false);

            GameObject.Find("EndMenue/Scorepoints").GetComponent<Text>().text = getScoreFormat();

        }
        else if (scene.name != "Init")
        {
            deathCanvas.gameObject.SetActive(false);
            finishCanvas.gameObject.SetActive(false);
            ingameUI.gameObject.SetActive(true);

            lifes = 3;

            updateLifes();

            // update level name
            ingameUI.Find("LevelName").GetComponent<Text>().text = "\"" + scene.name + "\"";

            startTimer();
        }

    }

    /// <summary>
    /// sets the startTime for the Timer
    /// </summary>
    private void startTimer()
    {
        pausedTimer = false;
        startTime = Time.time;
    }

    /// <summary>
    /// stores the time (for levelchange, death...)
    /// </summary>
    private void pauseTimer()
    {
        pausedTimer = true;
        storedTime += Time.time - startTime;
    }

    /// <summary>
    /// updates the Score / Timer Value by adding the stored time to the actual time
    /// </summary>
    private void updateTimer()
    {
        if (!pausedTimer)
        {
            score = storedTime + (Time.time - startTime);
        }
        ingameUI.Find("Timer").GetComponent<Text>().text = getScoreFormat();
    }

    /// <summary>
    /// returns the score as string in mm:ss:msmsms
    /// </summary>
    /// <returns></returns>
    private string getScoreFormat()
    {
        int minutes = (int)(score / 60);
        int seconds = (int) (score - (minutes*60));
        int ms = (int)((score - (seconds + (minutes*60))) * 1000);


        //score = Random.Range(1, 1000000);
        return minutes.ToString("D2") + ":" + seconds.ToString("D2") + ":" + ms.ToString("D3");
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
        for (int i = 0; i < lifes; i++)
        {
            lifeText += "❤";
        }

        ingameUI.Find("LebenScore").GetComponent<Text>().text = lifeText;
        deathCanvas.Find("LebenScore").GetComponent<Text>().text = lifeText;

        // deactivate respawn
        if (lifes < 1)
        {
            deathCanvas.Find("Button_Respawn").gameObject.SetActive(false);
        }
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
    /// changes scene
    /// </summary>
    /// <param name="level">Levelname</param>
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// load the next Level (by build index)
    /// resets the lifes and disable canvas
    /// </summary>
    public void nextLevel()
    {
        finishCanvas.gameObject.SetActive(false);
        lifes = 3;
        SceneManager.LoadScene(++sceneNumber);
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
    /// method defines what happens if the player dies
    /// - print to console
    /// - respawn the player
    /// - pause the timer
    /// </summary>
    public void playerDeath()
    {
        pauseTimer();

        deathCanvas.gameObject.SetActive(true);

        // destroy the player
        destroyPlayer();

        if (lifes > 0)
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
    /// - pause the Timer
    /// </summary>
    public void playerFinish()
    {
        pauseTimer();

        // disable moving and print to console
        player.GetComponent<Player>().movingEnabled = false;
        Debug.Log("Ziel erreicht");

        // [OPTIONAL] destroy player
        destroyPlayer();

        finishCanvas.Find("Scorepoints").GetComponent<Text>().text = getScoreFormat();

        // Enables the Canvas-Element
        finishCanvas.gameObject.SetActive(true);
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

        startTimer();
    }
}