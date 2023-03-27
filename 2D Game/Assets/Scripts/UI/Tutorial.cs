using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int index;

    public Animator animator;

    public List<GameObject> tutorialList;

    void Start()
    {
        if (!PlayerData.instance.tutorialComplete)
        {
            ShowTutorial();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ShowTutorial()
    {
        index = 0;

        animator.Play("Show");

        tutorialList[index].SetActive(true);
    }

    public void SetTutorial(int value)
    {
        tutorialList[index].SetActive(false);

        index += value;

        tutorialList[index].SetActive(true);
    }

    public void HideTutorial()
    {
        PlayerData.instance.SetTutorialComplete();

        tutorialList[index].SetActive(false);

        animator.Play("Hide");

        StartCoroutine(HideTutorialDelay());
    }

    private IEnumerator HideTutorialDelay()
    {
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}