using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterMatChange : MonoBehaviour
{
    [Header("Hamster Prefab Parts")]
    [SerializeField] private Renderer hamsterBody;
    [SerializeField] private Renderer hamsterHead;
    [SerializeField] private Renderer hamsterLArm;
    [SerializeField] private Renderer hamsterRArm;
    [SerializeField] private Renderer hamsterLLeg;
    [SerializeField] private Renderer hamsterRLeg;

    [Header("Hamster Damage State 1 Mats")]
    [SerializeField] private Material hamsterBodyMat1;
    [SerializeField] private Material hamsterHeadMat1;
    [SerializeField] private Material hamsterLArmMat1;
    [SerializeField] private Material hamsterRArmMat1;
    [SerializeField] private Material hamsterLLegMat1;
    [SerializeField] private Material hamsterRLegMat1;

    [Header("Hamster Damage State 2 Mats")]
    [SerializeField] private Material hamsterBodyMat2;
    [SerializeField] private Material hamsterHeadMat2;
    [SerializeField] private Material hamsterLArmMat2;
    [SerializeField] private Material hamsterRArmMat2;
    [SerializeField] private Material hamsterLLegMat2;
    [SerializeField] private Material hamsterRLegMat2;

    [Header("Hamster Damage State 3 Mats")]
    [SerializeField] private Material hamsterBodyMat3;
    [SerializeField] private Material hamsterHeadMat3;
    [SerializeField] private Material hamsterLArmMat3;
    [SerializeField] private Material hamsterRArmMat3;
    [SerializeField] private Material hamsterLLegMat3;
    [SerializeField] private Material hamsterRLegMat3;

    public void SetDamageState1()
    {
        hamsterBody.material = hamsterBodyMat1;
        hamsterHead.material = hamsterHeadMat1;
        hamsterLArm.material = hamsterLArmMat1;
        hamsterRArm.material = hamsterRArmMat1;
        hamsterLLeg.material = hamsterLLegMat1;
        hamsterRLeg.material = hamsterRLegMat1;
    }
    public void SetDamageState2()
    {
        hamsterBody.material = hamsterBodyMat2;
        hamsterHead.material = hamsterHeadMat2;
        hamsterLArm.material = hamsterLArmMat2;
        hamsterRArm.material = hamsterRArmMat2;
        hamsterLLeg.material = hamsterLLegMat2;
        hamsterRLeg.material = hamsterRLegMat2;
    }
    public void SetDamageState3()
    {
        hamsterBody.material = hamsterBodyMat3;
        hamsterHead.material = hamsterHeadMat3;
        hamsterLArm.material = hamsterLArmMat3;
        hamsterRArm.material = hamsterRArmMat3;
        hamsterLLeg.material = hamsterLLegMat3;
        hamsterRLeg.material = hamsterRLegMat3;
    }
}
