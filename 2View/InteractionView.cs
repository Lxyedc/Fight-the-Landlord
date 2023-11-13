using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionView : View {

    /// <summary>
    /// 发牌
    /// </summary>
    public Button Deal;

    public Button Grab;
    public Button DisGrab;

    public Button Play;
    public Button Pass;

    /// <summary>
    /// 全部隐藏
    /// </summary>
    public void DeactiveAll()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
        Deal.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示发牌按钮
    /// </summary>
    public void AvtivePlay()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
        Deal.gameObject.SetActive(true);
    }
    /// <summary>
    /// 显示抢地主
    /// </summary>
    public void ActiveGrabAndDisGrab()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(true);
        DisGrab.gameObject.SetActive(true);
        Pass.gameObject.SetActive(false);
        Deal.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示出牌按钮
    /// </summary>
    public void ActiveDealAndPass(bool isActive = true)
    {
        Play.gameObject.SetActive(true);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Pass.gameObject.SetActive(true);
        Pass.interactable = isActive;
        Deal.gameObject.SetActive(false);
    }
}
