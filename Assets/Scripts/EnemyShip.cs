using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    float currentTime;
    GameObject ships;
    public float bulletSpeed;
    bool direction;
    public float time=1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Movement", 1f, 1f);
        currentTime = 0;
        ships = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }


    void Movement()
    {
        if (direction)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        else transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collsiiones");
        if (collision.gameObject.tag == "LeftBorder" || collision.gameObject.tag == "RigthBorder")
        {
            for (int i = 0; i < ships.transform.childCount; i++)
            {
                ships.transform.GetChild(i).transform.position = new Vector3(ships.transform.GetChild(i).transform.position.x,
                    ships.transform.GetChild(i).transform.position.y, ships.transform.GetChild(i).transform.position.z-0.5f);
                if(collision.gameObject.tag == "LeftBorder") 
                    ships.GetComponent<ShipsMovement>().rigth();
                if (collision.gameObject.tag == "RigthBorder")
                    ships.GetComponent<ShipsMovement>().left();
            }
        }
    }

    public void left()
    {
        direction = false;
    }

    public void rigth()
    {
        direction = true;
    }
}
