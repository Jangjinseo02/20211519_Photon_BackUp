using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class photonmgr : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public string roomName;
    private void Awake()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //OnConnet();
    }
    public void OnConnet()
    {
        int randnum = Random.RandomRange(1000, 9999);
        string playerName = "user" + randnum.ToString();

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }

    public override void OnConnectedToMaster()
    {
        //*
        PhotonNetwork.LoadLevel("samplegame");
        /*/
        //int randnum = Random.RandomRange(1000, 9999);
        int randnum = 1000;
        string roomName = "Room" + randnum.ToString();
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, null);
        //*/
    }

    public void CreateJoinRoom()
    {
        //int randnum = 1000;
        //string roomName = "Room" + randnum.ToString();
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 2 }, null);
    }
    public override void OnJoinedRoom()
    {
        //PhotonNetwork.CurrentRoom.PlayerCount*PhotonNetwork.LocalPlayer.GetPlayerNumber();
        //float usernum = (float)PhotonNetwork.LocalPlayer.GetPlayerNumber();
        //float usernum = (float)(PhotonNetwork.CurrentRoom.PlayerCount * PhotonNetwork.LocalPlayer.GetPlayerNumber());
        //Debug.Log("usernum : " + usernum.ToString());
        //PhotonNetwork.Instantiate("unitobj", new Vector3((-3f - (usernum)), 1f, 0f), Quaternion.identity, 0);
        PhotonNetwork.Instantiate("unit", Vector3.zero, Quaternion.identity, 0);

        if (PhotonNetwork.IsMasterClient)
        {
            GameObject.Find("colmgr").GetComponent<colmgr>().inititem();
            //DefaultBaseUtil.Instance.FindObjectScript<eatobjmgr>("eatobjmgr").EatObjInit();
        }
    }

    //void Reconnect()
    //{
    //    기존의 연결 끊기
    //    PhotonNetwork.Disconnect();

    //    연결 끊김을 기다리기 위해 잠시 대기
    //    StartCoroutine(ConnectAfterDisconnect());
    //}

    //IEnumerator ConnectAfterDisconnect()
    //{
    //    while (PhotonNetwork.IsConnected)
    //    {
    //        yield return null;
    //    }

    //    // 연결 재시도
    //    PhotonNetwork.ConnectUsingSettings();
    //}
}
