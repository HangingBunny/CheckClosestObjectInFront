using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour {
    public Material holoMat;
    public Material oldMat;
    public MeshRenderer mR;
	// Use this for initialization
	void Start () {
        mR = GetComponent<MeshRenderer>();
        oldMat = mR.material;
	}
	
	
	public void ChangeMaterial ()
    {
        mR.material = holoMat;
	}

    public void ResetMaterial()
    {
        mR.material = oldMat;
    }
}
