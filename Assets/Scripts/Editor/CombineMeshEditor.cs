using UnityEngine;
using UnityEditor;

public class CombineMeshesEditor : EditorWindow
{
    private GameObject[] selectedObjects;

    [MenuItem("Tools/Combine Meshes")]
    public static void ShowWindow()
    {
        GetWindow<CombineMeshesEditor>("Combine Meshes");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Combine Selected Meshes"))
        {
            CombineMeshes();
        }
    }

    private void CombineMeshes()
    {
        selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected. Please select objects to combine.");
            return;
        }

        // List to collect mesh filters and materials
        MeshFilter[] meshFilters = new MeshFilter[selectedObjects.Length];
        MeshRenderer[] meshRenderers = new MeshRenderer[selectedObjects.Length];
        int meshCount = 0;

        // Collect meshes and corresponding materials
        foreach (var obj in selectedObjects)
        {
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                meshFilters[meshCount] = meshFilter;
                meshRenderers[meshCount] = obj.GetComponent<MeshRenderer>();
                meshCount++;
            }
        }

        // Create arrays to store meshes and materials
        Mesh[] meshesToCombine = new Mesh[meshCount];
        Matrix4x4[] transforms = new Matrix4x4[meshCount];
        Material[][] materialsForSubMeshes = new Material[meshCount][];

        // Prepare the meshes and materials
        for (int i = 0; i < meshCount; i++)
        {
            meshesToCombine[i] = meshFilters[i].sharedMesh;
            transforms[i] = meshFilters[i].transform.localToWorldMatrix;
            materialsForSubMeshes[i] = meshRenderers[i].sharedMaterials;
        }

        // Create the combined mesh
        Mesh combinedMesh = new Mesh();
        CombineInstance[] combineInstances = new CombineInstance[meshCount];

        // Flatten meshes into a single combined mesh
        int subMeshCount = 0;
        for (int i = 0; i < meshCount; i++)
        {
            combineInstances[i] = new CombineInstance
            {
                mesh = meshesToCombine[i],
                transform = transforms[i]
            };
            subMeshCount += meshesToCombine[i].subMeshCount;
        }

        combinedMesh.CombineMeshes(combineInstances, true, false);

        // Create the new GameObject
        GameObject combinedObject = new GameObject("CombinedMeshObject");

        // Add MeshFilter and MeshRenderer
        MeshFilter combinedMeshFilter = combinedObject.AddComponent<MeshFilter>();
        combinedMeshFilter.mesh = combinedMesh;

        MeshRenderer combinedMeshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // Merge materials
        Material[] mergedMaterials = new Material[subMeshCount];
        int materialIndex = 0;
        for (int i = 0; i < meshCount; i++)
        {
            Material[] meshMaterials = materialsForSubMeshes[i];
            foreach (var material in meshMaterials)
            {
                mergedMaterials[materialIndex++] = material;
            }
        }

        // Assign the merged materials to the combined mesh
        combinedMeshRenderer.sharedMaterials = mergedMaterials;

        // Optionally, remove the original objects
        foreach (var obj in selectedObjects)
        {
            DestroyImmediate(obj);
        }

        // Select the new combined object
        Selection.activeGameObject = combinedObject;
    }
}
