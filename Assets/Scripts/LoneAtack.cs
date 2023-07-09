using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoneAtack : MonoBehaviour
{

    float timeToUnlock;
    float currentT;
    float timeInAtackMode;
    bool charge;
    bool atackMode;

    Transform originalParent;

    public Camera camera;
    public float maxAtackModeTime;

    Material material;

    // Start is called before the first frame update
    void Start()
    {
        timeToUnlock = Random.Range(5.0f, 11.0f);
        charge = false;
        atackMode = false;
        currentT = 0;
        timeInAtackMode = 0;
        originalParent = transform.parent;
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

        if (atackMode)
        {
            timeInAtackMode+= Time.deltaTime;

            if(timeInAtackMode >= maxAtackModeTime)
            {
                exitAtackMode();
            }

        }

        if (!charge)
        {
            currentT += Time.deltaTime;

            if (currentT >= timeToUnlock)
            {
                charge = true;
                currentT = 0;
            }
        }

        material.SetInt("_Charged", charge ? 1 : 0);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && charge && !atackMode)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
            GetComponentInParent<ShipsMovement>().saveTransformOfShipAttacking(transform.position);
            atackMode= true;
            camera.gameObject.SetActive(true);
            transform.parent = null;
        }
    }

    public void exitAtackMode()
    {
        atackMode= false;
        charge= false; 
        timeInAtackMode = 0;
        camera.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        transform.parent = originalParent;
        transform.position = GetComponentInParent<ShipsMovement>().getSavedTransformOfShipAttacking();
    }

}
