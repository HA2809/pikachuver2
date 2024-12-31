using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSession : MonoBehaviour
{
    public Button exitButton;
    public string username;
    public string SaveSessionURL = "http://localhost/pikachudbver2/saveSession.php";
    public BlockCtrl[,] blockMatrix;

    public int roundID;
    public int levelID;
    public int playTime;
    public int hintUsed;
    public int shuffleUsed;
    public bool state;

    void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClick);
        }

        InitializeBlockMatrix();
    }

    private void InitializeBlockMatrix()
    {
        GridSystem gridSystem = GridManagerCtrl.Instance.gridSystem;
        if (gridSystem != null)
        {
            int width = gridSystem.width;
            int height = gridSystem.height;
            blockMatrix = new BlockCtrl[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Node node = gridSystem.GetNodeByXY(x, y);
                    if (node != null)
                    {
                        blockMatrix[x, y] = node.blockCtrl;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("GridSystem is null");
        }
    }

    public void OnExitButtonClick()
    {
        UpdatePlayerProgress();

        if (blockMatrix == null)
        {
            Debug.LogError("blockMatrix is null");
            return;
        }

        string matrixJson = JsonUtility.ToJson(new BlockMatrixWrapper(blockMatrix));

        StartCoroutine(SaveSessionData(username, roundID, levelID, playTime, hintUsed, shuffleUsed, state, matrixJson));
    }

    private void UpdatePlayerProgress()
    {
        roundID = GameManager.Instance.CurrentRoundID;
        levelID = GameManager.Instance.CurrentLevel;
        playTime = GameManager.Instance.CurrentPlayTime;
        hintUsed = GameManager.Instance.RemainHint;
        shuffleUsed = GameManager.Instance.RemainShuffle;
        state = GameManager.Instance.CurrentState;
    }

    private IEnumerator SaveSessionData(string Username, int RoundID, int LevelID, int PlayTime, int HintUsed, int ShuffleUsed, bool State, string Matrix)
    {
        WWWForm form = new WWWForm();
        form.AddField("UsernamePost", Username);
        form.AddField("RoundID", RoundID);
        form.AddField("LevelID", LevelID);
        form.AddField("PlayTime", PlayTime);
        form.AddField("HintUsed", HintUsed);
        form.AddField("ShuffleUsed", ShuffleUsed);
        form.AddField("State", State ? 1 : 0);
        form.AddField("Matrix", Matrix);
        WWW www = new WWW(SaveSessionURL, form);
        yield return www;

        if (www.error != null)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Response: " + www.text);
        }
    }

    [System.Serializable]
    public class BlockMatrixWrapper
    {
        public List<BlockDataWrapper> blocks;

        public BlockMatrixWrapper(BlockCtrl[,] blockMatrix)
        {
            blocks = new List<BlockDataWrapper>();
            for (int i = 0; i < blockMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < blockMatrix.GetLength(1); j++)
                {
                    if (blockMatrix[i, j] != null)
                    {
                        blocks.Add(new BlockDataWrapper(blockMatrix[i, j]));
                    }
                    else
                    {
                        Debug.LogWarning($"blockMatrix[{i}, {j}] is null");
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class BlockDataWrapper
    {
        public string blockID;
        public string spriteName;
        public float x;
        public float y;
        public bool isRemoved;

        public BlockDataWrapper(BlockCtrl block)
        {
            blockID = block.blockID;
            spriteName = block.sprite != null ? block.sprite.name : "null";
            x = block.transform.position.x;
            y = block.transform.position.y;
            isRemoved = !block.IsOccupied();
        }
    }
}




