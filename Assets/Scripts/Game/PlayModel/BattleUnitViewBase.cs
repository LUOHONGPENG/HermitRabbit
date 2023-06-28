using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitViewBase : MonoBehaviour
{
    public SpriteRenderer srModel;

    private void LateUpdate()
    {
        srModel.transform.LookAt(Camera.main.transform.forward + srModel.transform.position);
    }
}
