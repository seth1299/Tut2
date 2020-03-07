using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    public Text winText;

    private int lives;

    public Text livesText;

    private bool isOnGround;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    Animator anim;

    private bool facingRight = true;

    private int animState;

    private Rigidbody rb;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musicClipOne;

        musicSource.loop = false;

        musicSource.Stop();

        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();

        rb2d = GetComponent<Rigidbody2D>();

        lives = 3;

        score.text = "Score: " + scoreValue.ToString();

        winText.text = "";

        livesText.text = "Lives: " + lives.ToString();

        animState = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rb2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if ( isOnGround )
            anim.ResetTrigger("Jump");

        if ( isOnGround && hozMovement == 0 )
        {
            animState = 0;
            anim.SetInteger("State", animState);
        }

        if ( isOnGround && hozMovement != 0 )
        {
            animState = 1;
            anim.SetInteger("State", animState);
        }

        if ( !isOnGround )
        {
            animState = 2;
            anim.SetInteger("State", animState);
        }

        if ( facingRight == false && hozMovement > 0 )
        {
            Flip();
        }
        else if ( facingRight == true && hozMovement < 0 )
        {
            Flip();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector2(50.0f, 0);
                lives = 3;
                livesText.text = "Lives: " + lives.ToString();
            }

            if (scoreValue == 8)
            {
                winText.text = "You win! Game created by Seth Grimes!";
                anim.SetInteger("State", 0);
                musicSource.Play();
                Destroy(this);
            }
        
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            if (lives == 0)
            {
                winText.text = "You lose! Game created by Seth Grimes!";
                anim.SetInteger("State", 0);
                Destroy(this);
            }

        }

        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

}