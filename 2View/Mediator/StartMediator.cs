using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMediator : EventMediator
{
    [Inject]
    public StartView StartView { get; set; }

    public override void OnRegister()
    {
        StartView.one.onClick.AddListener(OnOneClick);
        StartView.two.onClick.AddListener(OnTwoClick);
    }

    public override void OnRemove()
    {
        StartView.one.onClick.RemoveListener(OnOneClick);
        StartView.two.onClick.RemoveListener(OnTwoClick);
    }

    private void OnOneClick()
    {
        dispatcher.Dispatch(CommandEvent.ChangeMulitiple,1);
        //1 改model
        //2 删除面板
        Destroy(StartView.gameObject);
    }

    private void OnTwoClick()
    {
        dispatcher.Dispatch(CommandEvent.ChangeMulitiple, 2);
        Destroy(StartView.gameObject);

    }
}
