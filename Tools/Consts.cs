using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consts
{
    /// <summary>
    /// 数据路径
    /// </summary>
    public static string DataPath = Application.dataPath + @"\score.xml";
    public static string Bg = "normal";
    public static string DealCard = "givecard";
    public static string DisGrab = "buqiang";
    public static string Grab = "qiangdizhu1";
    public static string Select = "select";
    public static List<string> PassCard = new List<string> { "buyao1", "buyao2", "buyao3" };
    public static List<string> PlayCard = new List<string> { "dani1", "dani2", "dani3" };
    public static List<string> Single = new List<string> { "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13",
        "1", "2", "14", "15" };
    public static List<string> Double = new List<string> { "dui3", "dui4", "dui5", "dui6", "dui7", "dui8", "dui9", "dui10",
        "dui11", "dui12", "dui13", "dui1", "dui2"};
    public static string Straight = "shunzi";
    public static string DoubleStraight = "liandui";
    public static string TripleStraight = "feiji";
    public static string Three = "sange";
    public static string ThreeAndOne = "sandaiyi";
    public static string ThreeAndTwo = "sandaiyidui";
    public static string Boom = "zhadan";
    public static string JokerBoom = "wangzha";
}


/// <summary>
/// 出牌类型
/// </summary>
public enum CardType
{
    None,
    Single,//单 1
    Double,//对儿 2
    Straight,//顺子 5 - 12
    DoubleStraight,//双顺 >= 6
    TripleStraight,//飞机 >= 6 
    Three,//三不带 3
    ThreeAndOne,//三带一 4
    ThreeAndTwo,//三代二 5
    Boom,//炸弹 4
    JokerBoom//王炸 2
}
/// <summary>
/// 牌的大小
/// </summary>
public enum Weight
{
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    LJoker
}
public enum PanelType
{
    StartPanel,
    CharacterPanel,
    InteractionPanel,
    GameOverPanel
}

public enum CommandEvent
{
    ChangeMulitiple,//加倍不加倍
    RequsetDeal,//请求发牌
    GrabLandlord,//抢地主
    PlayCard,//出牌
    PassCard,//不出牌
    GameOver,//游戏结束
    RequestUpdate,//数据更新
    UpdateGameOver//更新结算界面
}

public enum ViewEvent
{
    DealCard,//给每个人发牌 characterView
    CompleteDeal ,//发牌结束
    DealThreeCards,//发地主牌
    RequestPlay,//玩家请求出牌
    SuccessedPlay,//成功出牌
    UpdateIntegration,//更新分数
    UpdateGameOver,//更新结算界面
    RestartGame//重新开始

}

/// <summary>
/// 持有牌的角色
/// </summary>
public enum CharacterType
{
    Library,
    Player,
    ComputerRight,
    ComputerLeft,
    Desk
}

/// <summary>
/// 花色
/// </summary>
public enum Colors
{
    None,//小王 大王
    Club,//梅花
    Heart,//红桃
    Spade,//黑桃
    Square//方片
}





/// <summary>
/// 牌的身份
/// </summary>
public enum Identity
{
    Farmer,
    Landlord
}

/// <summary>
/// desk生成手牌位置
/// </summary>
public enum ShowPoint
{
    Desk,
    Player,
    ComputerRight,
    ComputerLeft
}


