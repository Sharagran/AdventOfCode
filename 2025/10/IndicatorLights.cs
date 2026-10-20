using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10
{
    public struct Logs
    {
        
    }
    internal class IndicatorLights
    {
        private readonly BitArray goal;

        public BitArray[] buttons;
        private int[] joltage;
        public IndicatorLights(string diagram)
        {
            var parts = diagram.Split(' ');

            string goal = parts[0];
            goal = goal.Trim(['[', ']']);

            var buttonStrings = parts[1..(parts.Length-1)];

            string joltageRequirement = parts[parts.Length - 1];
            joltageRequirement = joltageRequirement.Trim(['{', '}']);

            this.joltage = Array.ConvertAll(joltageRequirement.Split(","), Convert.ToInt32);

            this.goal = new BitArray(goal.Length);
            for (int i = 0; i < goal.Length; i++)
            {
                this.goal[i] = goal[i] == '#';
            }

            this.buttons = new BitArray[buttonStrings.Length];
            for (int i = 0; i < buttonStrings.Length; i++)
            {
                this.buttons[i] = new BitArray(goal.Length);

                var item = buttonStrings[i];
                var tmp = item.Trim(['(', ')']);
                var indices = Array.ConvertAll(tmp.Split(','), Convert.ToInt32);
                foreach (var index in indices)
                {
                    this.buttons[i][index] = true;
                }
            }
        }

        public void PressButton(int index, BitArray s)
        {
            s.Xor(buttons[index]);

            for (int i = 0; i < buttons[index].Count; i++)
            {
                if(buttons[index][i])
                {
                    joltage[i]--;
                }
            }
        }

        public void PressButton(BitArray btn, BitArray s)
        {
            int index = buttons.ToList().IndexOf(btn);
            PressButton(index, s);
        }

        private bool IsInCorrectState(BitArray state)
        {
            state.Xor(goal);
            bool result = state.HasAnySet() == false;
            state.Xor(goal);
            return result;
        }

        public int CheckPerms()
        {
            for (int len = 1; len < buttons.Length; len++)
            {
                var perm = Helper.GetPermutations(Enumerable.Range(0, buttons.Length), len).ToList();

                int min = int.MaxValue;
                foreach (var indexArray in perm)
                {
                    var tmp = indexArray.ToList();
                    BitArray machineState = new BitArray(goal.Length);
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        var buttonIndex = tmp[i];
                        PressButton(buttonIndex, machineState);

                        if (IsInCorrectState(machineState))
                        {
                            if (i + 1 < min)
                                min = i + 1;

                            return min;
                        }
                    }
                }

            }

            return -1;
        }

        public int GetMinPressesToSatisfyJoltage()
        {
            int presses = 0;
            BitArray state = new BitArray(goal.Length);
            do
            {
                PressButton(FindMostPotentButton(), state);
                presses++;
            }
            while (joltage.Sum() > 0);
            return presses;
        }

        public BitArray FindMostPotentButton()
        {
            return buttons.MinBy(x =>
                CountOnes(x.Xor(goal))
            );
        }

        private int CountOnes(BitArray a)
        {
            int c = 0;
            for (int i = 0; i < a.Count; i++)
            {
                c += a[i] ? 1 : 0;
            }

            return c;
        }
    }
}
