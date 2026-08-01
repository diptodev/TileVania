using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    bool wasCollected=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !wasCollected)
        {
            wasCollected=true;
            AudioSource.PlayClipAtPoint(audioClip,transform.position);
            Destroy(gameObject);

        }
    } 
}
