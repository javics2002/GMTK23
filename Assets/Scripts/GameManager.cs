using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

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
		username = Environment.UserName;
	}

    // Update is called once per frame
    void Update()
    {
    }

    private void goMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void setInvadersWin()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("UIWin");

        if (obj != null)
        {
            obj.GetComponent<TextMeshProUGUI>().enabled = true;
            Invoke("goMenu", timeToChangeScene);
            SubmitScore();
        }
    }

    public void setInvadersLose()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("UILose");

        if (obj != null)
        {
            obj.GetComponent<TextMeshProUGUI>().enabled = true;
            Invoke("goMenu", timeToChangeScene);

        }
    }

    public static GameManager GetInstance() {
        return instance;
    }

	public void SubmitScore() {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score);
	}
}
