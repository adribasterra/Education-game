using UnityEngine;
using UnityEngine.UI;
using Utils;

public class UI_Assistant : MonoBehaviour
{
    [Header("Assistant")]
    public Text messageText;
    public AudioSource talkingAudioSource;
    public Button_UI button;

    public Transform exitPoint;
    public float movementSpeed;
    public float launchSpeed;

    private TextWriter.TextWriterSingle textWriterSingle;
    private int messageNum = 0;

    private void Awake() {
        button.onClickFunc = () => {
            if (textWriterSingle != null && textWriterSingle.IsActive()) {
                textWriterSingle.WriteAllAndDestroy();
            }
            else {
                int playingCell = SceneElementsController.PlayingCell;
                string[] messageArray;

                if (playingCell == (int)SceneElementsController.Cells.red)
                {
                    messageArray = new string[] {
                        "Hello Red cell, welcome to lunges.",
                        "This is the part of the body where the oxigen is obtained.",
                        "You have to take the oxygen where the map tells you.",
                        "Try to go fast so, the cells need you.",
                        "And BE CAREFUL, don't crash, or you'll lose oxygen.",
                        "Your adventure begins now, get ready!",
                        "Bye, and good luck!"
                    };
                }
                else if (playingCell == (int)SceneElementsController.Cells.white)
                {
                    messageArray = new string[] {
                        "Hello White Cell",
                        "You're in charge of taking care of the harming cells.",
                        "You should go where they are to get rid of them.",
                        "The map tells you where are located.",
                        "Try to go fast so they don't harm the body.",
                        "And BE CAREFUL, don't crash, you'll lose ammo.",
                        "Your adventure begins now, get ready!",
                        "Bye, and good luck!"
                    };
                }
                else if (playingCell == (int)SceneElementsController.Cells.platalet)
                {
                    messageArray = new string[] {
                        "Hello Platalet. You want to know your mission?",
                        "Platalets are responsible for plugging wounds to prevent bleeding.",
                        "But you cannot do it alone.",
                        "So you shall go there to find help, other platalets.",
                        "Find as much as you can with a limited time.",
                        "Your adventure begins now, get ready!",
                        "Bye, and good luck!"
                    };
                }
                else
                {
                    messageArray = new string[] {
                        "There must be an error.",
                        "Try coming back later on.",
                        "Surely our team will be fixing this problem.",
                        "Sorry for the inconvinience, hope to see you soon.",
                        "Bye, and thank you for your patience!"
                    };
                }

                if (messageNum < messageArray.Length)
                {
                    string message = messageArray[messageNum++];
                    StartTalkingSound();
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .02f, true, true, StopTalkingSound);
                }
                else
                {
                    //Animation of current player running to enter torrente sanguíneo
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(SceneElementsController.PlayingCell).GetComponent<PlayerMovement>().SetLobbyAnimationRun(true);
                    exitPoint.parent.GetChild(2).GetComponent<BoxCollider>().isTrigger = true;
                }
            }
        };
    }
    
    private void StartTalkingSound() {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound() {
        talkingAudioSource.Stop();
    }
}
