using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float climbSpeed;
    public float jumpForce; 
    private Rigidbody2D rb;
    public GameObject particles; 
    [SerializeField]
    private GameObject nextLevel;

    [SerializeField]
    private GameObject black;

    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject[] restart;

    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private AudioSource audioSource; 
    private int regainJumpTimePassed; 
    private int regainJump; 
    bool canClimb;
    bool canJump; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canClimb = false;
        canJump = true;
        nextLevel.SetActive(false);
        black.SetActive(false);
        playButton.SetActive(false);
        regainJump = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

      
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, 0f);
        Debug.Log(regainJump);
        if (vertical != 0 && canClimb == true)
            rb.velocity = new Vector3(rb.velocity.x, vertical * climbSpeed, 0f);

        if (canJump == true && Input.GetKeyDown(KeyCode.Space))
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);

        if (canJump == false && Input.GetKeyDown(KeyCode.Space) && regainJump > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
                regainJump -= 1; 
            }

        if (Input.GetKey(KeyCode.Tab))
        {
            black.SetActive(true);
            playButton.SetActive(true);
            restart[1].SetActive(true); 
            Time.timeScale = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            canClimb = true;
            canJump = true; 
        }
          
        if (collision.gameObject.CompareTag("Ground"))
            canJump = true; 

       

        if (collision.gameObject.CompareTag("FlatWall"))
            canJump = false;

        if (collision.gameObject.CompareTag("Spikes"))
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
            gameObject.GetComponent<SpriteRenderer>().sprite = null; 
            restart[0].SetActive(true);
            Time.timeScale = 0; 
        }

        if (collision.gameObject.CompareTag("BlueGear"))
            canJump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            canClimb = false;
            canJump = false; 
        }
           
        if (collision.gameObject.CompareTag("Ground"))
            canJump = false;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RegainJump"))
        {
            canJump = false;
            ParticleSystem ps = Instantiate(particles.GetComponent<ParticleSystem>(), collision.transform.position, Quaternion.identity);
            ps.Play();
            Destroy(collision.gameObject);
            audioSource.clip = audioClips[2];
            audioSource.Play(); 
            regainJump += 1;
        }
        if (collision.gameObject.CompareTag("GoldenGear"))
        {
            Destroy(collision.gameObject);
            nextLevel.SetActive(true);
            audioSource.clip = audioClips[0];
            audioSource.Play();
            Time.timeScale = 0;
        }
          
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
            restart[0].SetActive(true);
            Time.timeScale = 0; 
        }
    }
}
