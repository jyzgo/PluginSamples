using UnityEngine;
using ThinkingAnalytics;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class TAExample : MonoBehaviour, IDynamicSuperProperties
{


    public GUISkin skin;
    private Vector2 scrollPosition = Vector2.zero;
    //private static Color MainColor = new Color(0, 0,0);
    private static Color MainColor = new Color(84f / 255, 116f / 255, 241f / 255);
    private static Color TextColor = new Color(153f / 255, 153f / 255, 153f / 255);
    static int Margin = 40;
    static int Height = 80;
    static float ContainerWidth = Screen.width - 2 * Margin;
    // 动态公共属性接口
    public Dictionary<string, object> GetDynamicSuperProperties()
    {
       return new Dictionary<string, object>() {
           {"DynamicProperty", DateTime.Now}
       };
    }
    void Awake()
    {
    }

    void OnGUI() {
        GUILayout.BeginArea(new Rect(Margin, Screen.height * 0.15f, Screen.width-2*Margin, Screen.height));
        scrollPosition = GUILayout.BeginScrollView(new Vector2(0, 0), GUILayout.Width(Screen.width - 2 * Margin), GUILayout.Height(Screen.height - 100));
        GUIStyle style = GUI.skin.label;
        style.fontSize = 25;
        GUILayout.Label("设置用户ID",style);

        GUIStyle buttonStyle = GUI.skin.button;
        buttonStyle.fontSize = 20;
        GUILayout.BeginHorizontal(GUI.skin.box,GUILayout.Height(Height));
        if (GUILayout.Button("设置账号ID", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.Login("TA");
        }

        GUILayout.Space(20);
        if (GUILayout.Button("设置访客ID", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.Identify("TA_Distinct1", "22e445595b0f42bd8c5fe35bc44b88d6");
            
        }
        GUILayout.Space(20);
        if (GUILayout.Button("清除账号ID", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.Logout();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.Label("上传事件", GUI.skin.label);
        GUILayout.BeginHorizontal(GUI.skin.textArea, GUILayout.Height(Height));
        if (GUILayout.Button("普通事件", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.Track("TA", "22e445595b0f42bd8c5fe35bc44b88d6");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("首次事件", GUILayout.Height(Height)))
        {
            Dictionary<string, object> properties = new Dictionary<string, object>(){
                {"KEY_STRING", "B1"},
                {"KEY_BOOL", true},
                {"KEY_NUMBER", 50.65}
            };
            TDFirstEvent firstEvent = new TDFirstEvent("DEVICE_FIRST", properties);
            ThinkingAnalyticsAPI.Track(firstEvent);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("可更新事件", GUILayout.Height(Height)))
        {
            // 示例： 上报可被更新的事件，假设事件名为 UPDATABLE_EVENT
            TDUpdatableEvent updatableEvent = new TDUpdatableEvent("UPDATABLE_EVENT",
                new Dictionary<string, object>{
                    {"status", 3},
                    
                    {"price", 100}},
                "test_event_id");
            ThinkingAnalyticsAPI.Track(updatableEvent);

        }

        GUILayout.Space(20);
        if (GUILayout.Button("可重写事件", GUILayout.Height(Height)))
        {
            TDOverWritableEvent overWritableEvent = new TDOverWritableEvent("OVERWRITABLE_EVENT",
                new Dictionary<string, object>{
                    {"status", 3},
                    {"super1",100},
                    {"price", 100}},
                "test_event_id");
            ThinkingAnalyticsAPI.Track(overWritableEvent);
        }

        GUILayout.Space(20);
        if (GUILayout.Button("记录事件时长", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.TimeEvent("TATimeEvent");
            Invoke("TrackTimeEvent", 3);

        }

        GUILayout.Space(20);
        if (GUILayout.Button("自定义事件发生时间", GUILayout.Height(Height)))
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties["proper1"] = 1;
            properties["proper2"] = "proString";
            properties["proper3"] = true;
            properties["proper4"] = DateTime.Now;
            ThinkingAnalyticsAPI.Track("TA_001", properties, DateTime.Now.AddHours(-1));
        }
        GUILayout.EndHorizontal();




        GUILayout.Space(20);
        GUILayout.Label("用户属性", GUI.skin.label);
        GUILayout.BeginHorizontal(GUI.skin.textArea, GUILayout.Height(Height));
        if (GUILayout.Button("UserSet", GUILayout.Height(Height)))
        {
            Dictionary<string, object> userProperties = new Dictionary<string, object>();
            userProperties["UserProperty1"] = 1;
            userProperties["UserProperty2"] = false;
            userProperties["UserProperty3"] = DateTime.Now;
            userProperties["UserProperty4"] = "UserStrProperty";
            ThinkingAnalyticsAPI.UserSet(userProperties,DateTime.Now.AddHours(-1));
        }

        GUILayout.Space(20);
        if (GUILayout.Button("UserSetOnce", GUILayout.Height(Height)))
        {
            Dictionary<string, object> userProperties = new Dictionary<string, object>();
            userProperties["UserProperty1"] = 1;
            userProperties["UserProperty2"] = false;
            userProperties["UserProperty3"] = DateTime.Now;
            userProperties["UserProperty4"] = "UserStrProperty";
            ThinkingAnalyticsAPI.UserSetOnce(userProperties);

        }
        GUILayout.Space(20);
        if (GUILayout.Button("UserAdd", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.UserAdd("UserCoin", 1);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("UserUnset", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.UserUnset("UserProperty1");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("UserDelete", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.UserDelete();
        }
        GUILayout.Space(20);
        if (GUILayout.Button("UserAppend", GUILayout.Height(Height)))
        {
            List<string> stringList = new List<string>();
            stringList.Add("apple");
            stringList.Add("ball");
            stringList.Add("cat");
            ThinkingAnalyticsAPI.UserAppend(
                new Dictionary<string, object>
                {
                    {"USER_LIST", stringList }
                }
            );
        }
        GUILayout.EndHorizontal();

   

        GUILayout.Space(20);
        GUILayout.Label("其他配置选项", GUI.skin.label);
        GUILayout.BeginHorizontal(GUI.skin.textArea, GUILayout.Height(Height));
        if (GUILayout.Button("获取设备ID", GUILayout.Height(Height)))
        {
            Debug.Log("设备ID为:" + ThinkingAnalyticsAPI.GetDeviceId());
        }
        GUILayout.Space(20);
        if (GUILayout.Button("暂停数据上报", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.EnableTracking(false);
        }

        GUILayout.Space(20);
        if (GUILayout.Button("继续数据上报", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.EnableTracking(true);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("停止数据上报", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.OptOutTracking();
        }
        GUILayout.Space(20);
        if (GUILayout.Button("开始数据上报", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.OptInTracking();
        }
       
        GUILayout.Space(20);
        if (GUILayout.Button("校准时间", GUILayout.Height(Height)))
        {
            //时间戳,单位毫秒 对应时间为1608782412000 2020-12-24 12:00:12
            ThinkingAnalyticsAPI.CalibrateTime(1608782412000);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.Label("设置公共属性", GUI.skin.label);
        GUILayout.BeginHorizontal(GUI.skin.textArea, GUILayout.Height(Height));
        if (GUILayout.Button("设置静态公共属性", GUILayout.Height(Height)))
        {
            Dictionary<string, object> superProperties = new Dictionary<string, object>();
            List<object> listProperties = new List<object>();
            superProperties["super1"] = 1;
            superProperties["super2"] = "superstring";
            superProperties["super3"] = false;
            superProperties["super4"] = DateTime.Now;
            listProperties.Add(2);
            listProperties.Add("superStr");
            listProperties.Add(true);
            listProperties.Add(DateTime.Now);
            superProperties["super5"] = listProperties;
            ThinkingAnalyticsAPI.SetSuperProperties(superProperties);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("更新静态公共属性", GUILayout.Height(Height)))
        {
            Dictionary<string, object> superProperties = new Dictionary<string, object>();
            superProperties["super1"] = 2;
            superProperties["super6"] = "super6";
            ThinkingAnalyticsAPI.SetSuperProperties(superProperties);
        }
       
        GUILayout.Space(20);
        if (GUILayout.Button("清空部分静态公共属性", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.UnsetSuperProperty("super1");
            ThinkingAnalyticsAPI.UnsetSuperProperty("superX");
        }

        GUILayout.Space(20);
        if (GUILayout.Button("清空所有静态公共属性", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.ClearSuperProperties();
        }

        GUILayout.Space(20);
        if (GUILayout.Button("设置动态公共属性", GUILayout.Height(Height)))
        {
            ThinkingAnalyticsAPI.SetDynamicSuperProperties(this);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void Start()
    {
        // 开启自动采集事件
        ThinkingAnalyticsAPI.EnableAutoTrack(AUTO_TRACK_EVENTS.ALL);
    }
}
