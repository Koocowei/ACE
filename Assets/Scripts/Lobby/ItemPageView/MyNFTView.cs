using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Thirdweb;

public class MyNFTView : MonoBehaviour
{
    [SerializeField]
    Button Back_Btn, Direction_Btn;
    [SerializeField]
    Transform HorixontalContent, VerticalContent;
    [SerializeField]
    GameObject Horizontal_Sv, Vertical_Sv, NFTHorizontalSample, NFTVerticalSample;

    List<NFTData> NFTDataList = new();                        

    /// <summary>
    /// 顯示方向
    /// </summary>
    protected enum ShowDriection
    {
        Horizontal,
        Vertical,
    }

    /// <summary>
    /// //當前顯示方向
    /// </summary>
    protected ShowDriection currDirction;
    protected ShowDriection CurrShowDirction
    {
        get
        {
            return currDirction;
        }
        set
        {
            currDirction = value;
            Horizontal_Sv.SetActive(value == ShowDriection.Horizontal);
            Vertical_Sv.SetActive(value == ShowDriection.Vertical);

            DisplayNFT();
        }
    }

    private void Awake()
    {
        //返回
        Back_Btn.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });


        //顯示方向切換
        Direction_Btn.onClick.AddListener(() =>
        {
            ShowDriection dir = currDirction == ShowDriection.Horizontal ?
                                ShowDriection.Vertical :
                                ShowDriection.Horizontal;

            CurrShowDirction = dir;
        });
    }

    private void Start()
    {
        NFTHorizontalSample.SetActive(false);
        NFTVerticalSample.SetActive(false);

        //測試用
        NFTDataList = new List<NFTData>();
        for (int i = 0; i < 6; i++)
        {
            NFTData data = new NFTData();
            data.imgUrl = "";
            data.name = $"NFT {i}";
            data.date = $"2024-06-{i}";
            data.describe = $"NFT Describr{i}";
            data.rarity = $"Rarity {90 + i}";
            NFTDataList.Add(data);
        }

        CurrShowDirction = ShowDriection.Horizontal;
    }

    /// <summary>
    /// 獲取NFT訊息
    /// </summary>
    async private void GetNFTInfo()
    {
        //獲取所有NFT訊息
        Contract contract = ThirdwebManager.Instance.SDK.GetContract(DataManager.UserWalletAddress);
        var nftDataList = await contract.ERC1155.GetAll();

        NFTDataList = new List<NFTData>();
        foreach (var nft in nftDataList)
        {
            NFTData data = new NFTData();
            Debug.Log($"NFT Data: {data}");

            data.imgUrl = nft.metadata.image;
            data.name = nft.metadata.name;
            data.date = "";
            data.describe = nft.metadata.description;
            data.rarity = "";
            NFTDataList.Add(data);
        }

        CurrShowDirction = ShowDriection.Horizontal;
    }

    /// <summary>
    /// 顯示NFT
    /// </summary>
    private void DisplayNFT()
    {
        switch (currDirction)
        {
            //顯示橫向
            case ShowDriection.Horizontal:
                DisplayNFT(HorixontalContent, NFTHorizontalSample);
                break;

            //顯示直向
            case ShowDriection.Vertical:
                DisplayNFT(VerticalContent, NFTVerticalSample);
                break;
        }
    }

    /// <summary>
    /// 顯示NFT
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="sample"></param>
    private void DisplayNFT(Transform parent, GameObject sample)
    {
        for (int i = 1; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

        foreach (var data in NFTDataList)
        {
            NFTSample nftSample = Instantiate(sample, parent).GetComponent<NFTSample>();
            nftSample.gameObject.SetActive(true);
            nftSample.SetNFT(data);
        }
    }
}

/// <summary>
/// NFT資料
/// </summary>
public class NFTData
{
    public string imgUrl;
    public string name;
    public string tokenId;
    public string date;
    public string describe;
    public string rarity;
}