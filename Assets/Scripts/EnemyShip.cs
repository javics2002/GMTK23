using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    float currentTime;
    public float bulletSpeed;
    public float time=1f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if(currentTime > shootColdown)
        {
            bulletType.GetComponent<Bullet>().ChangeSpeed(bulletSpeed);
            Instantiate(bulletType, transform.GetChild(1).position, Quaternion.identity);
            currentTime= 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "LeftBorder" || collider.gameObject.tag == "RigthBorder")
        {

            //if (collider.gameObject.tag == "LeftBorder")
            //    GetComponentInParent<ShipsMovement>().goUp(ShipsMovement.borderTouched.left);
            //if (collider.gameObject.tag == "RigthBorder")
            //    GetComponentInParent<ShipsMovement>().goUp(ShipsMovement.borderTouched.right);

        }
    }

    public void left()
    {
        transform.Translate(Vector3.left);
    }

    public void rigth()
    {
        transform.Translate(Vector3.right);
    }

    public void up()
    {
        transform.Translate(Vector3.back);
    }
}
