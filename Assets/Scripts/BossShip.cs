using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip : Ship
{
    AudioSource gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        gameOverSound= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Bullet>() != null && !collider.GetComponent<Bullet>().isEnemyBullet)
        {
            GameManager.GetInstance().setInvadersLose();
            gameOverSound.Play();
            Destroy(gameObject);
        }
    }
}
