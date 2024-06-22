using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] InputField inputField;
    private string teamName;
    [SerializeField] string sceneName;

    private void Start()
    {
        if (PlayerPrefs.HasKey("TeamName"))
        {
            inputField.text = PlayerPrefs.GetString("TeamName");
        }
    }
    public void SetTeamName()
    {
        teamName = inputField.text;
        PlayerPrefs.SetString("TeamName", teamName);

    }
    public void Play()
    {
        SceneManager.LoadScene(sceneName);
    }
}
