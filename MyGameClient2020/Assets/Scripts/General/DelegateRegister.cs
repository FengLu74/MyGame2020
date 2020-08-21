using UnityEngine;

namespace MGame.General
{
    public delegate void DelegateObj(GameObject obj);
    public delegate void DelegateBool(bool yn);
    public delegate void DelegateString(string str);
    public delegate void DelegateDoubleString(string parm1,string parm2);
    public delegate void DelegateInt(int i);
    public delegate void DelegateFloat(float f);
    public delegate void DelegateNone();

    public delegate string DelegateObj_s(GameObject obj);
    public delegate string DelegateBool_s(bool yn);
    public delegate string DelegateString_s(string str);
    public delegate string DelegateInt_s(int i);
    public delegate string DelegateFloat_s(float f);
    public delegate string DelegateNone_s();//命名后缀_s表示返回值类型首字母

    public delegate string DelegateNone_ss(out string v);//命名后缀_ss表示2个返回值类型首字母
}