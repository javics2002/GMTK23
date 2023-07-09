using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedBullet;
    public bool isEnemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speedBullet * Time.deltaTime);
    }

    public void ChangeSpeed(float speed)
    {
        speedBullet = speed;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if((isEnemyBullet && collider.gameObject.GetComponent<PlayerShip>() != null) || 
           (!isEnemyBullet && collider.gameObject.GetComponent<EnemyShip>() != null) ||
           collider.gameObject.CompareTag("DeadZone"))
            Destroy(gameObject);
    }
}
