using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public GameObject winTextObject;
    public TextMeshProUGUI winText;
    Animator anim;
    public Text lives;
    private int lifeValue = 3;
    public AudioSource musicSource;
    public AudioClip musicClip;
    public AudioClip winClip;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = lifeValue.ToString();
        winTextObject.SetActive(false);
        anim = GetComponent<Animator>();
        musicSource.clip = musicClip;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -0.5f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 0.5f;
        }
        transform.localScale = characterScale;

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
        {
            transform.position = new Vector3(50.0f, 0.5f, 0.0f);
            lifeValue = 3;
            lives.text = lifeValue.ToString();
        }
        if (scoreValue == 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = winClip;
            musicSource.Play();
            musicSource.loop = false;
        }
        }
        
        if(collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lifeValue = lifeValue - 1;
            lives.text = lifeValue.ToString();
            if(lifeValue == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
            if(Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("State", 2);
            }
            if(Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("State", 2);
            }
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
                anim.SetInteger("State", 1);
            }

        }
    }

    
}
