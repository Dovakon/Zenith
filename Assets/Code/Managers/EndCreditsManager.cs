using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCreditsManager : MonoBehaviour {

    public Transform canvas;
    public float speed;

    private float move;
    private float Xpos;
    private float Ypos;
    void Start ()
    {
        Xpos = canvas.localPosition.x;
        Ypos = canvas.localPosition.y;
    }
	
	void Update () {

        if (Input.GetButtonDown("Cancel") || move > 0.98f)
        {
            SceneManager.LoadScene(0);
        }

        move += Time.deltaTime * speed;
        canvas.localPosition = Vector2.Lerp(new Vector2(Xpos, Ypos), new Vector2(Xpos, 2610), move);

    }
}
