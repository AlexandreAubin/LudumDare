using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = gameObject.GetComponentInChildren<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
