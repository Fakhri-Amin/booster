using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChecker : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        var music = FindObjectOfType<MusicPlayer>();
        if (music)
        {
            Destroy(music.gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
