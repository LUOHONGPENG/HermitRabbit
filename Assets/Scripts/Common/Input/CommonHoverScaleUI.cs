using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonHoverScaleUI : CommonHoverUI
{
    public Transform scaleTf;

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (scaleTf != null)
            {
                if (isHavor)
                {
                    this.transform.localScale = new Vector2(1.1f, 1.1f);
                }
                else
                {
                    this.transform.localScale = Vector2.one;
                }
            }
        }
    }
}
