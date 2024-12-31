using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataInserterSignUp : MonoBehaviour
{
    public InputField usernameInputField;
    public InputField passwordInputField;
    public Button submitButton;
    public GameObject errorPanel;
    public Button exitButton;

    string CreateUserURL = "http://localhost/pikachudbver2/InsertUser.php";
    string CheckUserURL = "http://localhost/pikachudbver2/checkUser.php";

   
    public static string registeredUsername;
    public static int registeredModeID;
    public static int registeredRoundID;

    // Start is called before the first frame update
    void Start()
    {
        
        if (usernameInputField == null)
        {
            Debug.LogError("usernameInputField is not assigned");
            return;
        }
        if (passwordInputField == null)
        {
            Debug.LogError("passwordInputField is not assigned");
            return;
        }
        if (submitButton == null)
        {
            Debug.LogError("submitButton is not assigned");
            return;
        }
        if (errorPanel == null)
        {
            Debug.LogError("errorPanel is not assigned");
            return;
        }
        if (exitButton == null)
        {
            Debug.LogError("exitButton is not assigned");
            return;
        }

        usernameInputField.text = "Username";
        passwordInputField.text = "Password";

        submitButton.onClick.AddListener(OnSubmitButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);

        errorPanel.SetActive(false);
    }

    void OnSubmitButtonClick()
    {
        string inputUsername = usernameInputField.text;
        string inputPassword = passwordInputField.text;
        StartCoroutine(CheckUserExists(inputUsername, inputPassword));
    }

    void OnExitButtonClick()
    {
        errorPanel.SetActive(false);
    }

    private IEnumerator CheckUserExists(string Username, string Password)
    {
        WWWForm form = new WWWForm();
        form.AddField("UsernamePost", Username);
        WWW www = new WWW(CheckUserURL, form);
        yield return www;

        if (www.error != null)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Response: " + www.text);
            if (www.text.Contains("Username already exists"))
            {
                errorPanel.SetActive(true);
            }
            else
            {
                CreateUser(Username, Password);
            }
        }
    }

    public void CreateUser(string Username, string Password)
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            Debug.LogError("Username or Password is empty!");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("UsernamePost", Username);
        form.AddField("PasswordPost", Password);
        WWW www = new WWW(CreateUserURL, form);
        Debug.Log("Sending Username: " + Username);
        Debug.Log("Sending Password: " + Password);
        StartCoroutine(PostData(form));
    }

    private IEnumerator PostData(WWWForm form)
    {
        using (WWW www = new WWW(CreateUserURL, form))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                Debug.Log("Response: " + www.text);
              
                registeredUsername = usernameInputField.text;
                registeredModeID = 1;
                registeredRoundID = 1;
            }
        }
    }
}





