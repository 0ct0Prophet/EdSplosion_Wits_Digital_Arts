using UnityEngine;

public class RulerFlight : MonoBehaviour
{
    public Transform rulerModel;

    private Rigidbody rb;
    private Quaternion axisCorrection = Quaternion.identity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rulerModel == null)
        {
            Debug.LogError("RulerPivot has not been assigned!");
            enabled = false;
            return;
        }

        MeshFilter[] meshes = rulerModel.GetComponentsInChildren<MeshFilter>();

        float longestLength = 0f;
        Vector3 longestDirection = Vector3.forward;

        foreach (MeshFilter meshFilter in meshes)
        {
            if (meshFilter.sharedMesh == null)
                continue;

            Vector3 size = meshFilter.sharedMesh.bounds.size;

            Vector3[] axes =
            {
                Vector3.right * size.x,
                Vector3.up * size.y,
                Vector3.forward * size.z
            };

            foreach (Vector3 axis in axes)
            {
                Vector3 worldAxis = meshFilter.transform.TransformVector(axis);

                if (worldAxis.sqrMagnitude > longestLength)
                {
                    longestLength = worldAxis.sqrMagnitude;

                    longestDirection =
                        rulerModel.InverseTransformDirection(worldAxis.normalized);
                }
            }
        }

        axisCorrection = Quaternion.FromToRotation(
            longestDirection.normalized,
            Vector3.forward
        );
    }

    void LateUpdate()
    {
        if (rb == null || rulerModel == null)
            return;

        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Quaternion flightRotation =
                Quaternion.LookRotation(rb.linearVelocity.normalized);

            rulerModel.rotation = flightRotation * axisCorrection;
        }
    }
}