using UnityEngine;
using UnityEditor;

public class CombineMeshesEditor : EditorWindow
{
    // This will hold the selected objects in the editor
    private GameObject[] selectedObjects;

    [MenuItem("Tools/Combine Meshes")]
    public static void ShowWindow()
    {
        // Open the editor window when the menu item is clicked
        GetWindow<CombineMeshesEditor>("Combine Meshes");
    }

    private void OnGUI()
    {
        // Display a button to combine meshes
        if (GUILayout.Button("Combine Selected Meshes"))
        {
            CombineMeshes();
        }
    }

    private void CombineMeshes()
    {
        // Get selected GameObjects in the scene
        selectedObjects = Selection.gameObjects;

        // Check if any objects are selected
        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected. Please select objects to combine.");
            return;
        }

        // Filter selected objects to only include meshes with MeshFilters
        MeshFilter[] meshFilters = new MeshFilter[selectedObjects.Length];
        int meshFilterCount = 0;

        // List to hold materials for the new combined mesh
        Material[] combinedMaterials = new Material[0];

        foreach (var obj in selectedObjects)
        {
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                meshFilters[meshFilterCount++] = meshFilter;
                // Collect materials from each MeshRenderer
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    Material[] materials = renderer.sharedMaterials;
                    combinedMaterials = CombineMaterialArrays(combinedMaterials, materials);
                }
            }
        }

        // Create an array to store meshes to combine
        Mesh[] meshesToCombine = new Mesh[meshFilterCount];
        Matrix4x4[] transforms = new Matrix4x4[meshFilterCount];

        for (int i = 0; i < meshFilterCount; i++)
        {
            meshesToCombine[i] = meshFilters[i].sharedMesh;
            transforms[i] = meshFilters[i].transform.localToWorldMatrix;
        }

        // Combine meshes using Mesh.CombineMeshes
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(GetCombineInstanceArray(meshesToCombine, transforms));

        // Create a new GameObject to hold the combined mesh
        GameObject combinedObject = new GameObject("CombinedMeshObject");

        // Add a MeshFilter and MeshRenderer to the new object
        MeshFilter meshFilterCombined = combinedObject.AddComponent<MeshFilter>();
        meshFilterCombined.mesh = combinedMesh;

        // Set the combined materials
        MeshRenderer meshRendererCombined = combinedObject.AddComponent<MeshRenderer>();
        meshRendererCombined.sharedMaterials = combinedMaterials;

        // Optional: If you want to remove the original objects
        foreach (var obj in selectedObjects)
        {
            DestroyImmediate(obj);
        }

        // Select the new combined object
        Selection.activeGameObject = combinedObject;
    }

    // Helper function to combine CombineInstance[] for Mesh.CombineMeshes
    private CombineInstance[] GetCombineInstanceArray(Mesh[] meshes, Matrix4x4[] transforms)
    {
        CombineInstance[] combineInstances = new CombineInstance[meshes.Length];

        for (int i = 0; i < meshes.Length; i++)
        {
            combineInstances[i] = new CombineInstance
            {
                mesh = meshes[i],
                transform = transforms[i]
            };
        }

        return combineInstances;
    }

    // Helper function to combine materials from multiple meshes
    private Material[] CombineMaterialArrays(Material[] existingMaterials, Material[] newMaterials)
    {
        int existingLength = existingMaterials.Length;
        int newLength = newMaterials.Length;

        Material[] combinedMaterials = new Material[existingLength + newLength];
        existingMaterials.CopyTo(combinedMaterials, 0);
        newMaterials.CopyTo(combinedMaterials, existingLength);

        return combinedMaterials;
    }
}
