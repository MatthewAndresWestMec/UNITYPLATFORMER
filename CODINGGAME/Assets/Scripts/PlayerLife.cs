using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!IsPlayerOnTerrain())
            {
                // Player is not on terrain, so the enemy dies
                Destroy(collision.gameObject);
                // You may want to add sound effect or other effects here
            }
            else
            {
                // Player is on terrain, so the player dies
                Die();
            }
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool IsPlayerOnTerrain()
    {
        // Check if the player is colliding with any object tagged as "Terrain"
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Terrain"))
            {
                return true;
            }
        }
        return false;
    }
}
