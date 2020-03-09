using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject endText;
    [SerializeField]
    private Sprite back;

    public Sprite[] photos;

    public List<Sprite> photosList = new List<Sprite>();

    public List<Button> buttons = new List<Button>();


    private bool first, second;
    private int firstIndex, secondIndex;
    private string firstPhoto, secondPhoto;
    private int correct = 0;
    //public Text end;

    private int tries;

    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Started");
        FillList();
        AddListeners();
        AddImages();
        Randomize(photosList);
    }

    public void FillList()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("BotonMemorama");
        Debug.Log("Size: " + array.Length);
        for(int i = 0; i < array.Length; i++)
        {
            Debug.Log("added");
            buttons.Add(array[i].GetComponent<Button>());
            buttons[i].image.sprite = back;
        }
    }

    public void AddImages()
    {
        int numberButtons = buttons.Count;
        int a = 0;

        for(int i = 0; i < numberButtons; i++)
        {
            if(a == numberButtons / 2)
            {
                a = 0;
            }
            photosList.Add(photos[a]);
            a++;
        }
    }

    public void AddListeners()
    {
        foreach(Button btn in buttons)
        {
            btn.onClick.AddListener(() => Selection());
        }
    }

    public void Selection()
    {
        //string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (!first)
        {
            first = true;
            firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstPhoto = photosList[firstIndex].name;
            buttons[firstIndex].image.sprite = photosList[firstIndex];
        }
        else if (!second)
        {
            second = true;
            secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondPhoto = photosList[secondIndex].name;
            buttons[secondIndex].image.sprite = photosList[secondIndex];
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);
        if (firstPhoto == secondPhoto)
        {
            Debug.Log("Match");
            yield return new WaitForSeconds(0.5f);
            buttons[firstIndex].interactable = false;
            buttons[secondIndex].interactable = false;

            buttons[firstIndex].image.color = new Color(0,0,0,0);
            buttons[secondIndex].image.color = new Color(0, 0, 0, 0);
            CheckEnd();
        }
        else
        {
            Debug.Log("No Match");
            buttons[firstIndex].image.sprite = back;
            buttons[secondIndex].image.sprite = back;
        }
        yield return new WaitForSeconds(0.5f);
        first = second = false;
    }

    public void CheckEnd()
    {
        correct++;
        if(correct == 8)
        {
            Debug.Log("Finished");
            endText.GetComponent<Text>().color = Color.white;
        }
    }

    public void Randomize(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite tmp = list[i];
            int random = Random.Range(i, list.Count);
            list[i] = list[random];
            list[random] = tmp;
        }
    }
}
