using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] string Pfoot;
    [SerializeField] string PHead;
    [SerializeField] string Aifoot;
    [SerializeField] string AiHead;
    [SerializeField] string AiJumpCheckTag;
    [SerializeField] string leftPostTag, rightPostTag;
    private Rigidbody ballRb;

    private void Start()
    {
        int ChooseSide = Random.Range(0, 2);
        ballRb = GetComponent<Rigidbody>();
        if(ChooseSide == 0)
        {
            ballRb.AddForce(new Vector3(-100, 5));
        }
        if (ChooseSide == 1)
        {
            ballRb.AddForce(new Vector3(100, 5));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.gameActive) return;
        if (other.CompareTag(Pfoot))
        {
            Player.instance.canShoot = true;
        }
        if (other.CompareTag(PHead))
        {
            Player.instance.canHead = true;
        }

        if (other.CompareTag(Aifoot))
        {
            AIControler.instance.aiCanShoot = true;
        }
        if (other.CompareTag(AiJumpCheckTag))
        {
            AIControler.instance.CanJump = true;
        }
        if (other.CompareTag(AiHead))
        {
            AIControler.instance.aiCanHeading = true;
        }
        if (other.CompareTag(rightPostTag) && !GameManager.instance.isScore)
        {
            GameManager.instance.ScorePlayer++;
            GameManager.instance.isScore = true;
            GameManager.instance.ContinueGame(true);
            StartCoroutine(GameManager.instance.PlayScoreEffect());
        }
        if (other.CompareTag(leftPostTag) && !GameManager.instance.isScore)
        {
            GameManager.instance.ScoreAI++;
            GameManager.instance.isScore = true;
            GameManager.instance.ContinueGame(false);
            StartCoroutine(GameManager.instance.PlayScoreEffect());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!GameManager.instance.gameActive) return;
        if (other.CompareTag(Pfoot))
        {
            Player.instance.canShoot = false;
        }
        if (other.CompareTag(PHead))
        {
            Player.instance.canHead = false;
        }

        if (other.CompareTag(Aifoot))
        {
            AIControler.instance.aiCanShoot = false;
        }
        if (other.CompareTag(AiJumpCheckTag))
        {
            AIControler.instance.CanJump = false;
        }
        if (other.CompareTag(AiHead))
        {
            AIControler.instance.aiCanHeading = false;
        }
    }
}
