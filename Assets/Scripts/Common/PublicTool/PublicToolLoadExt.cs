using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static void BeforeLoad()
    {
        BattleMgr.Instance.StopForQuit();
    }
}
