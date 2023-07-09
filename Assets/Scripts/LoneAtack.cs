using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoneAtack : MonoBehaviour
{

    float timeToUnlock;
    float watingUnlock;
    float waitingShoot;
    float timeInAtackMode;
    bool charge;
    bool atackMode;
    bool raise;
    bool push;

    Transform originalParent;

    public Camera camera;
    public float maxAtackModeTime;
    public float cameraSpeed;
    public float shootColdown;
    public float raiseSpeed;
    public float raiseTime;
    public float speed;
    public float timeToResetShip;

    Material material;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        timeToUnlock = Random.Range(5.0f, 11.0f);
        originalParent = transform.parent;

        charge = false;
        atackMode = false;
        push = false;
        raise = false;
        
        watingUnlock = 0;
        waitingShoot = 0;
        timeInAtackMode = 0;

        material = GetComponentInChildren<MeshRenderer>().material;
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (atackMode)
        {
            timeInAtackMode+= Time.deltaTime;
            waitingShoot+= Time.deltaTime;

            if(timeInAtackMode >= maxAtackModeTime || !GameManager.GetInstance().playing)
            {
                timeInAtackMode = 0;
                exitAtackMode();
            }

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, mouseX * cameraSpeed * Time.deltaTime);
            transform.Rotate(Vector3.right, mouseY * cameraSpeed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                if (waitingShoot > shootColdown)
                {
                    GetComponent<EnemyShip>().shoot();
                    waitingShoot = 0;
                }
            }
            
            if(raise) transform.Translate(Vector3.up * raiseSpeed * Time.deltaTime);
            if(push) transform.Translate(Vector3.back * speed * Time.deltaTime);

        }

        if (!charge)
        {
            watingUnlock += Time.deltaTime;

            if (watingUnlock >= timeToUnlock)
            {
                charge = true;
                watingUnlock = 0;
            }
        }

        material.SetInt("_Charged", charge ? 1 : 0);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && charge && !atackMode && GetComponentInParent<ShipsMovement>() != null
            && !GetComponentInParent<ShipsMovement>().isAnyShipAttacking())
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
            GetComponentInParent<ShipsMovement>().saveTransformOfShipAttacking(transform.position, transform.rotation);
            GetComponentInParent<ShipsMovement>().shipEnterInAtackMode();

            atackMode = true;
            raise = true;

            Invoke("StopRaise", raiseTime);

            camera.gameObject.SetActive(true);
            transform.parent = null;
        }
    }

    public void exitAtackMode()
    {
        atackMode= false;
        charge= false;
        push = false;

        camera.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;

        animator.SetBool("visible", false);

        //gameObject.SetActive(false);

        Invoke("ResetShipInArmy", timeToResetShip);
    }

    void ResetShipInArmy()
    {
        //gameObject.SetActive(true);
        animator.SetBool("visible", true);

        transform.parent = originalParent;
        GetComponentInParent<ShipsMovement>().shipExitAtackMode();
        transform.position = GetComponentInParent<ShipsMovement>().getSavedPosOfShipAttacking();
        transform.rotation = GetComponentInParent<ShipsMovement>().getSavedRotOfShipAttacking();
    }

    void StopRaise()
    {
        raise = false;
        push = true;
    }

}
