using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SelectionPanelControl : MonoBehaviour
{
    [SerializeField] private GameObject SelectionPanel,PropertiesPanel,IncrementalPanel;

    public RectTransform MainSelectionMenu,PropertiesMenu,IncrementalMenu;

    private void Start() 
    {
        MainSelectionMenu.DOAnchorPos(new Vector2(0,100),0.25f);
    }

    public void OpenIncrementalMenu()
    {
        IncrementalPanel.SetActive(true);
        IncrementalMenu.DOAnchorPos(new Vector2(0,500),0.25f);
        MainSelectionMenu.DOAnchorPos(new Vector2(-2500,0),0.25f).OnComplete(()=>SelectionPanel.SetActive(false));
    }

    public void OpenPropertiesMenu()
    {
        PropertiesPanel.SetActive(true);
        PropertiesMenu.DOAnchorPos(new Vector2(0,100),0.25f);
        MainSelectionMenu.DOAnchorPos(new Vector2(-2500,0),0.25f).OnComplete(()=>SelectionPanel.SetActive(false));
    }

    public void OpenMainMenuOnIncremental()
    {
        IncrementalMenu.DOAnchorPos(new Vector2(0,-500),0.25f).OnComplete(()=>IncrementalPanel.SetActive(false));
        SelectionPanel.SetActive(true);
        MainSelectionMenu.DOAnchorPos(new Vector2(0,100),0.25f);
    }

    public void OpenMainMenuOnProperties()
    {
        PropertiesMenu.DOAnchorPos(new Vector2(0,-200),0.25f).OnComplete(()=>PropertiesPanel.SetActive(false));
        SelectionPanel.SetActive(true);
        MainSelectionMenu.DOAnchorPos(new Vector2(0,100),0.25f);
    }
}
