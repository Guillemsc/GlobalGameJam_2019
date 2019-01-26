using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] Button PlayGameButton;

    AudioSource audio_source;

    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    void OnPointerEnter()
    {

    }


}
