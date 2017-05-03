using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    public Text text;



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// sets the score text
    /// </summary>
    void Start () {
        text.text = GameObject.FindObjectOfType<GameController>().getScore();
    }
}
