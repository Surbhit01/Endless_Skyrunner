using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public Text EndScoreText;
    public Text Crashed;
    public Image backgroundImg;
    //public GameObject Crashed;
    private float transition = 0.3f;
    private bool isShown = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        //Crashed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShown)
            return;
        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.3f), transition);
        
    }
    public void ToggleEndMenu(float score)
    {
        gameObject.SetActive(true);
        //Crashed.SetActive(true);
        EndScoreText.text = ((int)score).ToString();
        isShown = true;
    }

    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GameScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
