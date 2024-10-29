using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] Upgrade upgrade;
    [SerializeField] int minPrice = 200;
    [SerializeField] TextMeshProUGUI showPrice;
    [SerializeField] TextMeshProUGUI showLevel;
    int currentPrice;
    // Start is called before the first frame update
    void Start()
    {
        currentPrice = minPrice;
        showPrice.text = currentPrice.ToString();
        showLevel.text = "Cấp độ: " + GlobalGameVar.Instance().upgradeDic[upgrade] + "/" + GlobalGameVar.Instance().maxUpgradeDic[upgrade];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyUpgrade() {
        GlobalGameVar game = GlobalGameVar.Instance();
        if (game.money >= currentPrice && game.upgradeDic[upgrade] < game.maxUpgradeDic[upgrade]) {
            game.UpgradeOne(upgrade);
            game.ChangeMoney(game.money - currentPrice);
            currentPrice = (int) (minPrice * Mathf.Pow(1.5f, game.upgradeDic[upgrade]));
            if (upgrade == Upgrade.Number) {
                Shop.instance.IncreaseMaxShipNumber();
            } else if (upgrade == Upgrade.Bed) {
                GameManager.instance.SetBedActive(game.upgradeDic[Upgrade.Bed]);
            }
            showPrice.text = currentPrice.ToString();
            showLevel.text = "Cấp độ: " + game.upgradeDic[upgrade] + "/" + game.maxUpgradeDic[upgrade];
        }
    }
}
