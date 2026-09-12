using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public enum PlayerState
    {
        idle = 0,
        run,
    }

    gamemgr gmr;
    Vector3 goalpos;
    float speed = 1f;//5f;//10f;
    PlayerState pstate;
    Animator ani;
    SpriteRenderer sp;

    PhotonView pv;
    // Start is called before the first frame update
    private void Awake()
    {
        ani = transform.GetChild(0).gameObject.GetComponent<Animator>();
        sp = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        pv = GetComponent<PhotonView>();
        gmr = FindObjectOfType<gamemgr>();
    }
    void Start()
    {
        /*
        if (pv.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(0, 2f, 0);
            }
            else
            {                
                transform.position = new Vector3(0, -2f, 0);
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {                
                transform.position = new Vector3(0, -2f, 0);
            }
            else
            {                
                transform.position = new Vector3(0, 2f, 0);
            }
        }
        */
        goalpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gmr.state.Equals(gamemgr.GameState.Exit)) return;
        if (!pv.IsMine) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos2d = pos;            
            goalpos = pos2d;
            
            SetPlayerState(PlayerState.run);

            if ((goalpos.x - transform.position.x) <= 0)
            {
                sp.flipX = true;
            } else
            {
                sp.flipX = false;
            }              
        }

        transform.position = Vector3.MoveTowards(transform.position, goalpos, speed * Time.deltaTime);
        
        float length = Vector3.Distance(transform.position, goalpos);
        if(length <= 0.01f)
        {
            SetPlayerState(PlayerState.idle);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, goalpos, speed * Time.deltaTime);
            
            //pv.RPC("SetMoveRPC", RpcTarget.All, transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "item")
        {
            collision.gameObject.GetComponent<itemobj>().Get(this);
        }
    }

    void SetPlayerState(PlayerState state)
    {
        if (pstate == state) return;
        pstate = state;
        switch(pstate)
        {
            case PlayerState.idle:
                {
                    ani.Play("idle");
                }
                break;
            case PlayerState.run:
                {
                    ani.Play("run");
                }
                break;
        }
    }

    [PunRPC]
    void SetMoveRPC(Vector3 pos)
    {
        transform.position = pos;
    }
}
