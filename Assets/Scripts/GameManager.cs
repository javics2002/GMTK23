using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        invadersLose= false;
        invadersWin= false;
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
    }

    public void setInvadersLose()
    {
        invadersLose = true;
    }
}
