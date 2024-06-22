using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUIName : MonoBehaviour
{
    [SerializeField]

    private Text textBox;

    [SerializeField]
    private string[] names = { "Lombos", "Benfica", "Braga", "Sporting", "Eletrico" };

    void Start()
    {
        SetRandomName();
    }

    private void SetRandomName()
    {
        string randomName = names[Random.Range(0, names.Length)];
        textBox.text = randomName;
    }


    void Update()
    {
        
    }
}
