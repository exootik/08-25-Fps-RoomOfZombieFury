using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int higtScore;
    public int currentScore;
    public Text currentScoreText;
    public Text textExplicatif;

    public GameObject[] weapons;

    [SerializeField] private int currentWeaponIndex = 0;


    void Start()
    {
        Instance = this;

        SwitchWeapon(currentWeaponIndex);

        StartCoroutine(ShowTextExplicatif());
    }

    void Update()
    {
        // SCORE :
        if (currentScore > higtScore)
        {
            higtScore = currentScore;
        }

        currentScoreText.text = currentScore.ToString();

        // Switch WEAPON :
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
    }

    void SwitchWeapon(int newIndex)
    {
        weapons[currentWeaponIndex].SetActive(false);

        weapons[newIndex].SetActive(true);

        currentWeaponIndex = newIndex;

    }

    private IEnumerator ShowTextExplicatif()
    {
        yield return new WaitForSeconds(10f);

        textExplicatif.text = "";
    }
}
