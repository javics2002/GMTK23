using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoneAtack : MonoBehaviour
{

    float timeToUnlock;
    float currentT;
    bool charge;

    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        timeToUnlock = Random.Range(5, 11);
        charge = false;
        currentT = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!charge)
        {
            currentT += Time.deltaTime;

            if (currentT >= timeToUnlock)
            {
                charge = true;
                currentT = 0;
            }
        }

    }

    public bool isCharge()
    {
        return charge;
    }

    public void setCharge(bool charge_)
    {
        charge = charge_;
    }

    public void setCameraActive(bool active)
    {
        camera.gameObject.SetActive(active);
    }

}
