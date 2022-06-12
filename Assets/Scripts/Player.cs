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

    void Start()
    {
        currentHealth = Health;
        UpSprite.SetActive(true);
        DownSprite.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(DelayedBongoAnimation(AnimationDelay));
            //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Targetable"))
            //{
            //    if (obj.transform.position.x > MIN_HITTABLE && obj.transform.position.x < MAX_HITTABLE)
            //    {
            //        obj.GetComponent<ITargetable>().Hit();
            //    }
            //}
        }
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
        if(Score % 100 == 0)
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
        if(Score <= 0)
        {
            GameOver();
        }
    }
}
