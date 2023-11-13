using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public  class GameOverCommand:EventCommand
{

    [Inject]
    public RoundModel RoundModel { get; set; }

    [Inject]
    public IntegrationModel IntegrationModel { get; set; }

   [Inject]
   public CardModel CardModel { get; set; }

    public override void Execute()
    {
        int temp = IntegrationModel.Result;

        GameOverArgs e = (GameOverArgs)evt.data;
        //更新数据
        if (e.PlayerWin)
            IntegrationModel.PlayerIntegration += temp;
        else
            IntegrationModel.PlayerIntegration -= temp;
        if(e.ComputerLeftWin)
            IntegrationModel.ComputerLeftIntegration += temp;
        else
            IntegrationModel.ComputerLeftIntegration -= temp;
        if (e.ComputerRightWin)
            IntegrationModel.ComputerRightIntegration += temp;
        else
            IntegrationModel.ComputerRightIntegration -= temp;

        RoundModel.isLandlord = e.isLandlord;
        RoundModel.isWin = e.PlayerWin;

        //存储数据
        GameData data = new GameData()
        {
             computerLeftIntegration = IntegrationModel.ComputerLeftIntegration,
             computerRightIntegration = IntegrationModel.ComputerRightIntegration,
             playerIntegration = IntegrationModel.PlayerIntegration,
        };
        Tool.SaveData(data);


        //显示数据
        GameData gameDate = new GameData()
        {
            playerIntegration = IntegrationModel.PlayerIntegration,
            computerRightIntegration = IntegrationModel.ComputerRightIntegration,
            computerLeftIntegration = IntegrationModel.ComputerLeftIntegration,
        };
        dispatcher.Dispatch(ViewEvent.UpdateIntegration, gameDate);

        //添加面板
        Tool.CreateUIPanel(PanelType.GameOverPanel);

    

        //清除游戏数据
        RoundModel.InitRound();
        CardModel.InitCardLibrary();

    }
}