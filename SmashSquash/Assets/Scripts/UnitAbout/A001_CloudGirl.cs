using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A001_CloudGirl : A000_Default
{
    //單位初始的資料
    private string unitNameDefault = "Cloud Girl";
    private int spiritGradeDefault = 112;
    private SpiritControlStandard spiritControlDefault = SpiritControlStandard.Ap;

    private int hpDefault = 15444;
    private int atkDefault = 4251;
    private int defDefault = 2310;

    //建構子 初始化該單位
    public A001_CloudGirl()
    {
        this.unitName = unitNameDefault;
        this.spiritGrade = spiritGradeDefault;
        this.spiritControl = spiritControlDefault;

        this.hp = hpDefault;
        this.atk = atkDefault;
        this.def = defDefault;
    }
}
