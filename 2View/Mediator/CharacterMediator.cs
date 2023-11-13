using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMediator : EventMediator
{
    [Inject]
    public CharacterView CharacterView { get; set; }

    public override void OnRegister()
    {
        //更新头像
        CharacterView.Init();

        dispatcher.AddListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.AddListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.AddListener(ViewEvent.DealThreeCards, OnDealThreeCards);
        dispatcher.AddListener(ViewEvent.RequestPlay, OnPlayerPlayCard);
        dispatcher.AddListener(ViewEvent.SuccessedPlay, OnSuccessedPlay);
        dispatcher.AddListener(ViewEvent.UpdateIntegration, OnUpdateIntegration);
        dispatcher.AddListener(ViewEvent.RestartGame, OnRestartGame);
       
        dispatcher.Dispatch(CommandEvent.RequestUpdate);

        RoundModel.ComputerHandler += RoundModel_ComputerHandler;
        RoundModel.PlayerHandler += RoundModel_PlayerHandler;
    }


    public override void OnRemove()
    {
        dispatcher.RemoveListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.RemoveListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.DealThreeCards, OnDealThreeCards);
        dispatcher.RemoveListener(ViewEvent.RequestPlay, OnPlayerPlayCard);
        dispatcher.RemoveListener(ViewEvent.UpdateIntegration, OnUpdateIntegration);
        dispatcher.RemoveListener(ViewEvent.SuccessedPlay, OnSuccessedPlay);
        dispatcher.RemoveListener(ViewEvent.RestartGame, OnRestartGame);

        RoundModel.ComputerHandler -= RoundModel_ComputerHandler;
        RoundModel.PlayerHandler -= RoundModel_PlayerHandler;

    }


    /// <summary>
    /// 处理发牌
    /// </summary>
    /// <param name="evt"></param>
    private void OnDealCard(IEvent evt)
    {
        DealCardArgs e = (DealCardArgs)evt.data;
        CharacterView.AddCard(e.cType, e.card, e.isSlect, ShowPoint.Desk);
        Sound.Instance.PlayEffect(Consts.DealCard);
    }
    /// <summary>
    /// 发牌结束后
    /// </summary>
    private void OnCompleteDeal()
    {
        CharacterView.ComputerLeftControl.Sort(true);
        CharacterView.ComputerRightControl.Sort(true);
        CharacterView.DeskControl.Sort(true);
    }

    /// <summary>
    /// 发三张牌
    /// </summary>
    /// <param name="payload"></param>
    private void OnDealThreeCards(IEvent evt)
    {
        GrabAndDisGrabArgs e = (GrabAndDisGrabArgs)evt.data;
        CharacterView.DealThreeCard(e.cType);
    }

    /// <summary>
    /// 玩家出牌调用
    /// </summary>
    private void OnPlayerPlayCard()
    {
        //不能直接出牌
        //需要判断 
        List<Card> cardList = CharacterView.PlayerControl.FindSelectCard();
        CardType cardType;
       if( Rulers.CanPop(cardList, out cardType))
        {
            PlayCardArgs e = new PlayCardArgs()
            {
                CardType = cardType,
                CharacterType = CharacterType.Player,
                Length = cardList.Count,
                Weight = Tool.GetWeight(cardList, cardType)
            };

            dispatcher.Dispatch(CommandEvent.PlayCard, e);

        }
        else
        {
            Debug.Log("牌不对！");
        }
    }
    /// <summary>
    /// 成功出牌
    /// </summary>
    /// <param name="payload"></param>
    private void OnSuccessedPlay()
    {
        List<Card> cardList = CharacterView.PlayerControl.FindSelectCard();
        //清空桌面
        CharacterView.DeskControl.Clear(ShowPoint.Player);
        //添加到桌面
        foreach (var card in cardList)
            CharacterView.DeskControl.AddCard(card, false,ShowPoint.Player);

        CharacterView.PlayerControl.DestroySelectCard();

        if (!CharacterView.PlayerControl.HasCard)
        {
            //游戏结束
            Identity r = CharacterView.ComputerRightControl.Identity;
            Identity l = CharacterView.ComputerLeftControl.Identity;
            Identity p = CharacterView.PlayerControl.Identity;
            GameOverArgs eee = new GameOverArgs()
            {
                ComputerRightWin = r == p ? true : false,
                ComputerLeftWin = l == p ? true : false,
                PlayerWin  = true,
                isLandlord = p == Identity.Landlord,
            };
            //游戏结束
            dispatcher.Dispatch(CommandEvent.GameOver, eee);
        }


    }

    private void RoundModel_ComputerHandler(ComputerSmartArgs e)
    {
        StartCoroutine(Delay(e));
    }
        /// <summary>
        /// 电脑出牌
        /// </summary>
        /// <param name="e">传递的一些参数</param>
        private IEnumerator Delay(ComputerSmartArgs e)
    {
        bool can = false;
        yield return new WaitForSeconds(1.4f);

        switch (e.CharacterType)
        { 
            case CharacterType.ComputerRight:

                //清空桌面
                CharacterView.DeskControl.Clear(ShowPoint.ComputerRight);


                can =  CharacterView.ComputerRightControl.SmartSelectCards(e.CardType, e.Weight, e.Length, 
                    e.IsBiggest == CharacterType.ComputerRight);
                if (can)
                {
                    List<Card> cardList = CharacterView.ComputerRightControl.SelectCards;
                    CardType CurrType = CharacterView.ComputerRightControl.CurrType;

                    //添加牌桌面
                    foreach (var card in cardList)
                        CharacterView.DeskControl.AddCard(card, false, ShowPoint.ComputerRight);
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                         CardType = CurrType,
                         Length = cardList.Count,
                         CharacterType = CharacterType.ComputerRight,
                          Weight = Tool.GetWeight(cardList,CurrType)
                    };


                    //判断胜负
                    if (!CharacterView.ComputerRightControl.HasCard)
                    {
                        Identity r = CharacterView.ComputerRightControl.Identity;
                        Identity l = CharacterView.ComputerLeftControl.Identity;
                        Identity p = CharacterView.PlayerControl.Identity;
                        GameOverArgs eee = new GameOverArgs()
                        {
                            ComputerRightWin = true,
                            ComputerLeftWin = l == r ? true : false,
                            PlayerWin = p == r ? true : false,
                            isLandlord = p == Identity.Landlord,

                        };
                        //游戏结束
                        dispatcher.Dispatch(CommandEvent.GameOver,eee);
                    }
                    else
                    {

                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);
                    }
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }

                break;
            case CharacterType.ComputerLeft:
                //清空桌面
                CharacterView.DeskControl.Clear(ShowPoint.ComputerLeft);
                
                can = CharacterView.ComputerLeftControl.SmartSelectCards(e.CardType, e.Weight, e.Length,
                    e.IsBiggest == CharacterType.ComputerLeft);
                if (can)
                {
                    List<Card> cardList = CharacterView.ComputerLeftControl.SelectCards;
                    CardType CurrType = CharacterView.ComputerLeftControl.CurrType;
                    //添加牌桌面
                    foreach (var card in cardList)
                        CharacterView.DeskControl.AddCard(card, false, ShowPoint.ComputerLeft);
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                        CardType = CurrType,
                        Length = cardList.Count,
                        CharacterType = CharacterType.ComputerLeft,
                        Weight = Tool.GetWeight(cardList, CurrType)
                    };


                    //判断胜负
                    if (!CharacterView.ComputerLeftControl.HasCard)
                    {
                        //游戏结束
                        Identity r = CharacterView.ComputerRightControl.Identity;
                        Identity l = CharacterView.ComputerLeftControl.Identity;
                        Identity p = CharacterView.PlayerControl.Identity;
                        GameOverArgs eee = new GameOverArgs()
                        {
                            ComputerLeftWin = true,
                            ComputerRightWin = r == l ? true : false,
                            PlayerWin = p == l ? true : false,
                            isLandlord = p == Identity.Landlord,

                        };
                        //游戏结束
                        dispatcher.Dispatch(CommandEvent.GameOver, eee);
                    }
                    else
                    {

                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);
                    }
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;

            default:
                break;
        }
    }
    /// <summary>
    /// 玩家出牌监听
    /// </summary>
    /// <param name="obj"></param>
    private void RoundModel_PlayerHandler(bool obj)
    {
        CharacterView.DeskControl.Clear(ShowPoint.Player);
    }
    /// <summary>
    /// 更新数据UI
    /// </summary>
    /// <param name="payload"></param>
    private void OnUpdateIntegration(IEvent data)
    {
        GameData gameData = (GameData)data.data;
        CharacterView.PlayerControl.characterUI.SetIntergation(gameData.playerIntegration);
        CharacterView.ComputerLeftControl.characterUI.SetIntergation(gameData.computerLeftIntegration);
        CharacterView.ComputerRightControl.characterUI.SetIntergation(gameData.computerRightIntegration);
    }

    /// <summary>
    /// 重新开始
    /// </summary>
    private void OnRestartGame()
    {
        //对象池的回收
        Lean.Pool.LeanPool.DespawnAll();

        //数据移除
        CharacterView.PlayerControl.CardList.Clear();
        CharacterView.ComputerLeftControl.CardList.Clear();
        CharacterView.ComputerRightControl.CardList.Clear();
        CharacterView.DeskControl.CardList.Clear();

        //初始化UI
        CharacterView.Init();
        CharacterView.PlayerControl.characterUI.SetRemain(0);
        CharacterView.ComputerLeftControl.characterUI.SetRemain(0);
        CharacterView.ComputerRightControl.characterUI.SetRemain(0);
        CharacterView.DeskControl.deskUI.SetAlpha(0);

    }

}
