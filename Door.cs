using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private float openShapeKey = 0.0f;
    private SkinnedMeshRenderer mesh;

	// Use this for initialization
	void Start () {
        openShapeKey = 0.0f;
        mesh = GetComponent<SkinnedMeshE>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Open()
    {
        if (openShapeKey >= 100f)
        {
            CancelInvoke("Open");
        }

      
    }
}
