using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSession : MonoBehaviour
{
    public Button continueButton;
    public string LoadSessionURL = "http://localhost/pikachudbver2/loadSession.php";
    public Transform blockParent;
    public GameObject blockPrefab;

    void Start()
    {
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueButtonClick);
        }
    }

    void OnContinueButtonClick()
    {
        // This method is now empty because we will call LoadUserSession directly from ContinueButtonHandler
    }

    public void LoadUserSession(string username, int modeID, int roundID)
    {
        StartCoroutine(LoadSessionData(username, modeID, roundID));
    }

    private IEnumerator LoadSessionData(string username, int modeID, int roundID)
    {
        WWWForm form = new WWWForm();
        form.AddField("UsernamePost", username);
        form.AddField("ModeIDPost", modeID);
        form.AddField("RoundIDPost", roundID);
        WWW www = new WWW(LoadSessionURL, form);
        yield return www;

        if (www.error != null)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Response: " + www.text);

            string jsonResponse = www.text;
            SessionData sessionData = JsonUtility.FromJson<SessionData>(jsonResponse);

            Debug.Log("Loaded Session: " + sessionData.Matrix);
            BlockMatrixWrapper matrixWrapper = JsonUtility.FromJson<BlockMatrixWrapper>(sessionData.Matrix);
            LoadMatrix(matrixWrapper);
        }
    }

    private void LoadMatrix(BlockMatrixWrapper matrixWrapper)
    {
        foreach (var blockData in matrixWrapper.blocks)
        {
            GameObject blockObj = Instantiate(blockPrefab, new Vector3(blockData.x, blockData.y, 0), Quaternion.identity, blockParent);
            BlockCtrl blockCtrl = blockObj.GetComponent<BlockCtrl>();
            blockCtrl.blockID = blockData.blockID;
            blockCtrl.sprite = Resources.Load<Sprite>(blockData.spriteName);
            blockCtrl.ReloadModel();
        }
    }

    [System.Serializable]
    public class SessionData
    {
        public int SessionID;
        public int RoundID;
        public int LevelID;
        public int PlayTime;
        public int HintUsed;
        public int ShuffleUsed;
        public bool State;
        public string Matrix;
    }

    [System.Serializable]
    public class BlockMatrixWrapper
    {
        public List<BlockDataWrapper> blocks;
    }

    [System.Serializable]
    public class BlockDataWrapper
    {
        public string blockID;
        public string spriteName;
        public float x;
        public float y;
    }
}



