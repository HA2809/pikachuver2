using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataCheckingLogin : MonoBehaviour
{
    public InputField usernameInputField;
    public InputField passwordInputField;
    public Button submitButton;
    public GameObject errorPanel;
    public Button exitButton;
    public GameObject mainMenuPanel;
    public GameObject loginPanel;

    string LoginUserURL = "http://localhost/pikachudbver2/loginUser.php";

    
    public static string loggedInUsername;
    public static int loggedInModeID;
    public static int loggedInRoundID;

    // Start is called before the first frame update
    void Start()
    {
        usernameInputField.text = "Username";
        passwordInputField.text = "Password";

        submitButton.onClick.AddListener(OnSubmitButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);

        errorPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    void OnSubmitButtonClick()
    {
        string inputUsername = usernameInputField.text;
        string inputPassword = passwordInputField.text;
        StartCoroutine(LoginUser(inputUsername, inputPassword));
    }

    void OnExitButtonClick()
    {
        errorPanel.SetActive(false);
    }

    private IEnumerator LoginUser(string Username, string Password)
    {
        WWWForm form = new WWWForm();
        form.AddField("UsernamePost", Username);
        form.AddField("PasswordPost", Password);
        WWW www = new WWW(LoginUserURL, form);
        yield return www;

        if (www.error != null)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Response: " + www.text);
            if (www.text.Contains("Invalid username or password"))
            {
                errorPanel.SetActive(true);
            }
            else if (www.text.Contains("Login successful"))
            {
                Debug.Log("Login successful");
               
                loggedInUsername = Username;
                loggedInModeID = 1; 
                loggedInRoundID = 1; 

                // Main Menu
                mainMenuPanel.SetActive(true);
                loginPanel.SetActive(false);
            }
        }
    }
}




