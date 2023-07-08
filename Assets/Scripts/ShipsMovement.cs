using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShipsMovement : MonoBehaviour
{
    public float movementMaxTime;
    private float timer;
    public GameObject[] ships;
    bool direction = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void left()
    {
        for(int i=0;i< transform.childCount;i++)
        {
            transform.GetChild(i).GetComponent<EnemyShip>().left();
        }
    }

    public void rigth()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<EnemyShip>().rigth();
        }
    }
}
