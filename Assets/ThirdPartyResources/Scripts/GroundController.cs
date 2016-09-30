using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

    private Transform transform;
	private SpriteRenderer sr;
	private float widthWorld, heightWorld;
	private int widthPixel, heightPixel;
	private Color transp;

	void Start(){
        transform = GetComponent<Transform>();
		sr = GetComponent<SpriteRenderer>();
		Texture2D tex = (Texture2D) sr.sprite.texture;
		Texture2D tex_clone = (Texture2D) Instantiate(tex);
		sr.sprite = Sprite.Create(tex_clone, 
		                          new Rect(0f, 0f, tex_clone.width, tex_clone.height),
		                          new Vector2(0.5f, 0.5f), 100f);
		transp = new Color(0f, 0f, 0f, 0f);
		InitSpriteDimensions();
		//BulletController.groundController = this;
	}



	private void InitSpriteDimensions() {
		widthWorld = sr.bounds.size.x;
		heightWorld = sr.bounds.size.y;
		widthPixel = sr.sprite.texture.width;
		heightPixel = sr.sprite.texture.height;
	}

	public void DestroyGround( PolygonCollider2D cc, Transform bulletTf ){
		V2int c = World2Pixel(cc.bounds.center.x, cc.bounds.center.y);
		int r = Mathf.RoundToInt(cc.bounds.size.x*widthPixel/widthWorld);

		int x, y, px, nx, py, ny, d;
		
		for (x = 0; x <= r; x++)
		{
			d = (int)Mathf.RoundToInt(Mathf.Sqrt(r * r - x * x));
			
			for (y = 0; y <= d; y++)
			{
				px = c.x + x;
				nx = c.x - x;
				py = c.y + y;
				ny = c.y - y;

				sr.sprite.texture.SetPixel(px, py, transp);
				sr.sprite.texture.SetPixel(nx, py, transp);
   				sr.sprite.texture.SetPixel(px, ny, transp);
				sr.sprite.texture.SetPixel(nx, ny, transp);
			}
		}/*

        int explosionSize = 50;

        Color[] explosionColor = new Color[explosionSize * explosionSize];

        for (int i = 0; i < explosionColor.Length; i++) {
            explosionColor[i] = new Color(1,0,0,1);
        }

        bulletTf.parent = transform;

        V2int localPos = World2Pixel(bulletTf.position.x - (bulletTf.lossyScale.x / 2), bulletTf.position.y - (bulletTf.lossyScale.y / 2));

        sr.sprite.texture.SetPixels(localPos.x, localPos.y, explosionSize, explosionSize, explosionColor);*/

		sr.sprite.texture.Apply();
		Destroy(GetComponent<PolygonCollider2D>());
		gameObject.AddComponent<PolygonCollider2D>();
	}

	private V2int World2Pixel(float x, float y) {
		V2int v = new V2int();
		
		float dx = x-transform.position.x;
		v.x = Mathf.RoundToInt(0.5f*widthPixel+ dx*widthPixel/widthWorld);
		
		float dy = y - transform.position.y;
		v.y = Mathf.RoundToInt(0.5f * heightPixel + dy * heightPixel / heightWorld);
		
		return v;
	}

    private V2int World2Pixel(Vector2 pos) {
        V2int toReturn = new V2int();

        toReturn.x = Mathf.RoundToInt(pos.x);
        toReturn.y = Mathf.RoundToInt(pos.y);

        Vector2 lossyScale = transform.lossyScale;

        toReturn.x -= Mathf.RoundToInt(transform.lossyScale.x / 2);
        toReturn.y -= Mathf.RoundToInt(transform.lossyScale.y / 2);

        print(toReturn.x);
        print(toReturn.y);

        return toReturn;
    }
}
