using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemobj : MonoBehaviour
{
    gamemgr gamemgr;

    SpriteRenderer sp;
    Collider2D col;
    PhotonView pv;
    //private Animator mosani;
    public GameObject EffectPre;

    int score = 1;

    // Start is called before the first frame update
    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        pv = GetComponent<PhotonView>();
        gamemgr = GameObject.Find("gamemgr").GetComponent<gamemgr>();
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void SetAlive()
    {

        SetObjActive(true);
        //col.enabled = true;
        //sp.color = Color.white;
    }

    public void Get(playercontrol target)
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    transform.position = new Vector3(0, 1000, 0);
        //}

        gamemgr.SettingScore(score);

        //gamemgr.SetScore();
        //col.enabled = false;
        //sp.color = Color.clear;

        SetObjActive(false);
        //GameObject eff = Instantiate(EffectPre, transform.position, Quaternion.identity);
        //StartCoroutine(SetEffEnd(eff));
    }

    public void SetObjActive(bool isactive)
    {
        //gameObject.SetActive(isactive);
        pv.RPC("ObjActiveRPC", RpcTarget.All, isactive, pv.ViewID);
    }

    [PunRPC]
    void ObjActiveRPC(bool isactive, int viewid)
    {
        //if (PhotonNetwork.IsMasterClient) return;

        gameObject.SetActive(isactive);
    }

    /*
    IEnumerator SetEffEnd(GameObject eff)
    {
        yield return new WaitForSeconds(0.2f);

        Destroy(eff);
        SetObjActive(false);
    }*/
}
