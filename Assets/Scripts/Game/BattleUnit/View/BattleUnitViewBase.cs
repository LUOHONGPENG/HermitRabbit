using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitViewBase : MonoBehaviour
{
    public SpriteRenderer srUnit;

    public Collider colUnit;

    private void LateUpdate()
    {
        srUnit.transform.LookAt(Camera.main.transform.forward + srUnit.transform.position);
    }
}
