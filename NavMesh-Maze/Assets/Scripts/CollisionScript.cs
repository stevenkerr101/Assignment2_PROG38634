using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionScript : MonoBehaviour
{
    public Text errorMsg;
    private Vector3 home;

    public void Start()
    {
        home = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //Scene scene = SceneManager.GetActiveScene();
            //UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
            transform.position = home;

        }
        if (collision.gameObject.CompareTag("Friendly"))
        {
            ScoreScript.scoreValue += 1;
            Destroy(collision.collider.gameObject);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndZone"))
        {
            if (ScoreScript.scoreValue < 3)
            {
                errorMsg.text = "YOU HAVE NOT COLLECTED ENOUGH AGENTS!!!!";
            }
        }

        if (other.gameObject.CompareTag("WarningZone"))
        {
            
                errorMsg.text = "Watch your step!! Its getting hot!!";
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EndZone"))
        {
            
            if (ScoreScript.scoreValue < 3)
            {

                errorMsg.text = "YOU HAVE NOT COLLECTED ENOUGH AGENTS!!!!";
            }
            if(ScoreScript.scoreValue == 3)
            {
                errorMsg.text = "CONGRATULATIONS, YOU SURVIVED AND RESCUED EVERYONE!!!";
                errorMsg.color = Color.green;
                Time.timeScale = 0;
            }

        }
        if (other.gameObject.CompareTag("WarningZone"))
        {

            errorMsg.text = "Watch your step!! Its getting hot!!";

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EndZone"))
        {

            errorMsg.text = "";
        }
        if (other.gameObject.CompareTag("WarningZone"))
        {

            errorMsg.text = "";

        }
    }
}
