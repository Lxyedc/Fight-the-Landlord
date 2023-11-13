using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public  class RequestUpdateCommand:EventCommand
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }
    public override void Execute()
    {
        GameData gameDate = new GameData()
        {
            playerIntegration = IntegrationModel.PlayerIntegration,
            computerRightIntegration = IntegrationModel.ComputerRightIntegration,
            computerLeftIntegration = IntegrationModel.ComputerLeftIntegration,
        };
        dispatcher.Dispatch(ViewEvent.UpdateIntegration, gameDate);
    }
}