using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loads : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public string animName ;
    
    public void OnClickAnimEvent()
    {
        anim1.Play(string.IsNullOrEmpty(animName) ?"move": animName);
        anim2.Play(string.IsNullOrEmpty(animName) ? "move" : animName);
    }
}
