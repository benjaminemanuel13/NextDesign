using Colors.Assistant.Plugin.Models;
using System.Runtime.CompilerServices;

namespace Colors.Common.Models
{
    public static class Orchestrator
    {
        public static event EventHandler<EventArguments.ProjectFormToFrontEventArgs>? ProjectFormToFrontEvent;

        public static event EventHandler<EventArguments.SelectionMoveEventArgs>? SelectionMoveEvent;

        public static void ProjectFormToFront(string formName)
        {
            ProjectFormToFrontEvent?.Invoke(new object(), new EventArguments.ProjectFormToFrontEventArgs(formName));
        }

        public static void MoveSelection(SelectionMove move)
        {
            SelectionMoveEvent?.Invoke(new object(), new EventArguments.SelectionMoveEventArgs(move));
        }
    }
}
