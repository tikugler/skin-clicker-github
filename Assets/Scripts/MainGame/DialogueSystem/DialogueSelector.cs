using UnityEngine;
using UnityEngine.UI;

public class DialogueSelector : MonoBehaviour
{
    private bool userFirstStart;

    public DialogueTrigger firstTrigger;
    public DialogueTrigger secondTrigger;
    public DialogueTrigger thirdTrigger;
    public GameObject userRegistrationPanel;

    private bool firstTriggerTriggered = false;
    private bool secondTriggerTriggered = false;
    private bool thirdTriggerTriggered = false;

    void Start()
    {
        CheckFirstStart();
    }

    void Update()
    {
        if (userFirstStart && !firstTriggerTriggered)
        {
            firstTrigger.TriggerDialogue();
            firstTriggerTriggered = true;
        }
        CheckWorkerBought();
    }

    private void CheckFirstStart()
    {
        userFirstStart = true;
        // Points anstatt credits checken
        if (Account.credits > 0)
        {
            userFirstStart = false;
            Account.SetNewPlayer(0);
        } else if (!Account.IsNewPlayer())
        {
            userFirstStart = false;
        }

        if (!userFirstStart)
        {
            gameObject.SetActive(false);
        }
    }

    //Disable all other Buttons here
    public void CheckMainButtonClickMultipleTimes()
    {
        if (Account.credits >= 5 && firstTriggerTriggered && !secondTriggerTriggered)
        {
            secondTrigger.TriggerDialogue();
            secondTriggerTriggered = true;
        }
    }

    public void CheckWorkerBought()
    {
        if(Worker.workerAmountWorkaround >= 1 && secondTriggerTriggered && !thirdTriggerTriggered)
        {
            thirdTrigger.TriggerDialogue();
            thirdTriggerTriggered = true;
        }

        if(thirdTrigger.HasDialogueEnded() && thirdTriggerTriggered)
        {
            var canvasGameObject = GameObject.Find("Canvas");
            Button shopCloseButton = FindObjectHelper.FindObjectInParent(canvasGameObject, "ShopCloseButton").GetComponent<Button>();
            shopCloseButton.onClick.Invoke();
            userRegistrationPanel.SetActive(true);
            RegistrationManager.isRegisteringTutorial = true;
            gameObject.SetActive(false);
        }
    }
}
