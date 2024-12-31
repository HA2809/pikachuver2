using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewMode : MonoBehaviour
{
    public void PlayGame()
    {
        int gameMode = GameModeManager.Instance.selectedGameMode;
        SceneManager.LoadSceneAsync(gameMode);
    }
}


