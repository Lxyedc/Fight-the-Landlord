using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Xml.Serialization;

public static class Tool  {

    static Transform uiParent;

    public static Transform UiParent
    {
        get
        {
            if (uiParent == null)
                uiParent = GameObject.Find("Canvas").transform;
            return uiParent;
        }

    }
    /// <summary>
    /// 生成Panel
    /// </summary>
    /// <param name="type">面板类型</param>
    /// <returns></returns>
    public static GameObject CreateUIPanel(PanelType type)
    {
        GameObject go = Resources.Load<GameObject>(type.ToString());
        if(go == null)
        {
            Debug.Log(type.ToString() + "不存在");
            return null;
        }
        else
        {
            GameObject panel = GameObject.Instantiate(go);
            panel.name = type.ToString();            
            panel.transform.SetParent(UiParent,false);
            return go;
        }
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="cards">要排序的牌</param>
    /// <param name="asc">升序 123</param>
    public static void Sort(List<Card> cards, bool asc)
    {
        cards.Sort(
            (Card a, Card b) =>
            {
                if (asc)
                    return a.Cardweight.CompareTo(b.Cardweight);
                else
                    return -a.Cardweight.CompareTo(b.Cardweight);

            });
    }

    /// <summary>
    /// 获取牌的大小
    /// </summary>
    /// <param name="cards">出的牌</param>
    /// <param name="cardType">出牌类型</param>
    /// <returns></returns>
    public static int GetWeight(List<Card> cards, CardType cardType) 
    {
        int totalWeight = 0;

       // 3335  3666 
       if(cardType == CardType.ThreeAndOne || cardType == CardType.ThreeAndTwo)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if(cards[i].Cardweight == cards[i + 1].Cardweight && cards[i].Cardweight == cards[i + 2].Cardweight)
                {
                    totalWeight += (int)cards[i].Cardweight;
                    totalWeight *= 3;
                    break;
                }
            }
        }//34567  678910
        else if(cardType == CardType.Straight|| cardType == CardType.DoubleStraight)
        {
            totalWeight = (int)cards[0].Cardweight;

        }
        else
        {
            for (int i = 0; i < cards.Count; i++)
            {
                totalWeight += (int)cards[i].Cardweight;

            }
        }

        return totalWeight;
    }

    /// <summary>
    /// 数据存储
    /// </summary>
    /// <param name="data"></param>
    public static void SaveData(GameData data)
    {
        Stream stream = new FileStream(Consts.DataPath, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
        XmlSerializer xml = new XmlSerializer(data.GetType());
        xml.Serialize(sw, data);

        stream.Close();
        sw.Close();

    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    public static GameData GetData()
    {
        GameData data = new GameData();
        Stream stream = new FileStream(Consts.DataPath, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(stream,true);
        XmlSerializer xml = new XmlSerializer(data.GetType());

        data = xml.Deserialize(sr) as GameData;
        stream.Close();
        sr.Close();

        return data;
    }
    
}
