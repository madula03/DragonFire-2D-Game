
using UnityEngine;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    [SerializeField] private AudioClip pickupCoinSound;
    private float coin = 0;

    public TextMeshProUGUI textCoins;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Coin")
        {
            SoundManager.instance.PlaySound(pickupCoinSound);
            coin++;
            textCoins.text = coin.ToString();

            Destroy(other.gameObject);
        }
    }
}
