using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : Item
{
    private void Awake()
    {
        InitItem();
    }

    private void InitItem()
    {
        CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();

        Init(circle);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
