using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShip : Ship
{
	float currentTime;
	Ship target;
    ShipsMovement shipsMovement;
    public float offset;
    public float epsilon = .5f;
    public GameObject points;

    AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        FindTarget();
        currentTime = 0;
        shipsMovement = FindObjectOfType<ShipsMovement>();

        shootSound = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GetInstance().playing)
            return;

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

        target = enemyShips[Random.Range(0, enemyShips.Length)].GetComponent<Ship>();
    }

    void Shoot() {
		if (currentTime > shootColdown) {
			bulletType.GetComponent<Bullet>().ChangeSpeed(-bulletSpeed);
			bulletType.GetComponent<Bullet>().isEnemyBullet = false;
			Instantiate(bulletType, transform.GetChild(1).position, Quaternion.identity).transform.localScale = Vector3.one;
			currentTime = 0;
            shootSound.Play();
			FindTarget();
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.yellow;

        if(target)
            Gizmos.DrawSphere(target.transform.position, 5);
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (GameManager.GetInstance().playing && collider.GetComponent<Bullet>() != null && collider.GetComponent<Bullet>().isEnemyBullet)
        {
			Instantiate(points, transform.position, Quaternion.identity)
				.GetComponentInChildren<TextMeshPro>().text = score.ToString();
			GameManager.GetInstance().setInvadersWin();
            GameManager.GetInstance().IncreaseScore(score);
            Destroy(gameObject);
        }
    }
}
