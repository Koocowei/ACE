using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFTSample : MonoBehaviour
{
    [SerializeField]
    Image NFT_Image;
    [SerializeField]
    Text NFTName_Txt, Date_Txt, Rarity_Txt, Describe_Txt;

    /// <summary>
    /// 設置NFT
    /// </summary>
    /// <param name="data"></param>
    async public void SetNFT(NFTData data)
    {
        NFTName_Txt.text = data.name;
        Date_Txt.text = data.date;
        Rarity_Txt.text = $"Rarity {data.rarity}%";
        Describe_Txt.text = data.describe;
        NFT_Image.sprite = await Utils.ImageUrlToSprite(data.imgUrl) as Sprite;
    }
}
