using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Frog : MonoBehaviour
{
    [Range(2, 5)]
    public float mouthRange = 4;
    [Range(.3f, .5f)]
    public float eatRange = .4f;
    public LayerMask mask;

    Animator anim;
    Collider2D hit;
    bool done;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (done)
            return;

        hit = Physics2D.OverlapCircle(transform.position, eatRange, mask);
        if (hit != null)
        {
            anim.SetTrigger("eat");
            hit.GetComponent<Candy>().GetEaten();
            done = true;
            StartCoroutine(LoadNextScene());
        }
        else if (Physics2D.OverlapCircle(transform.position, mouthRange, mask))
            anim.SetTrigger("openMouth");
        else
            anim.SetTrigger("idle");
    }

    public void Lose()
    {
        if (done)
            return;
        anim.SetTrigger("disappoint");
        done = true;
        StartCoroutine(LoadActualScene());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, mouthRange);
        Gizmos.DrawWireSphere(transform.position, eatRange);
    }

    IEnumerator LoadActualScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3);
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(activeScene + 1);
        else
        {
            print("<color=yellow>Game Over</color>");
            Application.Quit();
        }
    }
}