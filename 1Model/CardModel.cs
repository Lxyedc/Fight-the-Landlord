using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一副牌
/// </summary>
public class CardModel  {

    CharacterType cType = CharacterType.Library;
    Queue<Card> CardLibrary = new Queue<Card>();

    /// <summary>
    /// 剩余牌数
    /// </summary>
    public int CardCount
    {
        get
        {
            return CardLibrary.Count;
        }
    }

    /// <summary>
    /// 54张牌
    /// </summary>
    public void InitCardLibrary()
    {
        //52张
        for (int color = 1; color < 5; color++)
        {
            for (int weight = 0; weight < 13; weight++)
            {
                Colors c = (Colors)color;
                Weight w =(Weight) weight;
                string name = c.ToString() + w.ToString();
                Card card = new Card(name,c,w, cType);
                CardLibrary.Enqueue(card);
            }
        }

        Card sJoker = new Card("SJoker", Colors.None, Weight.SJoker, cType);
        Card lJoker = new Card("LJoker", Colors.None, Weight.LJoker, cType);
        CardLibrary.Enqueue(sJoker);
        CardLibrary.Enqueue(lJoker);


    }


    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        List<Card> newList = new List<Card>();
        foreach (var card in CardLibrary)
        {
            int index = Random.Range(0, newList.Count + 1);
            newList.Insert(index, card);
        }

        CardLibrary.Clear();
        foreach (var card in newList)
        {
            CardLibrary.Enqueue(card);
        }

        newList.Clear();
    }
    
    /// <summary>
    /// 最开始发牌
    /// </summary>
    /// <param name="sendTo">发给谁</param>
    public Card DealCard(CharacterType sendTo)
    {
        Card card = CardLibrary.Dequeue();
        card.BelongTo = sendTo;
        return card;
    }

}
