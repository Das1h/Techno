using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("shell"))
        {
            Destroy(collision.gameObject);

            audio.PlayOneShot(audio.clip);

            Invoke("changeScene", 3.0f);
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene("Main");
    }
}
