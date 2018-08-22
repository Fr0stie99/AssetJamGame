using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script currently draws the trajectory of a single hardcoded arc
TODO: make into a launcher*/


[RequireComponent(typeof(Rigidbody2D))]

public class TrajectoryGenerator : MonoBehaviour {

    Vector2 gravity;
    GameObject trajectoryContainer; //container for trajectories - so that we can delete the dots later
    public float BASE_FORCE, MAX_FORCE, FORCE_INCREMENT, DOT_TIME_STEP = 0.05f;
    float pushForce;
    bool launched = false;

    Vector2 launchVelocity;
    
    public int NUM_DOTS_TO_SHOW = 30;
    public GameObject trajectoryDotPrefab;

    private Rigidbody2D rb2D;
	// Use this for initialization
	void Awake () {
        rb2D = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity * rb2D.gravityScale;
        ResetPower();

	}

    /* Calculate the position of the next dot, given a specific time */
    Vector2 CalculateNextPosition(float elapsedTime)
    {
        return gravity * elapsedTime * elapsedTime * 0.5f
            + launchVelocity * elapsedTime + (Vector2) transform.position;

    }


    public void UpdateAndDrawTrajectory(Vector2 newVelocity)
    {
        DeleteTrajectory();
        SetLaunchVelocity(newVelocity);
        DrawTrajectory();
    }

    private void SetLaunchVelocity(Vector2 newVelocity)
    {
        launchVelocity = newVelocity * pushForce;
    }

    /* Draw a trajectory of a predicted path */
    public void DrawTrajectory()
    {
        trajectoryContainer = new GameObject(name: "CurrentTrajectory");
        trajectoryContainer.transform.SetParent(transform);
        for (int i = 0; i < NUM_DOTS_TO_SHOW; i++)
        {
            GameObject trajectoryDot = Instantiate(trajectoryDotPrefab);
            Vector2 dotPosition = CalculateNextPosition(DOT_TIME_STEP * i);
            trajectoryDot.transform.position = dotPosition;

            trajectoryDot.transform.SetParent(trajectoryContainer.transform);
        }
    }

    public void DeleteTrajectory()
    {
        if (trajectoryContainer != null)
            Destroy(trajectoryContainer);
    }


    //temp function TODO: move to player controller
    public void Launch()
    {
        launched = true;
        rb2D.velocity = launchVelocity;
        DeleteTrajectory();

    }

    public void IncreasePower()
    {
        if (pushForce <= MAX_FORCE)
            pushForce += Time.fixedDeltaTime * FORCE_INCREMENT;
    }

    public void ResetPower()
    {
        pushForce = BASE_FORCE;
    }

    public bool isLaunched()
    {
        return launched;
    }


}
