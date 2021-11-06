using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsScipt : MonoBehaviour
{
public float Speed;

    private float _mod = 0f;
    private float _modStep = 0.01f;
    private float _textureWidth;

    void Start()
    {
        _textureWidth = gameObject.transform.localScale.x;
    }

	void Update () 
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0f, _mod * Speed);
        _mod = _mod + _modStep;
        if (_mod > _textureWidth) _mod = 0;
	}
}
 
