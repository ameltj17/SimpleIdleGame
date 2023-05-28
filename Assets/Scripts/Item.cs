using UnityEngine;

[System.Serializable]

public class Item
{
    public Sprite img;
    public string name;
    public int earnCoin;
    public int levelMin;
    public bool buyable;
    public int price;
    public bool isPurchased;

    public string getItemStatus()
    {
        this.buyable = false;
        if (this.isPurchased)
        {
            return "You already 'purr'-chased this item ^-^";
        }
        else if (this.levelMin > Player.getLevel())
        {
            return "A-paw-logies, but you have to 'meow'-ster your skills and reach the minimum level to unlock this item";
        }
        else if (this.price > Player.getCoin())
        {
            return "Oops! It seems you need to gather more coins to buy this 'meow'-velous' item";
        }
        else
        {
            this.buyable = true;
            return "";
        }
    }

    public void buyItem()
    {
        Player.addCoin(-this.price);
        Player.setCoinTransaction(-this.price);
        this.isPurchased = true;
        this.buyable = false;
    }
}
