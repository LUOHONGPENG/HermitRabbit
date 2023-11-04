using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitAniState
{
    Idle,
    Ready
}


public partial class BattleUnitView
{
    public Animator aniUnit;

    public void ChangeAniState(UnitAniState state)
    {
        if(aniUnit != null)
        {
            aniUnit.Play(state.ToString(), -1, 0);
        }
    }



}
