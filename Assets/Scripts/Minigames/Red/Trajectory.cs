using UnityEngine;

public class Trajectory : MonoBehaviour
{
    #region Public

    public int numDots;
	public GameObject dotsParent;
	public GameObject dotPrefab;
	public float dotSpacing;
	[Range(0.1f, 0.6f)] public float dotMinScale;
	[Range(0.1f, 0.9f)] public float dotMaxScale;

	private Transform[] dots;
	private Vector3 dotPosition;
	private float timeStamp;

    #endregion

    #region Main functions

    void Start ()
	{
        //Hide trajectory
        SetParentActive(false);
		CreateDots();
	}

    #endregion

    #region Custom Methods

    void CreateDots()
	{
		dots = new Transform[numDots];
		dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

		float scale = dotMaxScale;
		float scaleFactor = scale / numDots;

		for (int i = 0; i < numDots; i++) {
			dots[i] = Instantiate(dotPrefab, null).transform;
			dots[i].parent = dotsParent.transform;

			dots[i].localScale = Vector3.one * scale;
			if (scale > dotMinScale)
            {
                scale -= scaleFactor;
            }
		}
	}

	public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
	{
		timeStamp = dotSpacing;
		for (int i = 0; i < numDots; i++) {
			dotPosition.x = (ballPos.x + forceApplied.x * timeStamp);
			dotPosition.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;
            dotPosition.z = 0;
			
			dots[i].position = dotPosition;
			timeStamp += dotSpacing;
		}
	}

	public void SetParentActive(bool b)
	{
		dotsParent.SetActive(b);
	}

    #endregion
}
