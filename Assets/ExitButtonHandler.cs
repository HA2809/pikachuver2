using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButtonHandler : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClick);
        }
    }

    void OnExitButtonClick()
    {
        
        SaveSession saveSession = FindObjectOfType<SaveSession>();
        if (saveSession != null)
        {
            saveSession.OnExitButtonClick();
        }

        
        SceneManager.LoadScene(0);
    }
}



