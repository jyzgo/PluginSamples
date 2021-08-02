using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitMgr : MonoBehaviour
{
    static InitMgr _current;

    public static InitMgr current
    {
        get
        {

            if (m_ShuttingDown)
            {
                return null;
            }
            if (_current == null)
            {
                var init = Resources.Load("InitMgr");
                var gb = Instantiate(init);
            }
            return _current;
        }
    }

    public const string CUR_MAX_LEVEL_KEY = "MAX_LEVEL_KEY";
    public const string CUR_LEVEL_KEY = "CUR_LEVEL_KEY";

    public void InitCall()
    {

    }


    private void Awake()
    {
        if(_current != null)
        {
            Destroy(gameObject);
            return;
        }
        _current = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }


    

    float playLevelTapTime = 0f;
    public const float INTERVALE_TAP = 1f;

    private void playNextLevel()
    {
        if (playLevelTapTime + INTERVALE_TAP > Time.time)
        {
            return;
        }

        curLevelIndex++;
        CallPlay();
    }



    int curPlayerMaxIndex = 1;
    int curLevelIndex = 1;

    float gameStartTime;
    private void CallPlay()
    {

        playLevelTapTime = Time.time;
        if (curLevelIndex >= curPlayerMaxIndex)
        {
            if (AnalyzeMgr.current != null)
            {
                AnalyzeMgr.current.OnFirstPlayNextLevel(curLevelIndex);
            }
        }
        gameStartTime = Time.time;
    }


    int _btnTouchIndex = 0;
    bool _isWinThisTime = true;

  

    void Win_Enter()
    {
        AdsMgr.current.ShowInter();

        PlayerPrefs.SetInt(CUR_LEVEL_KEY, curLevelIndex);
        PlayerPrefs.SetInt(CUR_MAX_LEVEL_KEY,curPlayerMaxIndex);
    }

    void Lose_Enter()
    {
    }

   
    static bool m_ShuttingDown = false;
    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }



    public void ToWin()
    {
        AnalyzeMgr.current.OnLevelWon(curLevelIndex, (int)Time.time);
        AdsMgr.current.ShowInter();
    }

    public void ToLose()
    {
        //when lose
        AnalyzeMgr.current.OnLevelLose(curLevelIndex, "none");
    }

    public void RestartLevel()
    {
        AdsMgr.current.ShowInter();
    }

    internal int GetCurrentLevelIndex()
    {
        return curLevelIndex;
    }



}
