using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    Rigidbody2D rb;
    GameObject GameManager;
    public float speed = 10f, speedDecay = 10f;
    private AudioSource sound, sound2;
    GameObject music;
    private Game gameScript;
    public bool mute = false;
    public int health = 3;
    float invuln = 0f;
    float cooldownTimer = 0;
    Animator animator;
    public float jumpForce = 10f;
    bool isGrounded;
    float distToGround;

    public Weapon CurrentWeapon;

    [System.Serializable]
    public class Weapon
    {
        public GameObject bullet;
        public GameObject bullet2;
        public float fireDelay;

        public virtual void make()
        {
            bullet = Instantiate<GameObject>(bullet);
            fireDelay = 0.25f;
        }

    }
    public class Upgrade: Weapon
    {
        public override void make()
        {
            bullet = Instantiate<GameObject>(bullet2);
            fireDelay = 0.2f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
        sound.volume=0.1f;
        music = GameObject.Find("BGAudio");
        GameManager = GameObject.Find("GameManager");
        sound2 = music.GetComponent<AudioSource>();
        gameScript = this.GetComponent<Game>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    bool Grounded()
    {
        distToGround = GetComponent<CircleCollider2D>().bounds.extents.y;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, distToGround + 0.02f);
        if (hit.collider != null)
        {
            return true;
        }
        else { return false; }
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Grounded())
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("Grounded", false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            Debug.Log("left");
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            Debug.Log("right");
            this.GetComponent<SpriteRenderer>().flipX = false;

        }
        //Debug.Log("invuln = " + invuln);
        invuln -= Time.deltaTime;
        cooldownTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded){
                rb.velocity = Vector2.up * jumpForce;
            }
            
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1f) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2.0f - 1f) * Time.deltaTime;
        }
        float value = Input.GetAxis("Vertical");
        float move = Input.GetAxis("Horizontal");
        float faster = Input.GetAxis("Fire3");
        if (faster != 0) value = value * 1.5f;
        
        if (move != 0)
        {
            Vector2 position = this.transform.position;
            position.x += (move/20f);
            this.transform.position = position;
            animator.SetBool("Running", true);

        }
        else
        {
            animator.SetBool("Running", false);
        }
            

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Landed");
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
           // transform.SetParent( collision.transform,false);

        }
        if (collision.gameObject.tag == "Enemy")
        {
            hit();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Jumping");
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            animator.SetBool("Grounded", false);
            //transform.SetParent(null);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Player touched");
        if (coll.gameObject.tag == "Enemy")
        {
            hit();          
        }
    }
    public void hit()
    {
        if (invuln <= 0)
        {
            health--;
            if (health > 0)
            {
                GameManager.GetComponent<GameManager>().startRespawn();
                invuln = 2f;
            }
            else
            {
                GameManager.GetComponent<GameManager>().startDie();
                invuln = 2f;
            }

            GameManager.GetComponent<GameManager>().LifeUpdate();

        }

        if (!mute)
        {
            sound.PlayOneShot(sound.clip,0.1f);
        }
    }
    public IEnumerator die()
    {
        Debug.Log("dying");
        Destroy(gameObject);
        yield return new WaitForSeconds(2f);

    }
    public IEnumerator respawn()
    {
        Debug.Log("respawning");

        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        Debug.Log("respawning2");
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(-7.62f, 0, 0);
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

    }
    public void fire()
    {
        if (cooldownTimer <= 0)
        {
            //Upgrade if survived for 20 seconds
            if (Time.time > 5f)
            {
                CurrentWeapon = new Upgrade();
            }
            cooldownTimer = CurrentWeapon.fireDelay;

            CurrentWeapon.make();
            CurrentWeapon.bullet.transform.position = transform.position;
            CurrentWeapon.bullet.transform.rotation = transform.rotation;
            //float x = shot.transform.rotation.eulerAngles;
            CurrentWeapon.bullet.transform.rotation *= Quaternion.Euler(0, 0, -90f);
            GameObject shot = CurrentWeapon.bullet;
        }
    }
    public void muteSound()
    {
        if (!mute) {
            mute = true;
            sound2.mute = true;
        }
        else {
            mute = false;
            sound2.mute = false;

        };
    }
}
