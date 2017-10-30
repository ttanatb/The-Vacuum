using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleGraphicsController : MonoBehaviour {
    [Range(0,1)]
    public float powerLevel; //Float to control the level of the energy bar on the rifle
    public GameObject powerTube; // the energybar on the rifle
    public int weaponType; // The current weapon type, used to index energyTypes
    public Material[] energyTypes; // An array of materials for the different EnergyColors


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Set the scale to powerlevel
        Vector3 scale = powerTube.transform.localScale;
        scale.z = Mathf.Clamp(powerLevel, 0, 1);
        powerTube.transform.localScale = scale;

        //set the material;
        powerTube.GetComponent<MeshRenderer>().material = energyTypes[weaponType];
	}
}
