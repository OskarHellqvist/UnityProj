using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class NoteAssigner : MonoBehaviour
{
    public static GameObject notePanel;
    public static GameObject noteImage;
    public static GameObject PaperImage;
    public static GameObject PostITImage;
    public static GameObject UtilityBillImage;
    public static GameObject SpectralConverganceImage;

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

