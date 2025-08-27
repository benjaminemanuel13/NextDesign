using Colors.Assistant.Plugin.Models;
using Colors.Common.Models;
using Microsoft.SemanticKernel;
using Smile_7.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Assistant.Plugin
{
    public class ColorsControlPlugin : IPlugin
    {
        [KernelFunction]
        [Description("Brings the requested Form to the forefront of the screen.")]
        public void ProjectFormToFront(string formName)
        {
            Orchestrator.ProjectFormToFront(formName);
        }

        [KernelFunction]
        [Description("Moves Currently selected item Left, Right, Up or Down by given number.")]
        public void MoveSelection(string direction, string number)
        {
            var move = new SelectionMove()
            {
                Direction = SelectionMoveDirection.Up,
                Amount = int.TryParse(number, out int num) ? num : 1
            };

            Orchestrator.MoveSelection(move);
        }
    }
}
