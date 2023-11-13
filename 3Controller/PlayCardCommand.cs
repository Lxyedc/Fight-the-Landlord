using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public  class PlayCardCommand:EventCommand
{

    [Inject]
    public RoundModel RoundModel { get; set; }
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }

    public override void Execute()
    {
        PlayCardArgs e = (PlayCardArgs)evt.data;

        if (e.CharacterType == CharacterType.Player)
        {
            if (e.CardType == RoundModel.CurrentType && e.Length == RoundModel.CurrentLength&&
                e.Weight > RoundModel.CurrentWeight)
                dispatcher.Dispatch(ViewEvent.SuccessedPlay);
            else if (e.CardType == CardType.Boom && RoundModel.CurrentType != CardType.Boom)
                dispatcher.Dispatch(ViewEvent.SuccessedPlay);
            else if (e.CardType == CardType.JokerBoom)
                dispatcher.Dispatch(ViewEvent.SuccessedPlay);
            else if (e.CharacterType == RoundModel.BiggestCharacter)
                dispatcher.Dispatch(ViewEvent.SuccessedPlay);
            else
            {
                UnityEngine.Debug.Log("重新选择");
                return;
            }

        }

        //播放音效
        if(e.CharacterType != RoundModel.BiggestCharacter && e.CardType != CardType.Single &&
            e.CardType != CardType.Double)
        {
            Sound.Instance.PlayEffect(Consts.PlayCard[UnityEngine.Random.Range(0, 3)]);
        }
        else
        {
            switch (e.CardType)
            {
                case CardType.None:
                    break;
                case CardType.Single:
                    Sound.Instance.PlayEffect(Consts.Single[e.Weight]);
                    break;
                case CardType.Double:
                    Sound.Instance.PlayEffect(Consts.Double[e.Weight/2]);
                    break;
                case CardType.Straight:
                    Sound.Instance.PlayEffect(Consts.Straight);
                    break;
                case CardType.DoubleStraight:
                    Sound.Instance.PlayEffect(Consts.DoubleStraight);
                    break;
                case CardType.TripleStraight:
                    Sound.Instance.PlayEffect(Consts.TripleStraight);
                    break;
                case CardType.Three:
                    Sound.Instance.PlayEffect(Consts.Three);
                    break;
                case CardType.ThreeAndOne:
                    Sound.Instance.PlayEffect(Consts.ThreeAndOne);
                    break;
                case CardType.ThreeAndTwo:
                    Sound.Instance.PlayEffect(Consts.ThreeAndTwo);
                    break;
                case CardType.Boom:
                    Sound.Instance.PlayEffect(Consts.Boom);
                    break;
                case CardType.JokerBoom:
                    Sound.Instance.PlayEffect(Consts.JokerBoom);
                    break;
                default:
                    break;
            }
        }



        //更新数据
        RoundModel.BiggestCharacter = e.CharacterType;
        RoundModel.CurrentLength = e.Length;
        RoundModel.CurrentWeight = e.Weight;
        RoundModel.CurrentType = e.CardType;

        //积分翻倍
        if (e.CardType == CardType.Boom)
            IntegrationModel.Mulitiple *=2 ;
        else if(e.CardType == CardType.JokerBoom)
            IntegrationModel.Mulitiple *= 4;

        //换人
        RoundModel.Turn();


    }
}