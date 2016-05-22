using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

    bool firstGame = true;

    public Text tutorialText;
    public bool hasSwiped = false;
    public bool hasCombined = false;
    public bool hasHitPill = false;

	
	// Update is called once per frame
	void Update () {
        if(!hasSwiped)
        {
            tutorialText.text = "Swipe to keep food up.";
        }
        else if (!hasCombined)
        {
            tutorialText.text = "Combine food to cause heartburn.";
        }
        else if (!hasHitPill)
        {
            tutorialText.text = "Pills break up food globules.";
        }
        else
        {
            tutorialText.text = string.Empty;
        }
	}
}
