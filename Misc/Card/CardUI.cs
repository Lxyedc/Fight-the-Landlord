using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CardUI : MonoBehaviour {

    Card card;
    Image image;
    bool isSelected;
    LearnButton btn;

    public Card Card
    {
        get
        {
            return card;
        }

        set
        {
            card = value;
            SetImage();
        }
    }

    /// <summary>
    /// 是否被点击
    /// </summary>
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }

        set
        {
            if (card.BelongTo != CharacterType.Player || isSelected == value)
                return;

            if (value)
                transform.localPosition += Vector3.up * 10;
            else
                transform.localPosition -= Vector3.up * 10;

            isSelected = value;
        }
    }



    /// <summary>
    /// 设置图片
    /// </summary>
    public void SetImage()
    {
        if(card.BelongTo == CharacterType.Player || card.BelongTo == CharacterType.Desk)
        {
            image.sprite = Resources.Load<Sprite>("Pokers/" +card.CardName);
        }
        else //电脑 显示背面
        {
            image.sprite = Resources.Load<Sprite>("Pokers/FixedBack");
        }
    }

    /// <summary>
    ///第一次地主牌
    /// </summary>
    public void SetImageAgain()
    {
        image.sprite = Resources.Load<Sprite>("Pokers/CardBack");

    }

    /// <summary>
    /// 设置为位置以及偏移
    /// </summary>
    /// <param name="parent">父物体</param>
    /// <param name="index">子物体索引</param>
    public void SetPosition(Transform parent, int index)
    {
        transform.SetParent(parent, false);
        transform.SetSiblingIndex(index);

        if(card.BelongTo == CharacterType.Desk || card.BelongTo == CharacterType.Player)
        {
            transform.localPosition = Vector3.right * index *25;

            //防止还原
            if (IsSelected)
                transform.localPosition += Vector3.up * 10;

          

        }
        else if (card.BelongTo == CharacterType.ComputerLeft || card.BelongTo == CharacterType.ComputerRight)
            transform.localPosition = -Vector3.up * 8 * index + Vector3.left * 8 * index;

    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void OnSpawn()
    {
        image = GetComponent<Image>();
        btn = GetComponent<LearnButton>();
        btn.PressedBtn += Btn_PressedBtn;
        btn.HighlightedBtn += Btn_HighlightedBtn;
    }

    private void Btn_HighlightedBtn()
    {
        if (Input.GetMouseButton(1)){

            if (card.BelongTo == CharacterType.Player)
            {
                IsSelected = !IsSelected;
                Sound.Instance.PlayEffect(Consts.Select);

            }
        }
   
    }

    private void Btn_PressedBtn()
    {
        if (card.BelongTo == CharacterType.Player)
        {
            IsSelected = !IsSelected;
            Sound.Instance.PlayEffect(Consts.Select);
        }
    }



    /// <summary>
    /// 回收数据
    /// </summary>
    public void OnDespawn()
    {
        btn.PressedBtn -= Btn_PressedBtn;
        btn.HighlightedBtn -= Btn_HighlightedBtn;
        isSelected = false;
        image.sprite = null;
        card = null;
    }

    /// <summary>
    /// 回收
    /// </summary>
    public void Destroy()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }

}
