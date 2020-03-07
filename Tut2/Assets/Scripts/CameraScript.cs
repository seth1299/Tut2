using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject target;

    public AudioSource musicSource;

    public AudioClip musicClipOne;


    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musicClipOne;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);

        if (Input.GetKey("escape"))
            Application.Quit();
    }
}