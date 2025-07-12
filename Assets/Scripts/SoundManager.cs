using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton

    [Header("               Audio Source                ")]
    [SerializeField] AudioSource soundEffects;

    [Header("               Audio Clip                ")]
    public AudioClip EatFood;
    public AudioClip HitObstacle;

    private void Awake()
    {
        // Ensure only one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EatFoodSound()
    {
        soundEffects.PlayOneShot(EatFood);
    }

    public void HitObstacleSound()
    {
        soundEffects.PlayOneShot(HitObstacle);
    }
}