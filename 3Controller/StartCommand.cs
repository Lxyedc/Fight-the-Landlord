using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StartCommand : Command
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }

    [Inject]
    public  RoundModel RoundModel { get; set; }

    [Inject]
    public  CardModel CardModel { get; set; }
    /// <summary>
    /// 执行事件
    /// </summary>
    public override void Execute()
    {
        Tool.CreateUIPanel(PanelType.StartPanel);

        //初始化model
        IntegrationModel.InitIntegration();
        RoundModel.InitRound();
        CardModel.InitCardLibrary();

        //TODO：读取数据
        GetData();

        //音乐
        Sound.Instance.PlayBG(Consts.Bg);
    }

    public void GetData()
    {
        FileInfo info = new FileInfo(Consts.DataPath);
        if (info.Exists)
        {
            GameData data = Tool.GetData();
            IntegrationModel.PlayerIntegration = data.playerIntegration;
            IntegrationModel.ComputerRightIntegration = data.computerRightIntegration;
            IntegrationModel.ComputerLeftIntegration = data.computerLeftIntegration;
        }
    }
}
