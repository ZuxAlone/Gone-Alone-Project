using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Player player;

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
    }

    void CheckDeath()
    {
        if (player.IsDead())
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Dead Zone"))
        {
            Die();
            GameManager.Instance.UpdateHealth(0);
        }
    }

    private void Die() 
    {
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("Death");
        GameManager.Instance.RespawnPlayer();
    }
}
