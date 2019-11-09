﻿using OfficeOpenXml;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class MyTool
{
    private static string m_ExcelLoadPath = @"E:\StudyData\GameExcel";
    private static string m_TextSavePath = @"E:\Projects\UnityProjects\ChaoticEra\Assets\Resources\GameConfigs";
    private static string m_ScriptTemplatePath = @"E:\Projects\UnityProjects\ChaoticEra\Assets\Editor\Template.txt";
    private static string m_ScriptSavePath = @"E:\Projects\UnityProjects\ChaoticEra\Assets\Scripts\Configs";
    private static string m_VarTemplate = @"public {var_type} {var_name} { get; private set; }";
    private static string m_DataToVarTemplate = @"config.{var_name} = ({var_type})Helper.ParseString(data[{id}], ""{var_type}"");";
    
    [MenuItem("MyTool/ClearAllConfigScript")]
    public static void ClearAllScripts()
    {
        ClearData(m_ScriptSavePath);
    }

    private static void ClearData(string path)
    {
        DirectoryInfo fdir = new DirectoryInfo(path);
        FileInfo[] files = fdir.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            var f = files[i];
            f.Delete();
        }
        AssetDatabase.Refresh();
        Debug.Log("脚本清理完成");
    }

    [MenuItem("MyTool/LoadAllBaseObject")]
    public static void LoadAllBaseObject()
    {
        string textPath = m_TextSavePath + @"\PartID.txt";
        string scriptPath = m_ScriptSavePath + @"\BaseObjectList.cs";
        
        string text = File.ReadAllText(textPath);
        var lines = text.Split('\n');
        StringBuilder sb = new StringBuilder();
        sb.Append("public class BaseObjectList\n{\n\tpublic static void AddAllBaseObject()\n\t{");
        sb.Append('\n');
        string str = "\t\tResManager.Instance.PutBaseObject({1}, new {0}());";
        for (int i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            if(line == "")
            {
                continue;
            }  
            var infos = line.Split('\t');
            var temp = string.Format(str, infos[0], infos[1]);
            sb.Append(temp);
            sb.Append('\n');
        }

        sb.Append("\t}\n}");
        File.WriteAllText(scriptPath, sb.ToString());
        AssetDatabase.Refresh();
        Debug.Log("设置完成");
    }

    [MenuItem("MyTool/ExcelToTxt")]
    public static void ExcelToTxt()
    {
        DirectoryInfo fdir = new DirectoryInfo(m_ExcelLoadPath);
        FileInfo[] files = fdir.GetFiles();
        if (files.Length == 0)
        {
            return;
        }
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i];
            string suf = file.Name.Split('.')[1];
            if (suf != "xlsx")
            {
                continue;
            }
            string path = m_ExcelLoadPath + "\\" + file.Name;
            ExcelPackage package = new ExcelPackage(new FileStream(path, FileMode.Open));            
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];
            StringBuilder Content = new StringBuilder();
            for (int j = sheet.Dimension.Start.Row, k = sheet.Dimension.End.Row; j <= k; j++)
            {
                for (int m = sheet.Dimension.Start.Column, n = sheet.Dimension.End.Column; m <= n; m++)
                {
                    string str = sheet.GetValue(j, m)?.ToString();
                    if(str == null)
                    {
                        continue;
                    }
                    Content.Append(str);
                    Content.Append('\t');
                }
                Content.Append('\n');
            }
            package.Dispose();
            string txtpath = m_TextSavePath + "\\" + file.Name.Split('.')[0] +  ".txt";       
            File.WriteAllText(txtpath, Content.ToString());
            
        }
        AssetDatabase.Refresh();
        Debug.Log("脚本转换完成");
    }


    [MenuItem("MyTool/TxtToScript")]
    public static void TxtToScript()
    {
        DirectoryInfo fdir = new DirectoryInfo(m_TextSavePath);
        FileInfo[] files = fdir.GetFiles();
        if (files.Length == 0)
        {
            return;
        }
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i];
            var res = file.Name.Split('.');
            string suf = res[res.Length - 1];
            if (suf != "txt")
            {
                continue;
            }
            string path = m_TextSavePath + "\\" + file.Name;
            string resPath = "GameConfigs/" + file.Name.Split('.')[0];
            string className = file.Name.Split('.')[0] + "Config";
            string scriptPath = m_ScriptSavePath + "\\" + className + ".cs";
            string text = File.ReadAllText(path);
            var lines = text.Split('\n');
            var types = lines[0].Split('\t');
            var names = lines[1].Split('\t');
            var colCount = types.Length;
            StringBuilder varSb = new StringBuilder();
            StringBuilder dataToVarSb = new StringBuilder();
            for (int n = 0; n < colCount; n++)
            {
                string name = names[n];
                string type = types[n];
                if (name == "" || type == "")
                {
                    continue;
                }
                string varStr = m_VarTemplate.Replace("{var_type}", type).Replace("{var_name}", name);
                string dataToVarStr = m_DataToVarTemplate.Replace("{id}", n.ToString())
                                                         .Replace("{var_type}", type)
                                                         .Replace("{var_name}", name);
                varSb.Append("\t");
                varSb.Append(varStr);
                varSb.Append('\n');
                dataToVarSb.Append("\t\t\t");
                dataToVarSb.Append(dataToVarStr);
                dataToVarSb.Append('\n');
            }
            StringBuilder tempSb = new StringBuilder(File.ReadAllText(m_ScriptTemplatePath));
            tempSb.Replace("{var}", varSb.ToString());
            tempSb.Replace("{data_var}", dataToVarSb.ToString());
            tempSb.Replace("{class_name}", className);
            tempSb.Replace("{path}", resPath);
            tempSb.Replace("{key_type}", types[0]);
            tempSb.Replace("{key}", names[0]);
            File.WriteAllText(scriptPath, tempSb.ToString());
        }
        AssetDatabase.Refresh();
        Debug.Log("文本转换完成");
    }

    

}