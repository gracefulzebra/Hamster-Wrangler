using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    [SerializeField] Animator ShopAni;

    public void OpenShopAnimation()
    {
        ShopAni.Play("Shop Open");
    }

    public void CloseShopAnimation()
    {
        ShopAni.Play("Shop Close");
    }
}
