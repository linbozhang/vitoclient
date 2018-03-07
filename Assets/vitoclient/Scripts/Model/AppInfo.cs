using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using BestHTTP;
using System;
using System.IO;
using System.Text;

public class AppInfo {
    public int Id;
    public string AppName;
    public string VersionId;
    public int VersionCode;
    public string Description;
    public string IconPath;
    public int UserId;
    public string UpdateDate;
    public string CreateDate;

    public AppInfo(string appname,string versionid,int versioncode,string iconpath,string description)
    {
        AppName = appname;
        VersionId = versionid;
        VersionCode = versioncode;
        IconPath = iconpath;
        Description = description;
        Id = 0;
    }
    
    public string ToJson()
    {
       return JsonConvert.SerializeObject(this);
    }

    public static void ActionSelectUser(string userName,string token,System.Action<bool,string> callback)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://localhost:8080/v1/users/name/" + userName),
            HTTPMethods.Get, (req, res) =>
            {
                if (res.StatusCode == 201)
                {
                    callback(true, res.DataAsText);
                    UnityEngine.Debug.Log("创建APP成功:" + res.DataAsText);
                }
                else
                {
                    callback(false, res.DataAsText);
                    UnityEngine.Debug.Log("创建APP失败:" + res.StatusCode + res.DataAsText);
                }
            });
        request.SetHeader("Authorization", "Bearer" + " " + token);
        request.Send();
    }

    public static void ActionModifyUser(User userData,string token)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://localhost:8080/v1/users/" + userData.Id),
            HTTPMethods.Put, (req, res) =>
            {
                if (res.StatusCode == 200 && res.DataAsText == "\"OK\"")
                {
                    UnityEngine.Debug.Log("修改用户数据成功:" + res.DataAsText);
                }
                else
                {
                    UnityEngine.Debug.Log("修改用户数据失败:" + res.StatusCode + res.DataAsText);
                }
            });
        request.AddHeader("Content-Type", "application/json");
        request.SetHeader("Authorization", "Bearer" + " " + token);
        request.RawData = Encoding.UTF8.GetBytes(userData.ToJson());
        request.Send();
    }

    public static void ActionDeleteUser(int userid,string token)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://localhost:8080/v1/users/" + userid),
            HTTPMethods.Delete, (req, res) =>
             {
                 if (res.StatusCode == 200&&res.DataAsText=="\"OK\"")
                 {
                     UnityEngine.Debug.Log("删除用户成功:" + res.DataAsText);
                 }else
                 {
                     UnityEngine.Debug.Log( "删除用户失败:"+res.StatusCode + res.DataAsText);
                 }
             });
        request.SetHeader("Authorization", "Bearer" + " " + token);
        request.Send();
    }
    public static void ActionGetAppList(string token,System.Action<bool,string>callback)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://localhost:8080/v1/app_info/app_list/"),
            HTTPMethods.Get, (req, res) =>
            {
                if (res.StatusCode == 200 || res.StatusCode == 201)
                {
                    UnityEngine.Debug.Log("获取app列表成功" + res.DataAsText);
                    callback(true,res.DataAsText);
                }
                else
                {
                    UnityEngine.Debug.Log("获取app列表失败" + res.StatusCode + res.DataAsText);
                    callback(false, res.DataAsText);
                }
            });
        request.AddHeader("Content-Type", "application/json");
        request.SetHeader("Authorization", "Bearer" + " " + token);
        request.Send();
    }
    public static void ActionAddApp(AppInfo appData,string token)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://localhost:8080/v1/app_info/"),
            HTTPMethods.Post, (req,res)=>
            {
                if(res.StatusCode==200||res.StatusCode == 201)
                {
                    UnityEngine.Debug.Log("创建APP成功"+res.DataAsText);
                }else
                {
                    UnityEngine.Debug.Log("创建APP失败：" + res.StatusCode + res.DataAsText);
                }
            });
        request.AddHeader("Content-Type", "application/json");
        request.SetHeader("Authorization", "Bearer" + " " + token);
        request.RawData = Encoding.UTF8.GetBytes(appData.ToJson());
        request.Send();
    }
}

