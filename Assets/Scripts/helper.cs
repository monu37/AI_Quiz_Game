using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper : MonoBehaviour
{
    //first time
    public static void setfirsttime(int i)
    {
        PlayerPrefs.SetInt("firsttime", i);
    }
    public static int GetFirstTime()
    {
        return PlayerPrefs.GetInt("firsttime");
    }

    //total coin
    public static void settotalcoin(int totalcoin)
    {
        PlayerPrefs.SetInt("Totalcoin", totalcoin);
    }
    public static int GetTotalCoin()
    {
        return PlayerPrefs.GetInt("Totalcoin");
    }
    
    //total hints
    public static void settotalhint(int hint)
    {
        PlayerPrefs.SetInt("totalhint", hint);
    }
    public static int GetTotalHint()
    {
        return PlayerPrefs.GetInt("totalhint");
    }

    //lobby option
    public static void setoptionlockunlock(int optionno,int unlockno)
    {
        PlayerPrefs.SetInt("lobbyoptionno" + optionno, unlockno);
    }
    public static int GetOptionLockUnlock(int optionno)
    {
        return PlayerPrefs.GetInt("lobbyoptionno" + optionno);
    }

    //option level
    public static void setoptionlevel(int optionno, int levelno)
    {
        PlayerPrefs.SetInt("optionlevel" + optionno, levelno);
    }
    public static int GetOptionLevel(int optionno)
    {
        return PlayerPrefs.GetInt("optionlevel" + optionno);
    }
}
