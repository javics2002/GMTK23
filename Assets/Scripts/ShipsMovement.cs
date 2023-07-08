using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShipsMovement : MonoBehaviour
{
    direction dir;
    
    public enum direction
    {
        left,
        right
    }

    public enum borderTouched
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Movement", 1f, 1f);
        dir = direction.right;
    }

    void Movement()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                if (dir == direction.left)
                {
                    transform.GetChild(i).GetChild(j).GetComponent<EnemyShip>().left();
                }
                else transform.GetChild(i).GetChild(j).GetComponent<EnemyShip>().rigth();
            }
        }
    }

    public void goUp(borderTouched border)
    {
        switch (border)
        {
            case borderTouched.left:
                dir = direction.right;
                break;
            case borderTouched.right:
                dir = direction.left;
                break;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<EnemyShip>().up();
        }
    }
}
