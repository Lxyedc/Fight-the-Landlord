using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public  class GrabLandlordCommand : EventCommand
{
    [Inject]
    public  RoundModel RoundModel { get; set; }

    public override void Execute()
    {
        GrabAndDisGrabArgs e = (GrabAndDisGrabArgs)evt.data;
        //发地主牌
        dispatcher.Dispatch(ViewEvent.DealThreeCards,e);
        //地主开始游戏
        RoundModel.Start(e.cType);
    }
}