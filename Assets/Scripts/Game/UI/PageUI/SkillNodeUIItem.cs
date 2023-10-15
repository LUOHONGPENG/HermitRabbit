using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SkillNodeUIItem : MonoBehaviour
{
    public enum NodeState
    {
        Unlocked,
        CanUnlock,
        LockCondition,
        NotEnoughSP
    }

    public NodeState nodeState = NodeState.NotEnoughSP;

    public Image imgFrame;
    public Image imgIcon;
    public Image imgButton;
    public SpriteMask spMask;


    public List<Color> listColor = new List<Color>();

    public Button btnNode;
    public CommonHoverUI hoverUI;

    private SkillNodeExcelItem nodeItem;

    public void Init(SkillNodeExcelItem nodeItem)
    {
        this.nodeItem = nodeItem;

        //Set Sprite
        if(nodeItem.nodeType == SkillNodeType.Active)
        {
            imgFrame.sprite = Resources.Load("Sprite/Skill/imgIconSkill_Active", typeof(Sprite)) as Sprite;
            imgButton.sprite = Resources.Load("Sprite/Skill/imgIconSkill_ActiveFill", typeof(Sprite)) as Sprite;
            spMask.sprite = Resources.Load("Sprite/Skill/imgIconSkill_ActiveFill", typeof(Sprite)) as Sprite;
        }
        else if(nodeItem.nodeType == SkillNodeType.Passive)
        {
            imgFrame.sprite = Resources.Load("Sprite/Skill/imgIconSkill_Passive", typeof(Sprite)) as Sprite;
            imgButton.sprite = Resources.Load("Sprite/Skill/imgIconSkill_PassiveFill", typeof(Sprite)) as Sprite;
            spMask.sprite = Resources.Load("Sprite/Skill/imgIconSkill_PassiveFill", typeof(Sprite)) as Sprite;
        }
        else if(nodeItem.nodeType == SkillNodeType.Numerical)
        {
            imgFrame.sprite = Resources.Load("Sprite/Skill/imgIconSkill_Numerical", typeof(Sprite)) as Sprite;
            imgButton.sprite = Resources.Load("Sprite/Skill/imgIconSkill_NumericalFill", typeof(Sprite)) as Sprite;
            spMask.sprite = Resources.Load("Sprite/Skill/imgIconSkill_NumericalFill", typeof(Sprite)) as Sprite;
        }

        imgIcon.sprite = Resources.Load("Sprite/Skill/" + nodeItem.iconUrl, typeof(Sprite)) as Sprite;

        btnNode.onClick.RemoveAllListeners();
        btnNode.onClick.AddListener(delegate ()
        {
            RefreshNodeState();
            switch (nodeState)
            {
                case NodeState.Unlocked:
                    break;
                case NodeState.CanUnlock:
                    BattleCharacterData characterData = PublicTool.GetCharacterData(nodeItem.characterID);
                    characterData.SPSpent += nodeItem.costSP;
                    characterData.AcquireSkillNode(nodeItem.id);
                    EventCenter.Instance.EventTrigger("RefreshSkillTreeUI", null);
                    break;
                case NodeState.LockCondition:
                case NodeState.NotEnoughSP:
                    break;
            }
            //Check Whether the node is unlocked
        });
    }

    public int GetNodeID()
    {
        return nodeItem.id;
    }

    public void RefreshNodeState()
    {
        BattleCharacterData characterData = PublicTool.GetCharacterData(nodeItem.characterID);
        if (PublicTool.CheckWhetherCharacterUnlockSkill(nodeItem.characterID, nodeItem.id))
        {
            nodeState = NodeState.Unlocked;
        }
        else
        {
            bool isMeetUnlockNode = false;
            for(int i =0;i< nodeItem.conditionPreNode.Count; i++)
            {
                int unlockKey = nodeItem.conditionPreNode[i];
                if(unlockKey == 0)
                {
                    isMeetUnlockNode = true;
                    break;
                }

                if (characterData.CheckUnlockSkillNode(unlockKey))
                {
                    isMeetUnlockNode = true;
                    break;
                }
            }
            if(characterData.SPSpent < nodeItem.conditionSPSpent)
            {
                nodeState = NodeState.LockCondition;
            }
            else if (!isMeetUnlockNode)
            {
                nodeState = NodeState.LockCondition;
            }
            else if (characterData.SPLeft < nodeItem.costSP)
            {
                nodeState = NodeState.NotEnoughSP;
            }
            else
            {
                nodeState = NodeState.CanUnlock;
            }
        }
    }


    public void UpdateNodeUI()
    {
        RefreshNodeState();
        switch (nodeState)
        {
            case NodeState.Unlocked:
                imgFrame.color = listColor[2];
                imgIcon.color = listColor[2];
                break;
            case NodeState.CanUnlock:
                imgFrame.color = listColor[1];
                imgIcon.color = listColor[1];
                break;
            case NodeState.LockCondition:
            case NodeState.NotEnoughSP:
                imgFrame.color = listColor[0];
                imgIcon.color = listColor[0];
                break;
        }
    }



    private void Update()
    {
        if (hoverUI.isHavor && nodeState == NodeState.CanUnlock)
        {
            this.transform.localScale = new Vector2(1.1f, 1.1f);
        }
        else
        {
            this.transform.localScale = Vector2.one;
        }
    }
}
