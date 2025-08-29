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
        public void FormToFront(string formName)
        {
            Orchestrator.FormToFront(formName);
        }

        [KernelFunction]
        [Description("Moves Currently selected item Left, Right, Up or Down by given number.")]
        public void MoveSelection(string direction, string number)
        {
            SelectionMoveDirection dir = SelectionMoveDirection.Undefined;
            direction = direction?.ToLower();

            if (direction != null)
            {
                if (direction.Contains("left"))
                {
                    dir = SelectionMoveDirection.Left;
                }
                else if (direction.Contains("right"))
                {
                    dir = SelectionMoveDirection.Right;
                }
                else if (direction.Contains("up"))
                {
                    dir = SelectionMoveDirection.Up;
                }
                else if (direction.Contains("down"))
                {
                    dir = SelectionMoveDirection.Down;
                }
            }

            var move = new SelectionMove()
            {
                Direction = dir,
                Amount = int.TryParse(number, out int num) ? num : 1
            };

            Orchestrator.MoveSelection(move);
        }


        [KernelFunction]
        [Description("Selects the requested Form.")]
        public void SelctForm(string formName)
        {
            Orchestrator.SelectForm(formName);
        }
    }
}
