using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainSceneManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
