using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 不出牌
/// </summary>
public class PassCardCommand :Command
{

    [Inject]
    public RoundModel RoundModel{ get; set; }

    public override void Execute()
    {
        RoundModel.Turn();
        Sound.Instance.PlayEffect(Consts.PassCard[UnityEngine.Random.Range(0, 3)]);
    }
}
