using Colors.Assistant.Plugin.Models;
using System.Runtime.CompilerServices;

namespace Colors.Common.Models
{
    public static class Orchestrator
    {
        public static event EventHandler<EventArguments.FormToFrontEventArgs>? ProjectFormToFrontEvent;

        public static event EventHandler<EventArguments.SelectionMoveEventArgs>? SelectionMoveEvent;

        public static void FormToFront(string formName)
        {
            ProjectFormToFrontEvent?.Invoke(new object(), new EventArguments.FormToFrontEventArgs(formName));
        }

        public static void MoveSelection(SelectionMove move)
        {
            SelectionMoveEvent?.Invoke(new object(), new EventArguments.SelectionMoveEventArgs(move));
        }
    }
}
