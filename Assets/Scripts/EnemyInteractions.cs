using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractions : MonoBehaviour
{
    public AudioSource landSound;
    public AudioSource spikeSound;
    public AudioSource enemySound;
    public AudioSource gameSound;
    public AudioSource gemSound;
    public AudioSource jumpSound;

    public BoxCollider2D collider2d;
    public Rigidbody2D rb;
    public Animator anim;

    public GameObject gameOverMenu;

    private void Update()
    {
        if (transform.position.y < -15 && !gameObject.GetComponent<CharacterMovement>().isDead)
        {
            StartCoroutine(deathSequence());
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            mute();
        }
    }

    public void mute()
    {
        if (jumpSound.mute)
        {
            landSound.mute = false;
            spikeSound.mute = false;
            enemySound.mute = false;
            gameSound.mute = false;
            gemSound.mute = false;
            jumpSound.mute = false;
        }
        else
        {
            landSound.mute = true;
            spikeSound.mute = true;
            enemySound.mute = true;
            gameSound.mute = true;
            gemSound.mute = true;
            jumpSound.mute = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            landSound.Play();
        }
        if (collision.gameObject.layer == 10) // spike
        {
            spikeSound.Play();
            StartCoroutine(deathSequence());
        } else if (collision.gameObject.layer == 9) // mushroom
        {
            if ((transform.position.y - collision.transform.position.y) >= 0.7f)
            {
                landSound.Play();
                StartCoroutine(destroyEnemy(collision.gameObject));
            } else
            {
                enemySound.Play();
                StartCoroutine(deathSequence());
            }
        } else if (collision.gameObject.CompareTag("Gem"))
        {
            gemSound.Play();
        }
    }

    IEnumerator destroyEnemy(GameObject enemy)
    {
        enemy.layer = 8;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Vector3 scaleChange = new Vector3(0, -0.8f, 0);
        enemy.GetComponent<Transform>().localScale += scaleChange;
        float posX = enemy.transform.position.x;
        float posY = enemy.transform.position.y - 0.35f;
        float posZ = enemy.transform.position.z;
        enemy.GetComponent<Transform>().position = new Vector3(posX, posY, posZ);
        float posX2 = transform.position.x;
        float posY2 = transform.position.y - 0.1f;
        float posZ2 = transform.position.z;
        transform.position = new Vector3(posX2, posY2, posZ2);

        yield return new WaitForSeconds(0.2f);
        Destroy(enemy);
    }

    IEnumerator deathSequence()
    {
        collider2d.enabled = false;
        rb.velocity = new Vector2(0, 8);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        anim.SetBool("isIdle", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isDead", true);
        gameObject.GetComponent<CharacterMovement>().isDead = true;
        transform.GetChild(2).gameObject.transform.parent = null;
        yield return new WaitForSeconds(1);
        gameOverMenu.SetActive(true);
    }
}
