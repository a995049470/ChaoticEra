using System.Collections.Generic;
using UnityEngine;

public class LanguageConfig
{
	private static string m_Path = "GameConfigs/Language";
	private static Dictionary<string, LanguageConfig> m_Dic = new Dictionary<string, LanguageConfig>();
	private static bool m_Init;	
	public string id { get; private set; }
	public string value { get; private set; }
	
	public static LanguageConfig Get(string id)
	{
		Init();
		LanguageConfig value = null;
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
			LanguageConfig config = new LanguageConfig();
			config.id = (string)Helper.ParseString(data[0], "string");
			config.value = (string)Helper.ParseString(data[1], "string");

			m_Dic[config.id] = config;
        }
	}
	
	
}