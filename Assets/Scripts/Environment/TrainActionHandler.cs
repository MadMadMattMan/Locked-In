using UnityEngine;

public class TrainActionHandler : MonoBehaviour {

    public int selectedPath = 0;
    bool pathSelectionAvailable = false;


    public bool SelectPath(int i) {
        if (!pathSelectionAvailable)
            return false;

        selectedPath = i;
        return true;
    }
}