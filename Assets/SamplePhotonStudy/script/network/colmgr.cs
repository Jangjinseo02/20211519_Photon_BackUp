using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colmgr : MonoBehaviour
{
    public float time = 1f;
    public GameObject colpre;
    List<GameObject> collist = new List<GameObject>();
    float tick = 0;

    // Start is called before the first frame update
    void Start()
    {
        //inititem();
        tick = 0f;
    }

    public void inititem()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject itemobj = PhotonNetwork.InstantiateRoomObject("AppleItem", new Vector3(0, 1000f, 0), Quaternion.identity);
            //GameObject itemobj = Instantiate(colpre, transform);
            //itemobj.SetActive(false);
            itemobj.GetComponent<itemobj>().SetObjActive(false);
            collist.Add(itemobj);
        }
    }
    // Update is called once per frame
    public void ColUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)  return;
        if (PhotonNetwork.PlayerList.Length < 2)    return;
        //if (GlobalData.Instance.gstate != GameState.gstate_run) return;
        CreateCol();
    }

    void CreateCol()
    {
        tick += Time.deltaTime;

        if (tick >= time)
        {
            tick = 0.0f;
            for (int i = 0; i < collist.Count; i++)
            {
                if (!collist[i].activeSelf)
                {
                    Vector3 pos = Vector3.zero;
                    //pos.x = Random.RandomRange(-8.0f, 8.0f);
                    //pos.y = 4.5f;
                    pos.x = Random.RandomRange(-1.4f, 1.4f);
                    pos.y = Random.RandomRange(-2.5f, 2.5f);

                    collist[i].transform.position = pos;
                    //collist[i].SetActive(true);
                    collist[i].GetComponent<itemobj>().SetAlive();
                    break;
                }
            }
        }
    }
}
