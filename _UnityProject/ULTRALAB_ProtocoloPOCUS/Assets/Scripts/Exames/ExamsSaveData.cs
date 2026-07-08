using UnityEngine;

public static class ExamsSaveData
{
    // Imagem atual
    public static Sprite SavedImage;
    public static BodyArea.BodyRegion? SavedExam;

    // Backup para desfazer exclusão
    public static Sprite LastDeletedImage;
    public static BodyArea.BodyRegion? LastDeletedExam;

    public static bool HasImage => SavedImage != null;

    public static bool IsDefaultUltrasoundImage;

    public static void Save(Sprite image, BodyArea.BodyRegion exam)
    {
        SavedImage = image;
        SavedExam = exam;
    }

    public static void Clear()
    {
        // Guarda backup
        LastDeletedImage = SavedImage;
        LastDeletedExam = SavedExam;

        SavedImage = null;
        SavedExam = null;
    }

    public static void UndoDelete()
    {
        SavedImage = LastDeletedImage;
        SavedExam = LastDeletedExam;

        LastDeletedImage = null;
        LastDeletedExam = null;
    }
}