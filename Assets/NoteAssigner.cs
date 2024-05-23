using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class NoteAssigner : MonoBehaviour
{
    [SerializeField] public static GameObject notePanel;
    [SerializeField] public static GameObject noteImage;
    [SerializeField] public static GameObject PaperImage;
    [SerializeField] public static GameObject PostITImage;
    [SerializeField] public static GameObject UtilityBillImage;
    [SerializeField] public static GameObject SpectralConverganceImage;

    void Start()
    {
        notePanel = transform.Find("NotePanel")?.gameObject;
        noteImage = notePanel?.transform.Find("NoteImage")?.gameObject;
        PaperImage = notePanel?.transform.Find("PaperImage")?.gameObject;
        PostITImage = notePanel?.transform.Find("PostITImage")?.gameObject;
        UtilityBillImage = notePanel?.transform.Find("UtilityBillImage")?.gameObject;
        SpectralConverganceImage = notePanel?.transform.Find("SpectralConverganceImage")?.gameObject;

        if (notePanel == null || noteImage == null || PaperImage == null ||
            PostITImage == null || UtilityBillImage == null || SpectralConverganceImage == null)
        {
            Debug.LogError("One or more note objects could not be found. Please check the hierarchy.");
        }
    }
}

