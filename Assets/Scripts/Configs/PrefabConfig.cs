using System.Collections.Generic;
using UnityEngine;

public class PrefabConfig
{
	private static string m_Path = "GameConfigs/Prefab";
	private static Dictionary<int, PrefabConfig> m_Dic = new Dictionary<int, PrefabConfig>();
	private static bool m_Init;	
	public int id { get; private set; }
	public string path { get; private set; }
	
	public static PrefabConfig Get(int id)
	{
		Init();
		PrefabConfig value = null;
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
			PrefabConfig config = new PrefabConfig();
			config.id = (int)Helper.ParseString(data[0], "int");
			config.path = (string)Helper.ParseString(data[1], "string");

			m_Dic[config.id] = config;
        }
	}
	
	
}