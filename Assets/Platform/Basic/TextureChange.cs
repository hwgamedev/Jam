using UnityEngine;
using System.Collections;

public class TextureChange : MonoBehaviour {
	public Texture[] mainTexture;
	public Shader Script_shader = Shader.Find("Transparent/Diffuse");
	// Use this for initialization
	void Start () {
		if (mainTexture.Length > 0) {
			renderer.material.SetTexture ("_MainTex", mainTexture [pick_random_texture ()]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private int pick_random_texture(){
		return Random.Range(0, this.mainTexture.Length);
	}
}
