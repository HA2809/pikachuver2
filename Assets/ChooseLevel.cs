using UnityEngine;

public class ChooseLevel : MonoBehaviour
{
    public void SelectClassic()
    {
        GameModeManager.Instance.SetGameMode(1);
    }

    public void SelectRiseAndConnect()
    {
        GameModeManager.Instance.SetGameMode(2);
    }

    public void SelectDropAndLink()
    {
        GameModeManager.Instance.SetGameMode(3);
    }

    public void SelectMaster()
    {
        GameModeManager.Instance.SetGameMode(4);
    }
}

