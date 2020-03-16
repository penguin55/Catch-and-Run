using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Cinemachine2D : MonoBehaviour {

    private Vector2 velocity;

    private GameObject player; //Object which will following

    public float smoothTimeY;
    public float smoothTimeX;

    #region Bounding Cam

        #region SizeCam
        public float right;
        public float left;
        public float up;
        public float down;
        #endregion

    public Transform rightBound;
    public Transform leftBound;
    public Transform upBound;
    public Transform downBound;
    #endregion

    
    private void Update()
    {
        Following();
    }

    //Following camera function, but it'll stop when the player is not exist 
    private void Following()
    {
        if (transform.GetComponent<Camera>().orthographic)
        {
            if (player == null) return;
            try
            {
                float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
                float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

                transform.position = new Vector3(posX, posY, -10);

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBound.position.x + left, rightBound.position.x - right), Mathf.Clamp(transform.position.y, downBound.position.y + down, upBound.position.y - up), Mathf.Clamp(-10, -10, -10));
            }
            catch (System.Exception er)
            {

            }
        }
    }

    //To set what player will be followed by camera, player dependent from the owner client, so it will not following other camera
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
