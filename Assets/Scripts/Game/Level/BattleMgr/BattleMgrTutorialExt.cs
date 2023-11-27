using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    

    public void CheckTurnStartTutorial()
    {
        if(gameData.numDay == 1 && numTurn == 2)
        {
            PublicTool.StartConditionalTutorial(TutorialGroup.Battle, 4, -1);
        }
        else if(gameData.numDay == 2 && numTurn == 1)
        {

        }
    }


}
