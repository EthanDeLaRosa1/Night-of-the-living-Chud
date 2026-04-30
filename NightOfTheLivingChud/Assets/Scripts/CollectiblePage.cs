using UnityEngine;
using UnityEngine.UI;

public class CollectiblePage : MonoBehaviour
{
    public int pageNumber = 1;
    public Sprite pagePhoto;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.CollectPage(pageNumber, pagePhoto);
            Destroy(gameObject);
        }
    }
}