using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

public class ShipsMovement : MonoBehaviour {

	public direction dir;
	public float upTime = .5f;
	bool goingUp = false;

	Vector3 shipAttacking;
	bool anyShipAttacking;

	public enum direction {
		left,
		right
	}

	public enum borderTouched {
		left, right
	}

	// Start is called before the first frame update
	void Start() {
		dir = direction.right;
		anyShipAttacking= false;
	}

	void Update() {
		Movement();
	}

	void Movement() {
		for (int i = 0; i < transform.childCount; i++) {
			for (int j = 0; j < transform.GetChild(i).childCount; j++) {
				if (goingUp) {
					transform.GetChild(i).GetChild(j).GetComponent<EnemyShip>().up();
				}

				else if (dir == direction.left) {
					transform.GetChild(i).GetChild(j).GetComponent<EnemyShip>().left();
				}

				else {
					transform.GetChild(i).GetChild(j).GetComponent<EnemyShip>().rigth();
				}
			}
		}

		if (anyShipAttacking)
		{
            if (goingUp)
            {
				shipAttacking += (Vector3.back * 5 * Time.deltaTime);
            }

            else if (dir == direction.left)
            {
                shipAttacking += (Vector3.left * 2 * Time.deltaTime);
            }

            else
            {
                shipAttacking += (Vector3.right * 2 * Time.deltaTime);
            }
        }
	}

	public void goUp(borderTouched border) {
		switch (border) {
			case borderTouched.left:
				dir = direction.right;
				break;
			case borderTouched.right:
				dir = direction.left;
				break;
		}

		goingUp = true;
		Invoke("StopGoingUp", upTime);
	}

	void StopGoingUp() {
		goingUp = false;
	}

	public void saveTransformOfShipAttacking(Vector3 tr)
	{
		anyShipAttacking= true;
		shipAttacking = tr;
	}

	public Vector3 getSavedTransformOfShipAttacking()
	{
		return shipAttacking;
	}
}
