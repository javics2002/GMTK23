using UnityEngine;
using Dan.Main;
using System.Security.Cryptography.X509Certificates;
using System;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public GameObject entry;

    string publicLeaderboardKey = "4f94383e7156f195db0b86d45e9b185084cbc46e46f58ca440c04a73c3093007";

	// Start is called before the first frame update
	void Start()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, (msg) => {
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50 * msg.Length);

            for (int i = 0; i < msg.Length; i++) {
                GameObject newEntry = Instantiate(entry, transform);

                newEntry.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = msg[i].Username;
                newEntry.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = msg[i].Score.ToString();
            }
        });

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
