using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePointer : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private GameObject player;

    private Vector3 relativeDirectionFromForwardToGoal;

    //private void Start()
    //{
    //    PointTowardsGoalRelativeToForward();
    //}

    private void Update()
    {
        PointTowardsGoalRelativeToForward();

        Debug.DrawRay(transform.position, transform.up * 10.0f, Color.red);
    }

    private void FixedUpdate()
    {
        transform.up = GoalVectorInFrameOfPlayer();
    }

    private Vector3 PointTowardsGoal()
    {
        if(playerManager.CurrentGoal != null)
        {
            return playerManager.CurrentGoal.transform.position - player.transform.position;
        }
        else
        {
            return Vector3.down;
        }
    }

    private Vector3 PointTowardsGoalRelativeToForward()
    {
        // Translate direction to player's local space
        relativeDirectionFromForwardToGoal = player.transform.InverseTransformDirection(PointTowardsGoal());

        return relativeDirectionFromForwardToGoal;
    }

    // Map check changing vector B (vector from player to goal)'s reference frame from world space to the local
    // space of the player creates the vector which points the player towards the goal relative 
    // to their forward vector specifically.

    // Assume y-axis is consistent, as player rotating about this is the major problem we are focused on 
    // solving.
    private Vector3 GoalVectorInFrameOfPlayer()
    {
        // pr
        // Vector leading directly from the player's position to the designated goal's position.
        Vector3 originalVector = PointTowardsGoal();

        // Obtain rotation value; Rotation angle about the y-axis.
        // Player's y-axis rotation.
        float theta = player.transform.localRotation.eulerAngles.y * Mathf.PI / 180;

        // p1
        // Vector from player to goal reframed in the current axes frame of the player.
        // Incorporates the rotation of the player into the vector pointing towards the goal.
        // Rotation matrix assumes rotation about the original y-axis, meaning the y-axis 
        // of the original frame and the new frame are constantly equivalent.
        Vector3 vectorInNewFrame = new Vector3(
            originalVector.x * Mathf.Cos(theta) - originalVector.z * Mathf.Sin(theta),
            originalVector.y,
            originalVector.x * Mathf.Sin(theta) + originalVector.z * Mathf.Cos(theta)
            );

        //DebugRays(theta, originalVector, vectorInNewFrame);

        return vectorInNewFrame;
    }

    private void DebugRays(float playerRotation, Vector3 vectorToGoal, Vector3 reframedVectorToGoal)
    {
        // Vector towards goal
        Debug.DrawRay(player.transform.position, vectorToGoal, Color.yellow);

        // Forward vector of player
        Debug.DrawRay(player.transform.position, player.transform.forward * 10.0f, Color.blue);

        // Unity's Invers vector
        Debug.DrawRay(player.transform.position, PointTowardsGoalRelativeToForward(), Color.green);

        // Mathed Out Reference Frame vector
        Debug.DrawRay(player.transform.position, reframedVectorToGoal, Color.red);

        Debug.Log($"Full Vector Information: \n" +
            $"Current Angle = {playerRotation}\n" +
            $"Current Goal Direction = {vectorToGoal}\n" +
            $"Inverse Unity Vector = {PointTowardsGoalRelativeToForward()}\n" +
            $"Math Vector = {reframedVectorToGoal}\n");
    }

}
