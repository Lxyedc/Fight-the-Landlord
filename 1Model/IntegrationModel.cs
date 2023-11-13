﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationModel
{
    /// <summary>
    /// 底分
    /// </summary>
    public int BasePoint;

    /// <summary>
    /// 倍数
    /// </summary>
    public int Mulitiple;

    /// <summary>
    /// 积分
    /// </summary>
    public int Result
    {
        get
        {
            return (Mulitiple * BasePoint);
        }
    }

    public int PlayerIntegration
    {
        get
        {
            return playerIntegration;
        }

        set
        {
            if (value < 0)
                playerIntegration = 0;
            else
                playerIntegration = value;
        }
    }

    public int ComputerLeftIntegration
    {
        get
        {
            return computerLeftIntegration;
        }

        set
        {
            if (value < 0)
                computerLeftIntegration = 0;
            else
                computerLeftIntegration = value;
        }
    }

    public int ComputerRightIntegration
    {
        get
        {
            return computerRightIntegration;
        }

        set
        {
            if (value < 0)
                computerRightIntegration = 0;
            else 
                computerRightIntegration = value;
        }
    }

    private int playerIntegration;

    private int computerLeftIntegration;

    private int computerRightIntegration;


    public void InitIntegration()
    {
        Mulitiple = 1;
        BasePoint = 100;
        PlayerIntegration = 2000;
        ComputerLeftIntegration = 2000;
        ComputerRightIntegration = 2000;
    }

}
