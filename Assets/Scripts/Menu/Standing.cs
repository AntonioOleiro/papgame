using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Standing : MonoBehaviour
{
    public static Standing instance;
    [SerializeField]
    private Transform entryContiner;
    [SerializeField]
    private Transform entryTamplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        entryTamplate.gameObject.SetActive(false);
        // entry and score and name in highscoreentry list...
        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{pts = 7, name = PlayerPrefs.GetString("TeamName"), win = 2, draw = 1, loss = 0 },
            new HighscoreEntry{pts = 1, name = "braga", win = 0, draw = 1, loss = 2 },
             new HighscoreEntry{pts = 2, name = "benfica", win = 0, draw = 2, loss = 1 },
              new HighscoreEntry{pts = 6, name = "eletrico", win = 2, draw = 0, loss = 1 },
               new HighscoreEntry{pts = 4, name = "sporting", win = 1, draw = 1, loss = 1 },
                new HighscoreEntry{pts = 9, name = "lombos", win = 3, draw = 0, loss = 0 },

        };

        //sorting entry list by score..
        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].pts > highscoreEntryList[i].pts)
                {
                    //swap...
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreatingHighscoreEntryTransform(highscoreEntry, entryContiner, highscoreEntryTransformList);
        }
    }

    private void CreatingHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float tamplateHeight = 87.546f;  //template height....
        //set of for rank.. instantiate 1 template 10times....[1]
        Transform entryTransform = Instantiate(entryTamplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -tamplateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        //Setting rank order, username and score....[2]
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("SnTxt").GetComponent<Text>().text = rankString; // write rank order in all clone score template..

        int pts = highscoreEntry.pts;
        entryTransform.Find("ptsTxt").GetComponent<Text>().text = pts.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("teamTxt").GetComponent<Text>().text = name;
        if(entryTransform.Find("teamTxt").GetComponent<Text>().text == PlayerPrefs.GetString("TeamName"))
        {
            entryTransform.Find("teamTxt").GetComponent<Text>().color = Color.blue;
            entryTransform.Find("SnTxt").GetComponent<Text>().color = Color.blue;
            entryTransform.Find("winTxt").GetComponent<Text>().color = Color.blue;
            entryTransform.Find("DrawTxt").GetComponent<Text>().color = Color.blue;
            entryTransform.Find("lossTxt").GetComponent<Text>().color = Color.blue;
            entryTransform.Find("ptsTxt").GetComponent<Text>().color = Color.blue;
        }

        int win = highscoreEntry.win;
        entryTransform.Find("winTxt").GetComponent<Text>().text = win.ToString();

        int draw = highscoreEntry.draw;
        entryTransform.Find("DrawTxt").GetComponent<Text>().text = draw.ToString();

        int loss = highscoreEntry.loss;
        entryTransform.Find("lossTxt").GetComponent<Text>().text = loss.ToString();

        transformList.Add(entryTransform);
    }


    /*
     * Represents single highScore entry.  creat class to make all entry....
     */
    private class HighscoreEntry
    {
        public int pts;
        public string name;
        public int win;
        public int loss;
        public int draw;
    }
}
