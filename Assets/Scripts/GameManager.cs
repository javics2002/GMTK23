using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool invadersWin;
    bool invadersLose;

    public Text win;
    public Text lose;

    public float timeToChangeScene;

    public string username;
    public int score;

    static GameManager instance;

	string publicLeaderboardKey = "4f94383e7156f195db0b86d45e9b185084cbc46e46f58ca440c04a73c3093007";

	private void Awake() {
		if (!instance) { 
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
        invadersLose= false;
        invadersWin= false;

		username = Environment.UserName;
	}

    // Update is called once per frame
    void Update()
    {
        if (invadersLose)
        {
            lose.gameObject.SetActive(true);
            Invoke("goMenu", timeToChangeScene);
        }

        if (invadersWin)
        {
            win.gameObject.SetActive(true);
            Invoke("goMenu", timeToChangeScene);
        }
    }

    private void goMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void setInvadersWin()
    {
        invadersWin = true;
        SubmitScore();
    }

    public void setInvadersLose()
    {
        invadersLose = true;
    }

    public static GameManager GetInstance() {
        return instance;
    }

	public void SubmitScore() {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score);
	}
}
