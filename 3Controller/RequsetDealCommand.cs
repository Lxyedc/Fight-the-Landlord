using strange.extensions.command.impl;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class RequsetDealCommand : EventCommand
{
    [Inject]
    public CardModel CardModel { get; set; }

    public DeskControl DeskControl { get { return GameObject.FindObjectOfType<DeskControl>(); } }

    public override void Execute()
    {
        //发牌操作
        //UnityEngine.Debug.Log("发牌");
        //洗牌
        CardModel.Shuffle();
        DeskControl.StartCoroutine(DealCard());

    }

     IEnumerator  DealCard()
    {
        //给每个人17张
        CharacterType curr = CharacterType.Player;
        for (int i = 0; i < 51; i++)
        {
            if (curr == CharacterType.Library || curr == CharacterType.Desk)
                curr = CharacterType.Player;
            Deal(curr);
            curr++;
            yield return new WaitForSeconds(0.1f);
        }

        //地主牌 桌面发

        for (int i = 0; i < 3; i++)
        {
            Deal(CharacterType.Desk);
        }

        CardUI[] cardUIs = DeskControl.CreatePoint.GetComponentsInChildren<CardUI>();
        foreach (var ui in cardUIs)
            ui.SetImageAgain();

        //发牌结束
        dispatcher.Dispatch(ViewEvent.CompleteDeal);
    }

    /// <summary>
    /// 发牌
    /// </summary>
    /// <param name="cType"></param>
    void Deal(CharacterType cType)
    {
        Card card = CardModel.DealCard(cType);
        DealCardArgs e = new DealCardArgs()
        {
            card = card,
            cType = cType,
            isSlect = false
        };
        dispatcher.Dispatch(ViewEvent.DealCard,e);

    }
}