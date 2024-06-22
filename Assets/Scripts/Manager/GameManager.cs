using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]
    public bool gameActive, isScore;
    [HideInInspector]
    public int ScorePlayer, ScoreAI, fixtureIndex;
    // [HideInInspector]
    public float matchTime = 90;

    [Header(" Ui Reference...")]
    [SerializeField] GameObject gameOverScreen;

    [Header(" gameScene iteams Reference...")]
    [SerializeField] Transform ballResponPoint;
    [SerializeField] Transform playerResponPoint;
    [SerializeField] Transform AiResponPoint;
    GameObject ballGO, playerGO, AiGO;

    [Space(15)]
    [SerializeField] GameObject scoreEffect;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameActive = true;
        StartCoroutine(StartMatch());

        ballGO = GameObject.FindGameObjectWithTag("Ball");
        playerGO = GameObject.FindGameObjectWithTag("Player");
        AiGO = GameObject.FindGameObjectWithTag("AI");
    }

    private void LateUpdate()
    {
        if (!gameActive)
        {
            ShowGameOverScreen(gameOverScreen);
        }
        else
        {
            HideGameOverScreen(gameOverScreen);
        }
    }

    IEnumerator StartMatch()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(matchTime > 0)
            {
                matchTime--;
            }
            else
            {
                gameActive = false;
                break;
            }
        }
    }
    
    public void ContinueGame(bool playerScore)
    {
        StartCoroutine(ReResponGame(playerScore));
    }

    IEnumerator ReResponGame(bool playerScore)
    {
        yield return new WaitForSeconds(2f);
        isScore = false;
        if(gameActive)
        {
            ballGO.transform.position = ballResponPoint.position;
            ballGO.GetComponent<Rigidbody>().velocity = new Vector3(0, 0);

            playerGO.transform.position = playerResponPoint.position;
            AiGO.transform.position = AiResponPoint.position;
            if (playerScore)
            {
                ballGO.GetComponent<Rigidbody>().AddForce(new Vector3(100, 5));
            }
            else
            {
                ballGO.GetComponent<Rigidbody>().AddForce(new Vector3(-100, 5));
            }
        }
    }

    public IEnumerator PlayScoreEffect()
    {
        scoreEffect.SetActive(true);
        yield return new WaitForSeconds(3f);
        scoreEffect.SetActive(false);
    }

    public void GameOverContinue()
    {
        SceneManager.LoadScene(0);
        fixtureIndex = Fixture.instance.currentFixtureIndex++;
        PlayerPrefs.SetInt("CurrentIndex", fixtureIndex);
    }
    void ShowGameOverScreen(GameObject Ui)
    {
        Ui.SetActive(true);
    }
    void HideGameOverScreen(GameObject Ui)
    {
        Ui.SetActive(false);
    }
}
