
using UnityEngine;

public enum AttType
{
    Pow,//力量
    Ag,//敏捷
    Spd,//速度
    Root,//根骨
    Int,//悟性
    Lucky,//福源
    Charm,//魅力
    Reason,//理性
    Sen,//感性
    Courage,//胆识
    Lore,//知识
}

public class PersonData : BaseObject
{
    public string fullname;
    public int[] atts;
    public int id;
    public int sex;
    public Vector3Int pos;//z 表示地图ID
    public const int AttTypeCount = 11;
    
}
