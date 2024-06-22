using UnityEngine;

public class AIControler : MonoBehaviour
{
    private enum AiState { NONE,SEARCHING, MOVING}
    public static AIControler instance;
    private AiState aiState;
    private Rigidbody aiRigidbody;
    GameObject ballObject;

    [SerializeField] string ballTag;
    [SerializeField] float MaxBallDist = 5f;
    [SerializeField] float aiMoveSpeed, aiJumpForce;
    [HideInInspector]
    public bool aiCanShoot, aiCanHeading;
    private GameObject ballTarget;

    [SerializeField] Transform pointToCast, defArea , AttArea;
    [HideInInspector]
    public bool isJump,CanJump;
    [SerializeField] LayerMask groundLayer;

    [Header("Effect properties")]
    [SerializeField] ParticleSystem walkEffect;

    private void Start()
    {
        instance = this;
        aiState = AiState.NONE;
        aiRigidbody = GetComponent<Rigidbody>();
        ballObject = GameObject.FindGameObjectWithTag("Ball");
        Debug.Log("state is none.");
    }
    private void Update()
    {
        if (GameManager.instance.gameActive)
        {
            aiState = AiState.SEARCHING;

        }
        else { return; }
        if(aiState == AiState.SEARCHING)
        {
            Search();
            Debug.Log("state is Searching.");
            if (ballTarget == null)
            {
                Debug.LogWarning("Time out...");
                return;
            }
        }
        if (isJump && CanJump)
        {
            AiJump();
        }
        if (aiCanShoot)
        {
            Shooting();
        }
        if (aiCanHeading)
        {
            Heading();
        }
        
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.gameActive) return;
        if (aiState == AiState.MOVING)
        {
            isJump = Physics.Raycast(pointToCast.position, Vector3.down, 0.2f, groundLayer);
            Moving();

            walkEffect.gameObject.SetActive(true);
        }
        if (!isJump)
        {
            walkEffect.gameObject.SetActive(false);
        }
    }

    GameObject Search()
    {
        ballTarget = GameObject.FindGameObjectWithTag(ballTag);
        if(ballTarget != null)
        {
            aiState = AiState.MOVING;
        }
        else
        {
            Debug.Log("Code Stop Here.");
        }
        return ballTarget;
    }

    void Moving()
    {
        if (ballTarget == null) return;
        if(Mathf.Abs(ballTarget.transform.position.x - transform.position.x) < MaxBallDist 
            && AttArea.position.x < transform.position.x)
        {
            if(ballTarget.transform.position.x < transform.position.x)
            {
                aiRigidbody.velocity = new Vector3( -aiMoveSpeed * Time.deltaTime, aiRigidbody.velocity.y);
            }
            else
            {
                aiRigidbody.velocity = new Vector3(aiMoveSpeed * Time.deltaTime, aiRigidbody.velocity.y);
            }
        }
        else
        {
            if (transform.position.x < defArea.position.x)
            {
                aiRigidbody.velocity =  new Vector3(aiMoveSpeed * Time.deltaTime, aiRigidbody.velocity.y);
            }
        }
    }
    void Shooting()
    {
        Debug.Log("Ai shooting");
        ballObject.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 6));
    }
    void Heading()
    {
        Debug.Log("Ai Heading");
        ballObject.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 2));
    }
    void AiJump()
    {
        aiRigidbody.velocity = new Vector3(aiRigidbody.velocity.x, aiJumpForce);
    }
}
