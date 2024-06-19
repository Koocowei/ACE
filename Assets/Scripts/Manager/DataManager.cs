using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using MetaMask.Unity;
using System.Numerics;
using System;

public static class DataManager
{
    [Header("IG")]
    public static string IGClientId = "1019674972416526";                                                                               //IG應用程式編號
    public static string IGSecret = "2880b1cc76d96b0aa3e9da7251170e7f";                                                                 //IG密鑰                                      
    public static string IGIUserIdAndName { get; set; }                                                                                 //IG訊息

    [Header("Line")]
    public static string LineChannelId = "2005465224";                                                                                  //Line頻道ID
    public static string LineChannelSecret = "2ce90d03cf058e20c60f1c8a421889c6";                                                        //Line密鑰
    public static string LineMail { get; set; }                                                                                         //Line信箱

    [Header("Instagram")]
    public static string InstagramChannelID = "1380953359235919";
    public static string InstagramChannelSecret = "b38352322a94cad7812394a311ad26a7";
    public static string InstagramRedirectUri = "https://kooco.github.io/ACEdemo/demo.asiapoker/index.html";

    [Header("公用")]
    public static string RedirectUri = "https://kooco.github.io/ACEdemo/demo.asiapoker/index.html";                                  //重定向Url
    public static bool IsNotFirstInLogin { get; set; }                                                                               //非首次進入登入
    public static bool IsMobilePlatform { get; set; }                                                                                //是否為移動平台
    public static bool IsDefaultBrowser { get; set; }                                                                                //是否在預設瀏覽器內
    public static bool IsInCoinbase { get; set; }                                                                                    //是否在Coinbase瀏覽器

    [Header("場景")]
    public static SceneEnum CurrScene { get; set; }                 //當前場景

    [Header("用戶訊息")]
    public static string UserWalletAddress { get; set; }            //用戶錢包地址
    public static string UserWalletBalance { get; set; }            //用戶錢包餘額
    public static string UserNickname { get; set; }                 //用戶暱稱
    public static int UserAvatar { get; set; }                      //用戶頭像
    public static int UserStamina { get; set; }                     //用戶耐力
    public static int UserOTProps { get; set; }                     //用戶加時道具數量
    public static double UserCryptoChips { get; set; }              //用戶加密貨幣籌碼
    public static double UserVCChips { get; set; }                  //用戶虛擬貨幣籌碼
    public static double UserGoldChips{ get; set; }                 //用戶Gold籌碼

    [Header("大廳")]
    public static int MaxStaminaValue = 50;                                     //最大耐力值
    public static int MinMagnification = 40;                                    //購買籌碼最小倍率
    public static int MaxMagnification = 200;                                   //購買籌碼最大倍率
    public static List<double> CryptoSmallBlindList = new List<double>          //加密貨幣桌小盲值
    {
        50, 100, 200, 400,
    };
    public static List<double> VCSmallBlindList = new List<double>              //虛擬貨幣桌小盲值
    {
        200, 400, 800, 1000,
    };

    //  每日任務資料
    public static List<QuestInfo> DailyQuestList = new List<QuestInfo>
    {
        new QuestInfo(){ QuestName = "Daily Check-in",GetPoint = 30,FinishProgress = 5,CurrentProgress = 0,GetCoin = 3000},
        new QuestInfo(){ QuestName = "Cash Bet 1000 Amount",GetPoint = 10,FinishProgress = 1000,CurrentProgress = 0,GetCoin = 1000},
        new QuestInfo(){ QuestName = "Cash Bet 2000 Amount",GetPoint = 20,FinishProgress = 2000,CurrentProgress = 0,GetCoin = 2000},
        new QuestInfo(){ QuestName = "Cash Bet 3000 Amount",GetPoint = 30,FinishProgress = 3000,CurrentProgress = 0,GetCoin = 3000},
        new QuestInfo(){ QuestName = "Cash Bet 4000 Amount",GetPoint = 40,FinishProgress = 4000,CurrentProgress = 536,GetCoin = 4000},
        new QuestInfo(){ QuestName = "Cash Bet 5000 Amount",GetPoint = 50,FinishProgress = 5000,CurrentProgress = 333,GetCoin = 5000},
        new QuestInfo(){ QuestName = "Cash Bet 6000 Amount",GetPoint = 60,FinishProgress = 6000,CurrentProgress = 666,GetCoin = 6000},
        new QuestInfo(){ QuestName = "Cash Bet 7000 Amount",GetPoint = 70,FinishProgress = 7000,CurrentProgress = 999,GetCoin = 7000}
    };
    //  每周任務資料
    public static List<QuestInfo> WeekQuestList = new List<QuestInfo>
    {
        new QuestInfo(){ QuestName = "Weekly Check-in",GetPoint = 100,FinishProgress = 7,CurrentProgress = 0,GetCoin = 50000},
        new QuestInfo(){ QuestName = "Cash Bet 1000 Amount",GetPoint = 100,FinishProgress = 1000,CurrentProgress = 0,GetCoin = 5000},
        new QuestInfo(){ QuestName = "Cash Bet 2000 Amount",GetPoint = 200,FinishProgress = 2000,CurrentProgress = 0,GetCoin = 6000},
        new QuestInfo(){ QuestName = "Cash Bet 3000 Amount",GetPoint = 300,FinishProgress = 3000,CurrentProgress = 0,GetCoin = 7000},
        new QuestInfo(){ QuestName = "Cash Bet 4000 Amount",GetPoint = 400,FinishProgress = 4000,CurrentProgress = 0,GetCoin = 8000},
        new QuestInfo(){ QuestName = "Cash Bet 5000 Amount",GetPoint = 500,FinishProgress = 5000,CurrentProgress = 0,GetCoin = 9000},
        new QuestInfo(){ QuestName = "Cash Bet 6000 Amount",GetPoint = 600,FinishProgress = 6000,CurrentProgress = 0,GetCoin = 10000},
    };

}
