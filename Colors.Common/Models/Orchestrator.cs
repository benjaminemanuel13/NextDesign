using Colors.Assistant.Plugin.Models;
using System.Runtime.CompilerServices;

namespace Colors.Common.Models
{
    public static class Orchestrator
    {
        public static event EventHandler<EventArguments.FormToFrontEventArgs>? FormToFrontEvent;

        public static event EventHandler<EventArguments.SelectionMoveEventArgs>? SelectionMoveEvent;

        public static event EventHandler<EventArguments.SelectFormEventArgs>? SelectFormEvent;

        public static void FormToFront(string formName)
        {
            FormToFrontEvent?.Invoke(new object(), new EventArguments.FormToFrontEventArgs(formName));
        }

        public static void MoveSelection(SelectionMove move)
        {
            SelectionMoveEvent?.Invoke(new object(), new EventArguments.SelectionMoveEventArgs(move));
        }

        public static void SelectForm(string formName)
        {
            SelectFormEvent?.Invoke(new object(), new EventArguments.SelectFormEventArgs(formName));
        }
    }
}
