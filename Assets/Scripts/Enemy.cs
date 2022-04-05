using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerKill = 10;
    [SerializeField] int scorePerHit = 4;

    [SerializeField]int enemyHp = 6;

    Scoreboard scoreboard;

    void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(enemyHp < 1)
        {
            KillEnemy();
        }
   
    }

    void ProcessHit()
    {
        enemyHp--;
        scoreboard.IncreaseScore(scorePerHit);
        GameObject hit = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hit.transform.parent = parent;
    }
    void KillEnemy()
    {
        scoreboard.IncreaseScore(scorePerKill);
        Destroy(this.gameObject);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
    }

}
