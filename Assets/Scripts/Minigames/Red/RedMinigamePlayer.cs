using UnityEngine;

public class RedMinigamePlayer : MonoBehaviour
{
    #region Private

    private Rigidbody rb;
	private CircleCollider2D col;
    private bool hasArrived = false;
    private bool lostOutOfBounds = false;

	public Vector3 BallPos { get { return transform.position; } }

    #endregion

    #region Main functions

    void Awake ()
	{
		rb = GetComponent<Rigidbody> ();
		col = GetComponent<CircleCollider2D> ();
	}
    
    #endregion

    #region Custom Methods

    public void Push (Vector3 force)
	{
		rb.AddForce(force, ForceMode.Impulse);
	}

    public void ActivateRb(bool value)
    {
        if (value)
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    public void SetActive(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public void SetHasArrived()
    {
        hasArrived = true;
    }

    public bool GetHasArrived()
    {
        return hasArrived;
    }

    public bool GetLostOutOfBounds()
    {
        return lostOutOfBounds;
    }

    public void SetLostOutOfBounds()
    {
        lostOutOfBounds = true;
    }

    #endregion
}
