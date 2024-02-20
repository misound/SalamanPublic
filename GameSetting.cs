using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSetting
{
    public static bool AudioReady = false;

    public static AudioMgr BGMAudio;
    public static AudioMgr SEAudio;

    public static float Playerposx;
    public static float Playerposy;

    public static Vector3 Playerpos;

    public static int PlayerHP;
    /// <summary>
    /// 回復道具
    /// </summary>
    public static int Poka;
    /// <summary>
    /// 回復道具最大數量，通常為2
    /// </summary>
    public static readonly int MaxPoka = 2;

    public static IList<Itemdata> DList;
    public static IList<AtkWData> WList;

    /// <summary>
    /// 未來做可選關卡用的，但規模沒做大就算了
    /// </summary>
    public static string Level;

    /// <summary>
    /// 正在掉落的緩衝狀態，攝影機或聲音用的
    /// </summary>
    public static bool Falling = false;
    /// <summary>
    /// 掉落結束的狀態，會用到重載
    /// </summary>
    public static bool Falled = false;
    
    public static void Save() 
    {
        PlayerPrefs.SetInt("PlayerHP", PlayerHP);
        PlayerPrefs.SetInt("Poka", Poka);
        Level = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("level",Level);
        PlayerPrefs.Save();
    }
        
    /// <summary>
    /// 在遊戲一開始時載入，通常用於死亡。
    /// </summary>
    public static void Load()
    {
        string json = PlayerPrefs.GetString("data");
        string json2 = PlayerPrefs.GetString("data2");
        DList = JsonConvert.DeserializeObject<IList<Itemdata>>(json);
        WList = JsonConvert.DeserializeObject<IList<AtkWData>>(json2);
        AudioReady = bool.Parse(PlayerPrefs.GetString("AudioReady", "false"));
        Playerpos.x = PlayerPrefs.GetFloat("x");
        Playerpos.y = PlayerPrefs.GetFloat("y");
    }

    /// <summary>
    /// 載入存檔點位置和遊戲物件狀態，同時載入遊戲音量
    /// </summary>
    public static void Respawn()  //2023/2/15:做經過就能存檔的不會回血的臨時存檔點(在碰到陷阱或掉落時啟用)
    {
        string json = PlayerPrefs.GetString("data");
        string json2 = PlayerPrefs.GetString("data2");
        DList = JsonConvert.DeserializeObject<IList<Itemdata>>(json);
        WList = JsonConvert.DeserializeObject<IList<AtkWData>>(json2);
        AudioReady = bool.Parse(PlayerPrefs.GetString("AudioReady", "false"));
        Playerpos.x = PlayerPrefs.GetFloat("x");
        Playerpos.y = PlayerPrefs.GetFloat("y");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    /// <summary>
    /// 掉落，載入臨時存檔點位置和遊戲物件狀態，同時重載
    /// </summary>
    public static void FallOut()
    {
        string json = PlayerPrefs.GetString("data");
        string json2 = PlayerPrefs.GetString("data2");
        DList = JsonConvert.DeserializeObject<IList<Itemdata>>(json);
        WList = JsonConvert.DeserializeObject<IList<AtkWData>>(json2);
        Playerpos.x = PlayerPrefs.GetFloat("Tempx");
        Playerpos.y = PlayerPrefs.GetFloat("Tempy");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 臨時存檔點，內包含了紀錄目前位置和登錄了的遊戲物件狀態
    /// </summary>
    public static void TempPoint()
    {
        string json = PlayerPrefs.GetString("data");
        string json2 = PlayerPrefs.GetString("data2");
        DList = JsonConvert.DeserializeObject<IList<Itemdata>>(json);
        WList = JsonConvert.DeserializeObject<IList<AtkWData>>(json2);
        Playerpos.x = PlayerPrefs.GetFloat("Tempx");
        Playerpos.y = PlayerPrefs.GetFloat("Tempy");
        Falling = false;
        Falled = false;
    }

    /// <summary>
    /// 原本要做成Save來保存音量設置的，用了比較笨的方法
    /// </summary>
    public static void OptionSave()
    {
        PlayerPrefs.SetString("AudioReady", AudioReady.ToString());
        PlayerPrefs.SetFloat("BGMV", BGMAudio.BGM_audioSource.volume);
        PlayerPrefs.SetFloat("SEV", SEAudio.SE_audioSource.volume);
    }
}
