using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public GameObject cam;
    public float parallaxEffectX = 0.5f;
    public float parallaxEffectY = 0.5f;

    [Header("Repeat area (world units)")]
    public float repeatWidth = 10f;
    public float repeatHeight = 5f;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }
     
    void FixedUpdate()
    {
        float tempX = cam.transform.position.x * (1 - parallaxEffectX);
        float distX = cam.transform.position.x * parallaxEffectX;
         
        float distY = cam.transform.position.y * parallaxEffectY;

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        // Horizontal repeat
        if (tempX > startPosX + repeatWidth) startPosX += repeatWidth;
        else if (tempX < startPosX - repeatWidth) startPosX -= repeatWidth;

        // Y movement + repeat
        if (parallaxEffectY != 1f || repeatHeight != 0f)
        {
            float tempY = cam.transform.position.y * (1 - parallaxEffectY);

            // Vertical repeat
            if (tempY > startPosY + repeatHeight) startPosY += repeatHeight;
            else if (tempY < startPosY - repeatHeight) startPosY -= repeatHeight;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wireframe rectangle representing the repeat area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(repeatWidth, repeatHeight, 0));
    }
}
