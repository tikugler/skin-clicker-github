using UnityEngine;

public class DialogueSelector : MonoBehaviour
{
    private bool userFirstStart;

    public DialogueTrigger firstTrigger;
    public DialogueTrigger secondTrigger;
    public DialogueTrigger thirdTrigger;
    public ContentDistributor contentDistributor;

    private bool firstTriggerTriggered = false;
    private bool secondTriggerTriggered = false;

    void Start()
    {
        CheckFirstStart();
        if (userFirstStart && !firstTriggerTriggered)
        {
            firstTrigger.TriggerDialogue();
            firstTriggerTriggered = true;
        }
    }

    void Update()
    {
        CheckWorkerBought();
    }

    private void CheckFirstStart()
    {
        userFirstStart = true;
        if (Account.points > 0)
        {
            userFirstStart = false;
            Account.SetNewPlayer(0);
        } else if (!Account.IsNewPlayer())
        {
            userFirstStart = false;
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
        // if(Account.getWorkerAmount >= 1 && secondTriggerTriggered && !thirdTriggerTriggered)
        // {
        //     thirdTrigger.TriggerDialogue();
        //     thirdTriggerTriggered = true;
        //     gameObject.SetActive(false);
        // }
    }
}
