using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Text playerScoreTxt;
    [SerializeField] Text AiScoreTxt;
    [SerializeField] Text TimeTxt;

    [Header("gameover score")]
    [SerializeField] Text playerScoreGOTxt;
    [SerializeField] Text AiScoreGOTxt;

    private void LateUpdate()
    {
        playerScoreTxt.text = GameManager.instance.ScorePlayer.ToString();
        AiScoreTxt.text = GameManager.instance.ScoreAI.ToString();
        TimeTxt.text = "Time: " + GameManager.instance.matchTime;

        playerScoreGOTxt.text = GameManager.instance.ScorePlayer.ToString();
        AiScoreGOTxt.text = GameManager.instance.ScoreAI.ToString();
    }

}
