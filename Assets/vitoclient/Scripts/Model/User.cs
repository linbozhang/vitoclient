using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using BestHTTP;
using System;
using System.IO;
using System.Text;
class LoginData
{
    public string message;
    public int status;
    public string moreinfo;
}
public class User {
    public int Id;
    public string UserDescription;
    public string UserIcon;
    public string UserName;
    public string UserNick;
    public string UserPwd;
    public int UserType;

    public User(string name,string pwd,int type)
    {
        UserName = name;
        UserPwd = pwd;
        UserType = type;
        Id = 0;
    }
    
    public string ToJson()
    {
       return JsonConvert.SerializeObject(this);
    }

    public static void Login(string username,string pwd,System.Action<bool,string> callback)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://localhost:8080/v1/login"),
            HTTPMethods.Post, (req,res)=>
            {
                String result = res.DataAsText;
                LoginData data = JsonConvert.DeserializeObject<LoginData>(result);
                if (data.message == "login success")
                {
                    User.CurToken = data.moreinfo;
                    UnityEngine.Debug.Log("get token success :" + User.CurToken);
                    callback(true, data.message);
                }else
                {
                    callback(false,data.message);
                }
            });
        request.AddField("Username", username);
        request.AddField("Password", pwd);
        request.Send();
    }


    public static String CurToken;
    public static void ActionSelectUser(string userName,string token,System.Action<bool,string> callback)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://localhost:8080/v1/users/name/" + userName),
            HTTPMethods.Get, (req, res) =>
            {
                if (res.StatusCode == 200 && res.DataAsText == "\"OK\"")
                {
                    callback(true, res.DataAsText);
                    UnityEngine.Debug.Log("查询用户成功:" + res.DataAsText);
                }
                else
                {
                    callback(false, res.DataAsText);
                    UnityEngine.Debug.Log("查询用户成功:" + res.StatusCode + res.DataAsText);
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
    public static void ActionAddUser(User userdata,string token)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://localhost:8080/v1/users"),
            HTTPMethods.Post, (req,res)=>
            {
                if(res.StatusCode==200||res.StatusCode == 201)
                {
                    UnityEngine.Debug.Log("创建用户成功"+res.DataAsText);
                }else
                {
                    UnityEngine.Debug.Log("失败：" + res.StatusCode + res.DataAsText);
                }
            });
        request.AddHeader("Content-Type", "application/json");
        request.SetHeader("Authorization", "Bearer" + " " + token);
        request.RawData = Encoding.UTF8.GetBytes(userdata.ToJson());
        request.Send();
    }
}

