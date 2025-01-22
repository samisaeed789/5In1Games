using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public GameObject[] objectsToCombine; // Array of objects to combine into a single mesh

    void Start()
    {
        CombineMeshes();
    }

    void CombineMeshes()
    {
        // Create an array to store CombineInstance data for each mesh to combine
        CombineInstance[] combineInstances = new CombineInstance[objectsToCombine.Length];

        int i = 0;
        foreach (var obj in objectsToCombine)
        {
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                // Set up the CombineInstance with the mesh and the object's transform
                combineInstances[i].mesh = meshFilter.sharedMesh;
                combineInstances[i].transform = obj.transform.localToWorldMatrix;
                i++;
            }
        }

        // Create a new mesh to hold the combined meshes
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances);

        // Assign the combined mesh to a new MeshFilter on this object
        MeshFilter meshFilterCombined = GetComponent<MeshFilter>();
        if (meshFilterCombined == null)
        {
            meshFilterCombined = gameObject.AddComponent<MeshFilter>();
        }
        meshFilterCombined.mesh = combinedMesh;

        // Optional: If you want to merge materials too, you'll need to handle it here
        MeshRenderer meshRendererCombined = GetComponent<MeshRenderer>();
        if (meshRendererCombined == null)
        {
            meshRendererCombined = gameObject.AddComponent<MeshRenderer>();
        }

        // Combine materials based on the materials of the combined meshes
        Material[] materials = new Material[objectsToCombine.Length];
        for (int j = 0; j < objectsToCombine.Length; j++)
        {
            meshRendererCombined.materials = new Material[objectsToCombine.Length];
            meshRendererCombined.materials[j] = objectsToCombine[j].GetComponent<MeshRenderer>().sharedMaterial;
        }
    }
}
