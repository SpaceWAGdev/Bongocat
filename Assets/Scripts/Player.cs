using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject UpSprite;
    [SerializeField] private GameObject DownSprite;
    [SerializeField, Range(0f, 1f)] private float AnimationDelay = .3f;
    [SerializeField] private int Health;
    private int currentHealth;
    [SerializeField] private int Score = 0;
    //[SerializeField] float MIN_HITTABLE = -4.6f;
    //[SerializeField] float MAX_HITTABLE = 4.4f;
    [SerializeField] TMPro.TMP_Text UIScore;
    [SerializeField] TMPro.TMP_Text UIHealth;
    public bool Hitting = false;
    [SerializeField] GameObject FX;
    [SerializeField] AudioSystem audioManager;
    [SerializeField] EnemySpawner spawner;

    void Start()
    {
        currentHealth = Health;
        UpSprite.SetActive(true);
        DownSprite.SetActive(false);
    }

    void LateUpdate()
    {
        if (Input.anyKey)
        {
            StartCoroutine(DelayedBongoAnimation(AnimationDelay));
#nullable enable
            GameObject? hitObject = spawner.Hit();
#nullable disable

            if (hitObject != null)
            {
                string tag = hitObject.tag;
                if (tag == "Enemy")
                {
                    TakeDamage(1);
                }
                else if (tag == "Target")
                {
                    OnTargetDestroyed();
                }
                else
                {
                    throw new System.Exception($"Gameobject {hitObject}.tag doesn't match the implemented hittable types");
                }
                Destroy(hitObject);
            }
        }

        // For spawner before Commit 6e6f294
        // string res = spawner.HitTarget();
        // if (res == "Enemy")
        // {
        //     TakeDamage(1);
        // }
        // else if(res == "Target")
        // {
        //     OnTargetDestroyed();
        // }

        UIScore.text = Score.ToString();
        UIHealth.text = currentHealth.ToString();
    }

    IEnumerator DelayedBongoAnimation(float duration)
    {
        FX.SetActive(true);
        Hitting = true;
        UpSprite.SetActive(false);
        DownSprite.SetActive(true);
        yield return new WaitForSeconds(duration);
        UpSprite.SetActive(true);
        DownSprite.SetActive(false);
        Hitting = false;
        FX.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        audioManager.PlayDamageSound();
        Debug.Log($"Took {amount} damage");
        if (currentHealth <= 1)
        {
            GameOver();
        }
        currentHealth -= amount;
    }

    public void OnTargetDestroyed()
    {
        Score += 10;
        if (Score % 100 == 0)
        {
            audioManager.PlaySuccessSound();
        }
    }

    public void GameOver()
    {
        audioManager.PlayGameOverSound();
        GlobalScoreManager.AddToTotalScore(Score);
        GlobalScoreManager.NewHighScore(Score);
        SceneManager.LoadSceneAsync(0);
    }
    public void RemoveScore(int amount)
    {
        Score -= amount;
        if (Score <= 0)
        {
            GameOver();
        }
    }
}
