using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShip : Ship
{
	float currentTime;
	EnemyShip target;
    ShipsMovement shipsMovement;
    public float offset;
    public float epsilon = .5f;

	// Start is called before the first frame update
	void Start()
    {
        FindTarget();
        currentTime = 0;
        shipsMovement = FindObjectOfType<ShipsMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!target)
        {
            FindTarget();
        }

        if (transform.position.x - epsilon > target.transform.position.x +
            (shipsMovement.dir == ShipsMovement.direction.right ? target.speed : -target.speed) * offset
			* Mathf.Abs(transform.position.z - target.transform.position.z))
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        else if (transform.position.x + epsilon < target.transform.position.x +
            (shipsMovement.dir == ShipsMovement.direction.right ? target.speed : -target.speed) * offset
            * Mathf.Abs(transform.position.z - target.transform.position.z))
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else
            Shoot();

        currentTime += Time.deltaTime;
	}

    void FindTarget() {
        var enemyShips = GameObject.FindGameObjectsWithTag("Enemy");

        target = enemyShips[Random.Range(0, enemyShips.Length)].GetComponent<EnemyShip>();
    }

    void Shoot() {
		if (currentTime > shootColdown) {
			bulletType.GetComponent<Bullet>().ChangeSpeed(-bulletSpeed);
			bulletType.GetComponent<Bullet>().isEnemyBullet = false;
			Instantiate(bulletType, transform.GetChild(1).position, Quaternion.identity);
			currentTime = 0;
			FindTarget();
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.yellow;

        if(target)
            Gizmos.DrawSphere(target.transform.position, 5);
	}
}
