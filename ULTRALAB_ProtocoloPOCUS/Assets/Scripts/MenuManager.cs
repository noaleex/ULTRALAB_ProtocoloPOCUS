using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private string tutorial;
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject SelectionCharacter;

    string character;

    public void Play()
    {
        menuInicial.SetActive(false);
        SelectionCharacter.SetActive(true);
    }

    public void Back()
    {
        menuInicial.SetActive(true);
        SelectionCharacter.SetActive(false);
        character = null;

        print("Personagem Limpo");
        PlayerPrefs.DeleteKey("Character");
        PlayerPrefs.Save();
    }

    public void ButtonMale()
    {
        character = "Masculino";
        print("Personagem Masculino selecionado");
    }

    public void ButtonFemale()
    {
        character = "Feminino";
        print("Personagem Feminino selecionado");
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(character))
        {
            print("Selecione um personagem antes de iniciar!");
            return;
        }

        PlayerPrefs.SetString("Character", character);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(tutorial);
    }
}
