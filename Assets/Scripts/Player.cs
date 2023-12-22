using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Animator playerAnim;

    [SerializeField] private GameObject panelObject;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Material normalMat;
    [SerializeField] private Material damageMat;

    [SerializeField] private int playerHealth = 5;
    [SerializeField] private int jumpForce = 5;
    [SerializeField] private int playerSpeed = 2;
    [SerializeField] private int hitForce = 5;
    [SerializeField] private bool canWin;

    private float dir;
    private bool isFalling;
    private bool isWalking;
    private bool isJumping;
    private bool canGetHit;
    private bool canMove;

    void Start() 
    {
        canMove = true;
        canGetHit = true;
        canWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !GameManager.Instance.isGamePaused && GameManager.Instance.canPlay) 
        {
            Walk();
            Jump();
        }
        CheckFalling();
    }

    void Walk() 
    {
        dir = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(dir * playerSpeed, playerRb.velocity.y);
        if (dir != 0 && !isWalking) 
        {
            isWalking = true;
            playerAnim.SetBool("IsWalking", true);
        }
        if (dir == 0 && isWalking)
        {
            isWalking = false;
            playerAnim.SetBool("IsWalking", false);
        }
        if (dir == 1) GetComponent<SpriteRenderer>().flipX = false;
        if (dir == -1) GetComponent<SpriteRenderer>().flipX =  true;

        if (!groundCheck.IsGrounded) playerRb.velocity = new Vector2(dir * playerSpeed, playerRb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck.IsGrounded)
        {
            isJumping = true;
            playerRb.velocity = Vector2.up * jumpForce;
            playerAnim.SetBool("IsJumping", true);
            FindObjectOfType<AudioManager>().Play("Jump");
        }
    }

    void CheckFalling() 
    {
        if (playerRb.velocity.y < 0.1 && !isFalling) 
        {
            if (isJumping)
            {
                isJumping = false;
                playerAnim.SetBool("IsJumping", false);
            }
            isFalling = true;
            playerAnim.SetBool("IsFalling", true);
        }
        if (isFalling && groundCheck.IsGrounded) 
        {
            isFalling = false;
            playerAnim.SetBool("IsFalling", false);
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Spike")) 
        {
            if (canGetHit)
            {
                playerHealth--;
                GameManager.Instance.UpdateHealth(playerHealth);
                FindObjectOfType<AudioManager>().Play("Hit");
                StartCoroutine(HitPlayer(other.gameObject));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("House"))
        {
            if (CanWin())
            {
                GameManager.Instance.EndGame();
            }
            else
            {
                panelObject.SetActive(true);
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            panelObject.SetActive(false);
        }
    }

    IEnumerator HitPlayer(GameObject spikeObject) 
    {
        canMove = false;
        canGetHit = false;
        Vector2 direction = (gameObject.transform.position - spikeObject.transform.position).normalized;
        playerRb.velocity = direction * hitForce;
        StartCoroutine("DamageRoutine");
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(0.2f);
        canMove = true;
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        canGetHit = true;
    }

    IEnumerator DamageRoutine() 
    {
        spriteRenderer.material = damageMat;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = normalMat;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = damageMat;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = normalMat;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.7f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public int GetHealth()
    {
        return playerHealth;
    }

    public bool IsDead()
    {
        return playerHealth <= 0;
    }

    public bool CanWin()
    {
        return canWin;
    }

    public void SetWin(bool winCondition)
    {
        canWin = winCondition;
    }
}
