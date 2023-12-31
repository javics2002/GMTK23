using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{

    public float timeToChangeScene;

    public string username;
    public int score;
    public int round;

    public bool playing = false;

    public GameObject points;

    static GameManager instance;

	string publicLeaderboardKey = "4f94383e7156f195db0b86d45e9b185084cbc46e46f58ca440c04a73c3093007";

	private void Awake() {
		if (!instance) { 
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

		username = Environment.UserName;
        score = 0;
        round = 1;
	}

    private void goMenu()
    {
        SceneManager.LoadScene("Menu");
    }

	private void nextRound() {
		SceneManager.LoadScene("Spacee");
	}

	public void setInvadersWin()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("UIWin");

        if (obj != null)
        {
            obj.GetComponent<TextMeshProUGUI>().enabled = true;

            playing = false;
            
            StartCoroutine(GetPoints());
			
            round++;
		}
    }

    IEnumerator GetPoints() {
		var enemyShips = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (var enemy in enemyShips) {
            if(!enemy)
                continue;
            Ship ship = enemy.GetComponent<Ship>();
            if(!ship)
                continue;

            int score = ship.score;
			IncreaseScore(score);
            Instantiate(points, enemy.transform.position, Quaternion.identity)
                .GetComponentInChildren<TextMeshPro>().text = score.ToString();
            yield return new WaitForSeconds(0.05f);
        }

		Invoke("nextRound", timeToChangeScene);
	}

    public void setInvadersLose()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("UILose");

        if (obj != null)
        {
            obj.GetComponent<TextMeshProUGUI>().enabled = true;
            Invoke("goMenu", timeToChangeScene);

			playing = false;

			SubmitScore();
			score = 0;
            round = 1;
		}
    }

    public static GameManager GetInstance() {
        return instance;
    }

	public void SubmitScore() {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score);
	}

    public void IncreaseScore(int points) {
        score += points;
		GameObject.FindGameObjectWithTag("UIScore").GetComponent<TextMeshProUGUI>().text = score.ToString();
	}
}
