using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fixture : MonoBehaviour
{
    public static Fixture instance;
    private List<Team> teams = new List<Team>();
    private List<FixtureData> fixture = new List<FixtureData>();
    public int currentFixtureIndex = 0;

    [SerializeField] Transform fixtureTxt;
    [SerializeField] Transform fixtureContiner;

    private Team playerTeam;

    private void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
        fixtureTxt.gameObject.SetActive(false);
        InitTeam();
        fixture = GetFixture();
        Display();


        if (PlayerPrefs.HasKey("CurrentIndex"))
        {
            currentFixtureIndex = PlayerPrefs.GetInt("CurrentIndex", 0);
        }
        else
        {
            currentFixtureIndex = 0;
        }
    }

    void InitTeam()
    {
        Team newTeam = new Team(PlayerPrefs.GetString("TeamName"));
        teams.Add(newTeam);
        newTeam = new Team("benfica");
        teams.Add(newTeam);
        newTeam = new Team("sporting");
        teams.Add(newTeam);
        newTeam = new Team("eletrico");
        teams.Add(newTeam);
        newTeam = new Team("lombos");
        teams.Add(newTeam);
        newTeam = new Team("braga");
        teams.Add(newTeam);
    }
    public List<FixtureData> GetFixture()
    {
        List<FixtureData> newList = new List<FixtureData>();
        
        while(newList.Count < this.teams.Count / 2 * (teams.Count - 1))
        {
            //cria um novo jg
            for (int i = 0; i < teams.Count; i += 2)
            {
                FixtureData fixtureData = new FixtureData();
                fixtureData.MakeFixture(teams[i], teams[i + 1]);
                newList.Add(fixtureData);
            }
            for(int i = teams.Count - 1; i >1; i--)
            {
                Team tmp = teams[i - 1];
                teams[i - 1] = teams[i];
                teams[i] = tmp;
            }
        }
        return newList;
    }

    void Display()
    {
        float tamplateHight = 175f;
        int i = 0;

        foreach (FixtureData fd in fixture)
        {
            Transform fixtureTransform = Instantiate(fixtureTxt, fixtureContiner);
            i++;
            RectTransform fixtureRectTransform = fixtureTransform.GetComponent<RectTransform>();
            fixtureRectTransform.anchoredPosition = new Vector2(761.9554f, -tamplateHight * i);
            fixtureTransform.gameObject.SetActive(true);

            fixtureTransform.GetComponent<Text>().text = fd.GetFixtureData();
            Debug.Log(fd.GetFixtureData());
                    
        }
    }
    public void GetPlayerFixture()
    {
        FixtureData currentFixture = fixture[currentFixtureIndex];
            
            TeamManager.insatance.playerTeam = currentFixture.homeTeam.name;
            TeamManager.insatance.awayTeam = currentFixture.awayTeam.name;

    }
}
public class Team
{
    public string name;
    public int win;
    public int draw;
    public int loss;
    public int pts;
    public Team(string _name)
    {
        name = _name;
    }
}
public class FixtureData
{
    public Team homeTeam;
    public Team awayTeam;
    public void  MakeFixture(Team one, Team two)
    {
        homeTeam = one;
        awayTeam = two;
    }
    public string GetFixtureData()
    {
        return homeTeam.name + " vs " + awayTeam.name;
    }

}
