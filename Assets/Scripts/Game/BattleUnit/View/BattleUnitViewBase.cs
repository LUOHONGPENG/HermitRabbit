using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitViewBase : MonoBehaviour
{
    public SpriteRenderer srUnit;

    public Collider colUnit;

    private void LateUpdate()
    {
        Vector2 cameraPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 direction = cameraPos - thisPos;
        direction.Normalize();
        srUnit.transform.localPosition = new Vector3(direction.x * 0.4f, 0, direction.y * 0.4f);
        srUnit.transform.LookAt(Camera.main.transform.forward + srUnit.transform.position);
    }
}
