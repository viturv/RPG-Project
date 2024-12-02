using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler: MonoBehaviour
    {

        IAction CurrentAction;

        public void StartAction(IAction Action)
        {
            if(CurrentAction == Action) return;
            if(CurrentAction != null)
            {
                CurrentAction.Cancel();
            }
            CurrentAction= Action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}