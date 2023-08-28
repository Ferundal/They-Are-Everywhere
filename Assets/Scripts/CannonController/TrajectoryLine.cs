using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField, Min(3)] private int lineSegments = 60;
    [SerializeField, Min(1)] private float timeOfTheFlight = 3;

    public void ShowTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, LineRenderer lineRenderer)
    {
        float timeStep = timeOfTheFlight / lineSegments;

        Vector3[] lineRendererPoints = CalculateTrajectory(startPoint, startVelocity, timeStep);
        lineRenderer.positionCount = lineSegments;
        lineRenderer.SetPositions(lineRendererPoints);
    }

    private Vector3[] CalculateTrajectory(Vector3 startPoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRendererPoints = new Vector3[lineSegments];

        lineRendererPoints[0] = startPoint;

        for (int i = 1; i < lineSegments; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressWithNoGravityApplied = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPos = startPoint + progressWithNoGravityApplied - gravityOffset;
            lineRendererPoints[i] = newPos;
        }

        return lineRendererPoints;
    }
}
