using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class titlemgr : MonoBehaviour
{
    public Button titlebtn;
    public TMP_InputField roomidinput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTitleButton()
    {
        photonmgr pmgrsc = GameObject.Find("photonmgr").GetComponent<photonmgr>();
        pmgrsc.roomName = roomidinput.text;
        pmgrsc.OnConnet();
    }
}
