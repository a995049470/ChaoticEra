using System.Collections.Generic;
using UnityEngine;

public class PartIDConfig
{
	private static string m_Path = "GameConfigs/PartID";
	private static Dictionary<string, PartIDConfig> m_Dic = new Dictionary<string, PartIDConfig>();
	private static bool m_Init;	
	public string part { get; private set; }
	public int value { get; private set; }
	
	public static PartIDConfig Get(string id)
	{
		Init();
		PartIDConfig value = null;
		if(m_Dic.ContainsKey(id))
        {
            value = m_Dic[id];
        }
		else
		{
			Debug.LogError(id + "   不存在");
		}	
		return value;
	}
	
	public static string[] GetKeys()
	{
		Init();
		string[] keys = new string[m_Dic.Keys.Count];
        m_Dic.Keys.CopyTo(keys, 0);
        return keys;
	}

	public static void Init()
	{
		if(m_Init)
		{
			return;
		}
		m_Init = true;
		var text = Resources.Load<TextAsset>(m_Path).text;
        var infos = text.Split('\n');
        for (int i = 2; i < infos.Length; i++)
        {
            var info = infos[i];
			if(info == "")
			{
				break;
			}
            var data = info.Split('\t');
			PartIDConfig config = new PartIDConfig();
			config.part = (string)Helper.ParseString(data[0], "string");
			config.value = (int)Helper.ParseString(data[1], "int");

			m_Dic[config.part] = config;
        }
	}
	
	
}