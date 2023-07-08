using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    float currentTime;

    public float bulletSpeed;

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
}
