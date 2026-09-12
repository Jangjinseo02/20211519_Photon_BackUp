using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    gstate_init = 0,
    gstate_run,
    gstate_end,
}
public class GlobalData : Singleton<GlobalData>
{    
    //초기화
    public GlobalData()
    {

    }

    public int gameverion = 1;
    public GameState gstate = GameState.gstate_init;
}
