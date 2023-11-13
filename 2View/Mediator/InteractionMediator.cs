using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMediator : EventMediator
{
    [Inject]
    public InteractionView InteractionView { get; set; }

    [Inject]
    public RoundModel RoundModel { get; set; }

    public override void OnRegister()
    {
        InteractionView.Deal.onClick.AddListener(OnDealClick);
        InteractionView.Grab.onClick.AddListener(OnGrabClick);
        InteractionView.DisGrab.onClick.AddListener(OnDisGrabClick);
        InteractionView.Play.onClick.AddListener(OnPlayClick);
        InteractionView.Pass.onClick.AddListener(OnPassClick);

        dispatcher.AddListener(ViewEvent.RestartGame, OnRestartGame);
        dispatcher.AddListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.AddListener(ViewEvent.SuccessedPlay, OnSuccessedPlay);

        RoundModel.PlayerHandler += RoundModel_PlayerHandler;
    }

  

    public override void OnRemove()
    {
        InteractionView.Deal.onClick.RemoveListener(OnDealClick);
        InteractionView.Grab.onClick.RemoveListener(OnGrabClick);
        InteractionView.DisGrab.onClick.RemoveListener(OnDisGrabClick);
        InteractionView.Play.onClick.RemoveListener(OnPlayClick);
        InteractionView.Pass.onClick.RemoveListener(OnPassClick);

        dispatcher.RemoveListener(ViewEvent.RestartGame, OnRestartGame);
        dispatcher.RemoveListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.SuccessedPlay, OnSuccessedPlay);

        RoundModel.PlayerHandler -= RoundModel_PlayerHandler;

    }




    /// <summary>
    /// 点击发牌按钮
    /// </summary>
    private void OnDealClick()
    {
        dispatcher.Dispatch(CommandEvent.RequsetDeal);
        InteractionView.DeactiveAll();
    }

    /// <summary>
    /// 抢地主
    /// </summary>
    private void OnGrabClick()
    {
        InteractionView.DeactiveAll();
        GrabAndDisGrabArgs e = new GrabAndDisGrabArgs()
        {
            cType = CharacterType.Player
        };
        dispatcher.Dispatch(CommandEvent.GrabLandlord, e);
        Sound.Instance.PlayEffect(Consts.Grab);
    }

    /// <summary>
    /// 不抢地主
    /// </summary>
    private void OnDisGrabClick()
    {
        InteractionView.DeactiveAll();
        CharacterType temp = (CharacterType)UnityEngine.Random.Range(2, 4);
        GrabAndDisGrabArgs e = new GrabAndDisGrabArgs()
        {
            cType = temp
        };
        dispatcher.Dispatch(CommandEvent.GrabLandlord, e);
        Sound.Instance.PlayEffect(Consts.DisGrab);

    }


    /// <summary>
    /// 发牌结束
    /// </summary>
    /// <param name="payload"></param>
    private void OnCompleteDeal()
    {
        InteractionView.ActiveGrabAndDisGrab();
    }

    /// <summary>
    /// 玩家出牌显示的UI
    /// </summary>
    /// <param name="canClick">可以按下</param>
    private void RoundModel_PlayerHandler(bool canClick)
    {
        InteractionView.ActiveDealAndPass(canClick);
    }

    /// <summary>
    /// 玩家出牌
    /// </summary>
    private void OnPlayClick()
    {
        dispatcher.Dispatch(ViewEvent.RequestPlay);
    }
    /// <summary>
    /// 出牌成功
    /// </summary>
    /// <param name="payload"></param>
    private void OnSuccessedPlay()
    {
        InteractionView.DeactiveAll();

    }
    /// <summary>
    /// 不出牌
    /// </summary>
    private void OnPassClick()
    {
        InteractionView.DeactiveAll();
        dispatcher.Dispatch(CommandEvent.PassCard);
    }


    private void OnRestartGame()
    {
        InteractionView.DeactiveAll();
        InteractionView.AvtivePlay();
    }

}
