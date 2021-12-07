using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.ReferencePool;
using System;
using MGame.Resource;

public class loads : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public string animName ;
    public Transform BattleRoot;
    public Transform UIRoot;
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    public void OnClickAnimEvent()
    {
        //anim1.Play(string.IsNullOrEmpty(animName) ? "skill1" : animName);
        //anim2.Play(string.IsNullOrEmpty(animName) ? "move" : animName);
    }

    private void Start()
    {

        stopwatch.Start();
        //GameObject gameObject1 = AssetBundlesManager.Instance.LoadAsset<GameObject>("sp_1001006", "sp_1001006");
        //if (gameObject1 != null)
        //{
        //    Debug.Log("name = " + gameObject1.name);
        //    gameObject1 = Instantiate(gameObject1);
        //    gameObject1.transform.parent = BattleRoot;
        //    gameObject1.transform.localPosition = Vector3.zero + new Vector3(-3.81f, 0f, 0f);
        //    gameObject1.transform.localEulerAngles = Vector3.zero;
        //}
        stopwatch.Stop();
        Debug.Log(stopwatch.Elapsed.Minutes + "分" + stopwatch.ElapsedMilliseconds + "秒");
        //GameObject gameObject3 = AssetBundlesManager.Instance.LoadAsset<GameObject>("BulletExplode", "BulletExplode");
        //if (gameObject3 != null)
        //{
        //    Debug.Log("name = " + gameObject3.name);
        //    gameObject3 = Instantiate(gameObject3);
        //    gameObject3.transform.parent = BattleRoot;
        //    gameObject3.transform.localPosition = Vector3.zero;
        //    gameObject3.transform.localEulerAngles = Vector3.zero;
        //}
        stopwatch.Reset();
        stopwatch.Start();
        IEnumerator enumerator = AssetBundlesManager.Instance.LoadAssetAsyncWithCallBack<GameObject>("sp_1001003", GetObjectCallBack);
        StartCoroutine(enumerator);
        //if (gameObject1 != null)
        //{
        //    Debug.Log("name = " + gameObject1.name);
        //    gameObject1 = Instantiate(gameObject1);
        //    gameObject1.transform.parent = BattleRoot;
        //    gameObject1.transform.localPosition = Vector3.zero;
        //    gameObject1.transform.localEulerAngles = Vector3.zero;
        //}


        //GameObject gameObject2 = AssetBundlesManager.Instance.LoadAsset<GameObject>("UITestPanel", "UITestPanel");
        //if (gameObject1 != null)
        //{
        //    Debug.Log("name = " + gameObject2.name);
        //    gameObject2 = Instantiate(gameObject2);
        //    gameObject2.transform.parent = UIRoot;
        //    gameObject2.transform.localPosition = Vector3.zero;
        //    gameObject2.transform.localEulerAngles = Vector3.zero;
        //}
        ////GameObject gameObject2 = AssetBundlesManager.Instance.LoadAsset<GameObject>("sp_1001006", "sp_1001006");
        Debug.Log("dsaaaaaaaaaaaaaaaaa");
    }

    private void GetObjectCallBack(GameObject gameObject1)
    {
        if (gameObject1 != null)
        {
            Debug.Log("name = " + gameObject1.name);
            gameObject1 = Instantiate(gameObject1);
            gameObject1.transform.parent = UIRoot;
            gameObject1.transform.localPosition = Vector3.zero;
            gameObject1.transform.localEulerAngles = Vector3.zero;
        }
        stopwatch.Stop();
        Debug.Log(stopwatch.Elapsed.Minutes + "分" + stopwatch.ElapsedMilliseconds + "秒");
    }

    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    GameObject gameObject3 = AssetBundlesManager.Instance.LoadAsset<GameObject>("BulletExplode", "BulletExplode");
        //    if (gameObject3 != null)
        //    {
        //        Debug.Log("name = " + gameObject3.name);
        //        gameObject3 = Instantiate(gameObject3);
        //        gameObject3.transform.parent = BattleRoot;
        //        gameObject3.transform.localPosition = Vector3.zero;
        //        gameObject3.transform.localEulerAngles = Vector3.zero;
        //    }
        //}
    }
}

