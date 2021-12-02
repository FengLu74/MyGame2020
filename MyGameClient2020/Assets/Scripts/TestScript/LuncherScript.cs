using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MGame.Resource;
using Manager;

public class LuncherScript : MonoBehaviour
{
    public InputField field;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        Button.ButtonClickedEvent btnEvent = button.onClick;
        if (btnEvent != null)
        {
            btnEvent.RemoveAllListeners();
            btnEvent.AddListener(
                () =>
                {
                    OnClickEvent();
                }
                );
        }
        ResourcesManager.Instance.InitDataM();
        AssetBundlesManager.Instance.InitDataM();

    }
    private void OnClickEvent()
    {
        if(field.text!=null)
        {
            SceneManager.LoadScene("Battle");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
