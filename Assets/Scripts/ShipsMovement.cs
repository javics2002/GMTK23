using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

public class ShipsMovement : MonoBehaviour {
	direction dir;
	public float upTime = .5f;
	bool goingUp = false;


	public enum direction {
		left,
		right
	}

	public enum borderTouched {
		left, right
	}

	// Start is called before the first frame update
	void Start() {
		InvokeRepeating("Movement", 1f, 1f);
		dir = direction.right;
	}

	private void Update() {
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
}
