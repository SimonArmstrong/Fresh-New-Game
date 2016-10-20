using UnityEngine;
using System.Collections;

public class TexturePlace : MonoBehaviour {
    public Renderer rend;

    public void Place(Vector3 from, Vector3 direction) {
        RaycastHit hit;
        if (!Physics.Raycast(from, direction, out hit))
            return;

        rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
        tex.Apply();
    }
}
