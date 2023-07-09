using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    float currentTime;
	public float upSpeed = 5f;
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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GetComponentInParent<ShipsMovement>() != null 
            && !GetComponentInParent<ShipsMovement>().isAnyShipAttacking())
        {
            if (currentTime > shootColdown)
            {
                shoot();
                currentTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("LeftBorder") || collider.CompareTag("RigthBorder"))
        {
            if (collider.gameObject.tag == "LeftBorder")
                GetComponentInParent<ShipsMovement>().goUp(ShipsMovement.borderTouched.left);
            if (collider.gameObject.tag == "RigthBorder")
                GetComponentInParent<ShipsMovement>().goUp(ShipsMovement.borderTouched.right);
        }

        if (collider.GetComponent<Bullet>() != null && !collider.GetComponent<Bullet>().isEnemyBullet)
        {
            if (transform.parent == null)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
            }
            Destroy(gameObject);
        }
    }

    public void left()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void rigth()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void up()
    {
        transform.Translate(Vector3.back * upSpeed * Time.deltaTime);
    }

    public void shoot()
    {
        bulletType.GetComponent<Bullet>().ChangeSpeed(bulletSpeed);
        Instantiate(bulletType, transform.GetChild(1).position, transform.GetChild(1).rotation);
    }
}
