using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMulitipleCommand : EventCommand
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }

    public override void Execute()
    {
       
        //1.改model
        IntegrationModel.Mulitiple *= (int)evt.data;

        //添加面板
        Tool.CreateUIPanel(PanelType.CharacterPanel);
        Tool.CreateUIPanel(PanelType.InteractionPanel);
    }

}
