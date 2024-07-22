using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transfers_AnteView : MonoBehaviour
{
    [SerializeField]
    Button CloseBtn;

    [Header("Mask滑塊")]
    [SerializeField]
    GameObject MaskObj;

    [Header("質押介面")]
    [SerializeField]
    Button AnteBtn;
    [SerializeField]
    Button SumbitBtn;
    [SerializeField]
    GameObject AnteView, AnteSuccessView;

    [Header("贖回介面")]
    [SerializeField]
    Button RedeemBtn;



    private void Awake()
    {
        CloseBtn.onClick.AddListener(() =>
        {
            Destroy(this.gameObject);
        });

        AnteBtn.onClick.AddListener(() =>
        {
            
        });

        RedeemBtn.onClick.AddListener(() =>
        {


        });


        SumbitBtn.onClick.AddListener(() =>
        {
            AnteView.SetActive(!AnteView.activeSelf);
            AnteSuccessView.SetActive(!AnteSuccessView.activeSelf);
        });

    }

}
