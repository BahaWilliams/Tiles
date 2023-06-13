using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private int coinPickupAmount = 100;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddScore(coinPickupAmount);
            AudioSource.PlayClipAtPoint(coinPickupSound, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
