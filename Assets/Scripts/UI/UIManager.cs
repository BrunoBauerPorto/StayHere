using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; 
using TMPro;
using StayHere.Items;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Data Management")]
    public List<DocumentData> collectedDocuments = new List<DocumentData>();

    [Header("Main Panels")]
    public GameObject pauseMenuContainer;    
    public GameObject pauseMainView;         
    public GameObject documentReadingView;   
    public GameObject archiveTabView;        

    [Header("Document View Elements")]
    public Image paperImage;
    public TextMeshProUGUI readableText;

    [Header("Archive View Elements")]
    public Transform archiveListContent;
    public GameObject documentButtonPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        pauseMenuContainer.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (pauseMenuContainer.activeSelf)
        {
            CloseMenuAndResume();
        }
        else
        {
            OpenMainMenuPause();
        }
    }

    public void OpenMainMenuPause()
    {
        pauseMenuContainer.SetActive(true);

        pauseMainView.SetActive(true);
        documentReadingView.SetActive(false);
        archiveTabView.SetActive(false);

        FreezeGame();
    }

    public void OpenArchiveTab()
    {
        pauseMainView.SetActive(false);
        documentReadingView.SetActive(false);
        archiveTabView.SetActive(true);

        foreach (Transform child in archiveListContent) Destroy(child.gameObject);

        foreach (DocumentData doc in collectedDocuments)
        {
            GameObject newBtn = Instantiate(documentButtonPrefab, archiveListContent);
            newBtn.GetComponentInChildren<TextMeshProUGUI>().text = doc.documentTitle;
            newBtn.GetComponent<Button>().onClick.AddListener(() => ShowDocument(doc));
        }
    }

    public void CollectAndReadDocument(DocumentData data)
    {
        if (!collectedDocuments.Contains(data)) collectedDocuments.Add(data);

        pauseMenuContainer.SetActive(true);
        pauseMainView.SetActive(false); 
        archiveTabView.SetActive(false);
        ShowDocument(data);

        FreezeGame();
    }

    public void ShowDocument(DocumentData data)
    {
        archiveTabView.SetActive(false);
        documentReadingView.SetActive(true);
        paperImage.sprite = data.documentImage;
        readableText.text = data.documentContent;
    }

    public void CloseMenuAndResume()
    {
        pauseMenuContainer.SetActive(false);
        pauseMainView.SetActive(false);
        documentReadingView.SetActive(false);
        archiveTabView.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FreezeGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToArchiveList()
    {
        documentReadingView.SetActive(false); 
        OpenArchiveTab();                     
    }
}