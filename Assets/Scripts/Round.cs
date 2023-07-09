using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class Round : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = GameManager.GetInstance().round.ToString();
    }
}
