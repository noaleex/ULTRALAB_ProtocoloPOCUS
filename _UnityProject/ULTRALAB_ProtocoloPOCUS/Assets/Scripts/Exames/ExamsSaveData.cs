using UnityEngine;

public static class ExamsSaveData
{
   // Imagem atual
    public static Sprite SavedImage;
    public static BodyArea.BodyRegion? SavedExam;

    public static bool HasImage => SavedImage != null;

    public static bool IsDefaultUltrasoundImage;

    public static void Save(Sprite image, BodyArea.BodyRegion exam)
    {
        SavedImage = image;
        SavedExam = exam;
    }
}