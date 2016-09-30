using UnityEngine;
using System.Collections;

public class DestructibleTerrain : MonoBehaviour {
    [SerializeField]
    Texture2D emptyTexture;

    void Start() {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        print(rend);
        print(rend.material);
        print(rend.material.mainTexture);

        // duplicate the original sprite and assign to the material
        Texture2D texture = GameObject.Instantiate(rend.sprite.texture) as Texture2D;

        Sprite sprite = Sprite.Create(texture,
                                      new Rect(0f, 0f, texture.width, texture.height),
                                      new Vector2(texture.width / 2, texture.height / 2));

        rend.sprite = sprite;

        Color[] invisible = emptyTexture.GetPixels();

        texture.SetPixels(0,0, 200, 20, invisible);

        texture.Apply();
    }
}