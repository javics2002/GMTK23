using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsGenerator : MonoBehaviour
{
    public GameObject bossShip;

    public GameObject [] enemiesShips;

    public GameObject playerShip;
    public GameObject shipsParent;

    public int colums;
    public int rows;

    public float spaceWidth;
    public float spaceHeight;

    public float separation;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 bossShipPos = new Vector3(spaceWidth / 2, 0, spaceHeight);
        Vector3 playerShipPos = new Vector3(spaceWidth / 2, 0, 0);

        Instantiate(bossShip, bossShipPos, Quaternion.identity);
        Instantiate(playerShip, playerShipPos, Quaternion.identity);

        Vector3 enemyShipPos;

        int[] patronEnemies = new int[5];
        patronEnemies[0] = 2;
        patronEnemies[1] = 1;
        patronEnemies[2] = 1;
        patronEnemies[3] = 0;
        patronEnemies[4] = 0;

        int total = 0;
        for (int i = 0; i < rows; i++){

            for(int j = 0; j < colums; j++)
            {
                enemyShipPos = new Vector3(5 + j*10, 0, spaceHeight - 10 - i*10);
                GameObject go = Instantiate(enemiesShips[patronEnemies[i]], enemyShipPos, Quaternion.identity, shipsParent.transform);
                go.GetComponent<EnemyShip>().time += total/10;
                total++;
            }
        }


    }

    public GameObject[] getShips()
    {
        return enemiesShips;
    }
}
