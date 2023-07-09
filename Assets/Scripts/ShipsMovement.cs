using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

public class ShipsMovement : MonoBehaviour {

	public direction dir;
	public float upTime = .5f;
	bool goingUp = false;

	Vector3 shipAttackingPos;
	Quaternion shipAttackingRot;
	bool anyShipAttacking;

	float shipAttackingUpSpeed = 5;
	float shipAttackingRLSpeed = 2;

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
				shipAttackingPos += (Vector3.back * shipAttackingUpSpeed * Time.deltaTime);
            }

            else if (dir == direction.left)
            {
                shipAttackingPos += (Vector3.left * shipAttackingRLSpeed * Time.deltaTime);
            }

            else
            {
                shipAttackingPos += (Vector3.right * shipAttackingRLSpeed * Time.deltaTime);
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

	public void saveTransformOfShipAttacking(Vector3 tr, Quaternion rot)
	{
		shipAttackingPos = tr;
		shipAttackingRot= rot;
	}

	public Vector3 getSavedPosOfShipAttacking()
	{
		return shipAttackingPos;
	}

	public Quaternion getSavedRotOfShipAttacking()
	{
		return shipAttackingRot;
	}

	public void shipEnterInAtackMode()
	{
        anyShipAttacking = true;
    }

    public void shipExitAtackMode()
    {
        anyShipAttacking = false;
    }

    public bool isAnyShipAttacking()
	{
		return anyShipAttacking;
	}
}
