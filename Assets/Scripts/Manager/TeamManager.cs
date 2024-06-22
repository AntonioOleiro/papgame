using UnityEngine;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    public static TeamManager insatance;
    public string playerTeam;
    public string awayTeam;

    public Text playerTeamTxt;
    public Text aiTeamTxt;

    public Text playerTeamGOTxt;
    public Text aiTeamGOTxt;

    private void Awake()
    {
        insatance = this;
    }
    private void Start()
    {
        Fixture.instance.GetPlayerFixture();
        DisplayName();
        
    }

    private void Update()
    {
        if (!GameManager.instance.gameActive)
        {
            playerTeamGOTxt.text = playerTeam.ToString();
            aiTeamGOTxt.text = awayTeam.ToString();
        }
    }
    void DisplayName()
    {
        playerTeamTxt.text = playerTeam.ToString();
        aiTeamTxt.text = awayTeam.ToString();
    }
}
