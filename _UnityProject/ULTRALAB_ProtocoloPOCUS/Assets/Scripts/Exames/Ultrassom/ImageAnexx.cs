using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageAnexx : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image anexxImage;
    public bool approved;
    [SerializeField] private string uti;

    public void OnAnexx()
    {
        UpdateUI();

        AudioManager.Instance?.PlayBack();
    }

    public void UpdateUI()
    {
        if (ExamsSaveData.SavedImage == null)
            return;

        anexxImage.sprite = ExamsSaveData.SavedImage;
        anexxImage.color = Color.white;
    }

    public void OnConfirm()
    {
        switch (ExamsSaveData.SavedExam)
        {
            case BodyArea.BodyRegion.Heart:
                Debug.Log("Imagem do Coração confirmada.");
                approved = true;
                //COMEÇAR CUTSCENE
                    PauseController.SetPause(false);
                    PlayerReferences.Instance.RefreshReferences();
                    PlayerReferences.Instance.EnablePlayer();
                SceneManager.LoadScene(uti);
                break;

            case BodyArea.BodyRegion.Lung1:
                Debug.Log("Imagem do Pulmão confirmada.");
                //Reprovação, e todos abaixo também
                approved = false;
                break;

            case BodyArea.BodyRegion.Lung2:
                Debug.Log("Imagem do Pulmão confirmada.");
                approved = false;
                break;

            case BodyArea.BodyRegion.Bladder:
                Debug.Log("Imagem da Bexiga confirmada.");
                approved = false;
                break;

            case BodyArea.BodyRegion.Empty:
                Debug.Log("Imagem vazia confirmada.");
                approved = false;
                break;

            default:
                Debug.Log("Anexe uma imagem para confirmar.");
                break;
        }
    }
}