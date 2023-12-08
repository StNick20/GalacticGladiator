using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public GameObject[] powerUps;

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            powerUpDrop();
        }
    }

    public void Damage()
    {
        health -= 10;
    }

    void powerUpDrop()
    {
        int chance = Random.Range(1, 100);

        if(chance >= 0 && chance <= 20)
        {
            Instantiate(powerUps[0], transform.position, Quaternion.identity);
        }
        else if (chance >= 21 && chance <= 40)
        {
            Instantiate(powerUps[1], transform.position, Quaternion.identity);
        }
        else if (chance >= 41 && chance <= 50)
        {
            Instantiate(powerUps[2], transform.position, Quaternion.identity);
        }
    }
}
