using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonHandler : MonoBehaviour
{
    public Button continueButton;

    void Start()
    {
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueButtonClick);
        }
    }

    void OnContinueButtonClick()
    {
        LoadSession loadSession = FindObjectOfType<LoadSession>();
        if (loadSession != null)
        {
         
            string username = DataCheckingLogin.loggedInUsername ?? DataInserterSignUp.registeredUsername;
            int modeID = DataCheckingLogin.loggedInModeID != 0 ? DataCheckingLogin.loggedInModeID : DataInserterSignUp.registeredModeID;
            int roundID = DataCheckingLogin.loggedInRoundID != 0 ? DataCheckingLogin.loggedInRoundID : DataInserterSignUp.registeredRoundID;

            loadSession.LoadUserSession(username, modeID, roundID);
        }
    }
}




