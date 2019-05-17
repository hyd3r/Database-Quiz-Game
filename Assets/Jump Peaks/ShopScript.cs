using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    private int starAmount;
    public Text starText;
    public Toggle[] powerups;
    private bool isInit = false;

    void Start()
    {
        powerUpInit();
        updateShop();
    }
    public void powerUp1(bool pu1)
    {
        if (!isInit)
        {
            if (pu1)
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 200);
                PlayerPrefs.SetInt("PowerUp1", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 200);
                PlayerPrefs.SetInt("PowerUp1", 0);
            }
            updateShop();
        }
    }
    public void powerUp2(bool pu2)
    {
        if (!isInit)
        {
            if (pu2)
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 300);
                PlayerPrefs.SetInt("PowerUp2", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 300);
                PlayerPrefs.SetInt("PowerUp2", 0);
            }
            updateShop();
        }
    }
    public void powerUp3(bool pu3)
    {
        if (!isInit)
        {
            if (pu3)
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 500);
                PlayerPrefs.SetInt("PowerUp3", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 500);
                PlayerPrefs.SetInt("PowerUp3", 0);
            }
            updateShop();
        }
    }
    public void powerUp4(bool pu4)
    {
        if (!isInit)
        {
            if (pu4)
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 1000);
                PlayerPrefs.SetInt("PowerUp4", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 1000);
                PlayerPrefs.SetInt("PowerUp4", 0);
            }
            updateShop();
        }
    }
    public void powerUp5(bool pu5)
    {
        if (!isInit)
        {
            if (pu5)
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 100);
                PlayerPrefs.SetInt("PowerUp5", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 100);
                PlayerPrefs.SetInt("PowerUp5", 0);
            }
            updateShop();
        }
    }
    public void powerUp6(bool pu6)
    {
        if (!isInit)
        {
            if (pu6)
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 200);
                PlayerPrefs.SetInt("PowerUp6", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 200);
                PlayerPrefs.SetInt("PowerUp6", 0);
            }
            updateShop();
        }
    }
    public void powerUp7(bool pu7)
    {
        if (!isInit)
        {
            if (pu7)
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 300);
                PlayerPrefs.SetInt("PowerUp7", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 300);
                PlayerPrefs.SetInt("PowerUp7", 0);
            }
            updateShop();
        }
    }

    void Update()
    {
        if (Input.GetKey("z"))
        {
            if (Input.GetKey("x"))
            {
                if (Input.GetKeyDown("["))
                {
                    starCheat(false);
                }
                else if (Input.GetKeyDown("]"))
                {
                    starCheat(true);
                }
            }
        }
    }
    public void starCheat(bool increase)
    {
        if (!increase)
        {
            PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 100);
            updateShop();
        }
        else
        {
            PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 100);
            updateShop();
        }
    }

    private void powerUpInit()
    {
        isInit = true;
        if (PlayerPrefs.GetInt("PowerUp1", 0) == 1) powerups[0].isOn = true;
        if (PlayerPrefs.GetInt("PowerUp2", 0) == 1) powerups[1].isOn = true;
        if (PlayerPrefs.GetInt("PowerUp3", 0) == 1) powerups[2].isOn = true;
        if (PlayerPrefs.GetInt("PowerUp4", 0) == 1) powerups[3].isOn = true;
        if (PlayerPrefs.GetInt("PowerUp5", 0) == 1) powerups[4].isOn = true;
        if (PlayerPrefs.GetInt("PowerUp6", 0) == 1) powerups[5].isOn = true;
        if (PlayerPrefs.GetInt("PowerUp7", 0) == 1) powerups[6].isOn = true;
        isInit = false;
    }

    private void updateShop()
    {
        starAmount = PlayerPrefs.GetInt("Stars");
        starText.text = starAmount.ToString();
        if (starAmount < 1000)
        {
            powerups[3].interactable = false;
        }else powerups[3].interactable = true;
        if (starAmount < 500)
        {
            powerups[2].interactable = false;
        }
        else powerups[2].interactable = true;
        if (starAmount < 300)
        {
            powerups[1].interactable = false;
            powerups[6].interactable = false;
        }
        else { powerups[1].interactable = true; powerups[6].interactable = true; }
        if (starAmount < 200)
        {
            powerups[0].interactable = false;
            powerups[5].interactable = false;
        }
        else { powerups[0].interactable = true; powerups[5].interactable = true; }
        if (starAmount < 100)
        {
            powerups[4].interactable = false;
        }
        else powerups[4].interactable = true;
    }
}
