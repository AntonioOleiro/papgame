using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Player, ball and Movement Attributes")]
    private Rigidbody player;
    private GameObject ball;
    [SerializeField] float moveSpeed;
    float moveInput;

    [Header("Jump Properties")]
    [SerializeField] float jumpForce;
    [HideInInspector]
    public bool isGrounded, canShoot, canHead;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform pointToCast;

    [Header("Effect properties")]
    [SerializeField] ParticleSystem walkEffect;

  
     public bool isShooting, isHeading;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        player = GetComponent<Rigidbody>();
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    private void Update()
    {
        if (!GameManager.instance.gameActive) return;
        Jump();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            isShooting = true;
            StartCoroutine(AnimBoolShooting());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Stop();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Heading();
            isHeading = true;
            StartCoroutine(AnimBoolHeading());
        }

    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.gameActive) return;
        Movement();
        isGrounded = Physics.Raycast(pointToCast.position, Vector3.down, .2f, groundLayer);

        if (!isGrounded)
        {
            walkEffect.gameObject.SetActive(false);
        }
        else
        {
            walkEffect.gameObject.SetActive(true);
        }

    }
    void Movement()
    {
        float moveDirection;
        moveInput = Input.GetAxis("Horizontal");
        moveDirection = moveInput * moveSpeed* Time.deltaTime;
        player.velocity = new Vector3(moveDirection, player.velocity.y, player.velocity.z);
        
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            player.velocity = new Vector3(player.velocity.x, jumpForce);
        }
    }
    void Shoot()
    {
        if (canShoot)
        {
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(300, 250));
        }
    }
    void Stop()
    {
        if (canShoot)
        {
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(-0.2f,0.1f));
            Debug.Log(" Ball Stoping");
        }
    }
    void Heading()
    {
        if (canHead)
        {
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(300, 30));
        }
    }

    IEnumerator  AnimBoolHeading()
    {
        if (isHeading)
        {
           yield return new WaitForSeconds(.5f);
           isHeading = false;
        }

    }
    IEnumerator AnimBoolShooting()
    {
        if (isShooting)
        {
            yield return new WaitForSeconds(1f);
            isShooting = false;

        }

    }
}
