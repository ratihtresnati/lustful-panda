using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIQuestLog : MonoBehaviour
{
    public GameObject questInListPrefab;
    public RectTransform listTransform;

    public RectTransform questDescription;
    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text questGoldRewardText;
    public TMP_Text questExpRewardText;
    public TMP_Text questObjectiveText;
    public RectTransform rewardsContent;

    private GameObject questLogObject;
    private Button[] questButtons;

    private QuestSystem currentQuest;
    private int previousButtonIndex;

    private void Awake() {
        questLogObject = transform.GetChild(0).gameObject;
        questButtons = new Button[0];
        QuestLog.Initialize();
        QuestLog.onQuestChange += UpdateQuests;
        UpdateQuests(new List<QuestSystem>(), new List<QuestSystem>());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
            questLogObject.SetActive(!questLogObject.activeSelf);
        if (questLogObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            questLogObject.SetActive(false);
    }

    public void UpdateQuests(List<QuestSystem> active, List<QuestSystem> completed) {
        CleanupDestroyedButtons(); // Membersihkan tombol yang telah dihancurkan
        HandleSizeChange(active.Count + completed.Count);
        UpdateQuestNames(active, completed);
        UpdateSelectedQuest();
        ShowQuestDetails(currentQuest);
    }

    private void HandleSizeChange(int newCount) {
    // Ubah ukuran listTransform sesuai dengan jumlah quest
        listTransform.sizeDelta = new Vector2(0, newCount * 80);
        
        // Periksa apakah jumlah tombol perlu diperbarui
        int oldCount = questButtons.Length;
        System.Array.Resize(ref questButtons, newCount);

        // Memastikan tombol yang ada tetap digunakan
        for (int i = 0; i < newCount; i++) {
            if (i >= oldCount) {
                // Jika tombol baru, buat tombol baru
                questButtons[i] = InitializeButton(i);
            } else {
                // Jika tombol sudah ada, hanya update teks dan status
                if (questButtons[i] != null) {
                    UpdateQuestText(questButtons[i], QuestLog.getQuestNo(i));
                }
            }
        }
    }

    private void UpdateQuestNames(List<QuestSystem> active, List<QuestSystem> completed) {
    // Update tombol-tombol yang sudah ada untuk quest aktif dan selesai
        for (int i = 0; i < active.Count; i++) {
            UpdateQuestText(questButtons[i], active[i]);
        }
        for (int i = 0; i < completed.Count; i++) {
            UpdateQuestText(questButtons[i + active.Count], completed[i], true);
        }
    }

    private void UpdateSelectedQuest() {
        if (currentQuest == null)
            return;

        // Pastikan tombol sebelumnya valid sebelum meng-highlight
        if (questButtons.Length > previousButtonIndex && questButtons[previousButtonIndex] != null) {
            HighlightQuestButton(questButtons[previousButtonIndex], false);
        }

        for (int i = 0; i < questButtons.Length; i++) {
            if (questButtons[i] != null && questButtons[i].GetComponentInChildren<TMP_Text>().text == currentQuest.questName) {
                HighlightQuestButton(questButtons[i], true);
                previousButtonIndex = i;
                return;
            }
        }
    }

   private void ShowQuestDetails(QuestSystem quest) {
        questDescription.gameObject.SetActive(quest != null);
        if (quest == null)
            return;

        questNameText.text = quest.questName;
        questDescriptionText.text = quest.questDescription;
        questGoldRewardText.text = quest.goldReward + "gp";
        questExpRewardText.text = quest.expReward + "Exp";
        questObjectiveText.text = quest.objective.ToString();
        questDescriptionText.rectTransform.sizeDelta = new Vector2(0, questDescriptionText.preferredHeight);
        rewardsContent.anchoredPosition = new Vector2(0, -50 - questDescriptionText.rectTransform.sizeDelta.y);
        questDescription.sizeDelta = new Vector2(0, questDescriptionText.rectTransform.sizeDelta.y + 300);
    }

    private Button InitializeButton(int index) {
        Button button = Instantiate(questInListPrefab, listTransform).GetComponent<Button>();
        button.image.rectTransform.sizeDelta = new Vector2(0, 80);
        button.image.rectTransform.anchoredPosition = new Vector2(0, -80 * index);
        button.onClick.AddListener(delegate { QuestPress(button); });
        return button;
    }

    private void UpdateQuestText(Button questButton, QuestSystem quest, bool isCompleted = false) {
        if (questButton == null) {
            Debug.LogWarning("Quest button is null or destroyed, skipping update.");
            return; // Jika tombol null, keluar
        }

        TMP_Text text = questButton.GetComponentInChildren<TMP_Text>();
        if (text != null) {
            text.text = quest.questName; // Perbarui nama quest pada tombol
            text.color = isCompleted ? Color.gray : GetColorFromCategory(quest.questCategory); // Update warna jika quest sudah selesai
        } else {
            Debug.LogWarning("Text component not found in quest button.");
        }
    }

    private Color GetColorFromCategory(short category) {
        return Color.black; // Anda dapat membuat logika kategori warna di sini
    }

    private void HighlightQuestButton(Button questButton, bool active) {
        if (questButton == null) {
            Debug.LogWarning("Quest button is null or destroyed, skipping highlight.");
            return; // Hindari akses ke tombol null
        }

        questButton.image.color = active ? Color.green : new Color(0, 0, 0, 0);
    }

    private void QuestPress(Button questButton) {
        if (questButton == null) {
            Debug.LogWarning("Quest button is null or destroyed, skipping quest press.");
            return; // Hindari error jika tombol null
        }

        HighlightQuestButton(questButtons[previousButtonIndex], false);
        HighlightQuestButton(questButton, true);
        previousButtonIndex = System.Array.IndexOf(questButtons, questButton);
        if (previousButtonIndex < 0) {
            Debug.LogWarning("Failed to find quest button in the array.");
            return;
        }

        currentQuest = QuestLog.getQuestNo(previousButtonIndex);
        ShowQuestDetails(currentQuest);
    }

    private void CleanupDestroyedButtons() {
        for (int i = 0; i < questButtons.Length; i++) {
            if (questButtons[i] == null) {
                Debug.LogWarning($"Button at index {i} has been destroyed, removing from array.");
                questButtons[i] = null; // Pastikan referensinya null
            }
        }
    }
}
