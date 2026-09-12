using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamemgr : MonoBehaviour
{
    public enum GameState { Run, Exit }
    public GameState state;

    int score = 0;
    int otherScore = 0;
    [SerializeField] TMP_Text playerScoreText;
    [SerializeField] TMP_Text otherScoreText;
    [SerializeField] GameObject resultPanel;

    PhotonView pv;
    colmgr col;

    // Start is called before the first frame update
    void Start()
    {
        photonmgr pmgrsc = GameObject.Find("photonmgr").GetComponent<photonmgr>();
        pmgrsc.CreateJoinRoom();

        col = GameObject.Find("colmgr").GetComponent<colmgr>();
        resultPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => Exit());
        pv = GetComponent<PhotonView>();


        state = GameState.Run;
    }

    // Update is called once per frame
    void Update()
    {

        playerScoreText.text = "MY : " + score.ToString();
        otherScoreText.text = "OTHER : " + otherScore.ToString();

        if (PhotonNetwork.PlayerList.Length < 2) return;
        if (state.Equals(GameState.Exit)) return;

        col.ColUpdate();       

        if (score >= 10)
            pv.RPC("GameExit", RpcTarget.All);
    }

    public void SettingScore(int score)
    {
        this.score += score;

        pv.RPC("SetScore", RpcTarget.Others, this.score);
    }

    [PunRPC]
    private void SetScore(int score)
    {
        this.otherScore = score;
    }

    [PunRPC]
    private void GameExit()
    {
        state = GameState.Exit;

        if (this.score >= 10)
        {
            //승리 출력
            resultPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "MY WIN";
        }
        else if (this.otherScore >= 10)
        {
            //패배 출력
            resultPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "MY LOSE";
        }

        resultPanel.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Exit");
    }
}
