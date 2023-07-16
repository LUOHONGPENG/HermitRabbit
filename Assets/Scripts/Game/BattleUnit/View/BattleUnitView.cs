using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitView : MonoBehaviour
{
    public SpriteRenderer srUnit;

    public Collider colUnit;




    private void LateUpdate()
    {
        Vector2 cameraPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 direction = cameraPos - thisPos;
        direction.Normalize();
        srUnit.transform.localPosition = new Vector3(direction.x * 0.35f, 0, direction.y * 0.35f);
        srUnit.transform.LookAt(Camera.main.transform.forward + srUnit.transform.position);
    }

    public void MoveToPos(Vector2Int posID)
    {
        Vector3 tilePos = PublicTool.ConvertPosFromID(posID);
        this.transform.localPosition = new Vector3(tilePos.x, 0.35f, tilePos.z);
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
