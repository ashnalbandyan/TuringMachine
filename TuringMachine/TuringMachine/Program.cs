using System;
using System.Collections;
using System.Collections.Generic;


namespace ConsoleApplication1
{

    


    public class Test
    {

        public static int condition = 0;
        public string text;
        // Use this for initialization
       static void Main(string[] args)
        {

            //TuringMachinePlusNew(15, 5);
            //TuringMachineMinusNew(15, 5);
            //TuringMachineMultiplyNew(5, 3);
            TuringMachineDivideNew(50, 4);
            TuringMachineDivideNew(51, 5);
            TuringMachineDivideNew(52, 6);
           Console.WriteLine(TuringMachineDivideNew(56, 7));


        }

        // Update is called once per frame
        void Update()
        {
            //print(text.text);
        }

        public void OnClickedEvent()
        {
            Dictionary<string, int> VariablesAndValues = new Dictionary<string, int>();
            var PostFix = InfixToPostfix.Convert("(A+B/C*(D+E)-F)");

            if (PostFix == null)
            {
                Console.WriteLine("Error");
            }
            else
            {
                for (int i = 0; i < PostFix.Count; i++)
                {
                    var temp = PostFix[i];
                    int number;
                    if (int.TryParse(temp, out number))
                    {
                        VariablesAndValues.Add(temp, number);
                    }
                    else
                    {
                        if (temp[0] >= 'A' && temp[0] <= 'Z')
                        {
                            VariablesAndValues.Add(temp, 2);
                        }
                        else
                            if (temp[0] >= 'a' && temp[0] <= 'z')
                        {
                            VariablesAndValues.Add(temp, 2);
                        }
                        else
                            switch (temp)
                            {

                                case "+":
                                    int _number1;
                                    int _number2;
                                    if (VariablesAndValues.TryGetValue(PostFix[i - 2], out _number1) && VariablesAndValues.TryGetValue(PostFix[i - 1], out _number2))
                                    {
                                        TuringMachinePlusNew(_number1, _number2);
                                    }
                                    break;
                                case "-":
                                    int _number3;
                                    int _number4;
                                    if (VariablesAndValues.TryGetValue(PostFix[i - 2], out _number3) && VariablesAndValues.TryGetValue(PostFix[i - 1], out _number4))
                                    {
                                        TuringMachineMinusNew(_number3, _number4);
                                    }
                                    break;
                                case "*":
                                    int _number5;
                                    int _number6;
                                    if (VariablesAndValues.TryGetValue(PostFix[i - 2], out _number5) && VariablesAndValues.TryGetValue(PostFix[i - 1], out _number6))
                                    {
                                        TuringMachineMultiplyNew(_number5, _number6);
                                    }
                                    break;
                                case "/":
                                    int _number7;
                                    int _number8;
                                    if (VariablesAndValues.TryGetValue(PostFix[i - 2], out _number7) && VariablesAndValues.TryGetValue(PostFix[i - 1], out _number8))
                                    {
                                        TuringMachineDivideNew(_number7, _number8);
                                    }
                                    break;

                                default:
                                    break;
                            }
                    }
                }
            }


        }

        private bool TranslateInputedFunction(string _inputedField)
        {
            int countOfVariables = 0;
            string inputedField = _inputedField;
            foreach (var t in inputedField)
            {
                if (t >= 'a' && t <= 'z')
                {
                    countOfVariables++;
                }
                else
                    if (t >= 'A' && t <= 'Z')
                {
                    countOfVariables++;
                }
                else
                    switch (t)
                    {
                        case '+':
                            break;
                        case '-':
                            break;
                        case '*':
                            break;
                        case '/':
                            break;
                        case '^':
                            break;
                        case '(':
                            break;
                        case ')':
                            break;
                        case ' ':
                            break;
                        default:
                            return false;
                    }
            }
            return true;
        }


       static int TuringMachinePlusNew(int x, int y)
        {
            List<TuringMachineNode> PlusLogic = new List<TuringMachineNode>();

            PlusLogic.Add(new TuringMachineNode(0, 0, 0, HeadDirection.Right, 0));
            PlusLogic.Add(new TuringMachineNode(0, 1, 1, HeadDirection.Right, 1));

            PlusLogic.Add(new TuringMachineNode(1, 0, 1, HeadDirection.Right, 2));
            PlusLogic.Add(new TuringMachineNode(1, 1, 1, HeadDirection.Right, 1));

            PlusLogic.Add(new TuringMachineNode(2, 0, 0, HeadDirection.Left, 3));
            PlusLogic.Add(new TuringMachineNode(2, 1, 1, HeadDirection.Right, 2));

            PlusLogic.Add(new TuringMachineNode(3, 0, 0, HeadDirection.Left, 3));
            PlusLogic.Add(new TuringMachineNode(3, 1, 0, HeadDirection.Left, 4));

            PlusLogic.Add(new TuringMachineNode(4, 0, 0, HeadDirection.Stay, -1));
            PlusLogic.Add(new TuringMachineNode(4, 1, 1, HeadDirection.Left, 4));




            //////////////////////////////////////////////////////////////////////////////////////

            int result = 0;

            List<int> track = new List<int>();

            track.Add(0);

            for (int i = 0; i < x; i++)
            {
                track.Add(1);
            }

            track.Add(0);

            for (int i = 0; i < y; i++)
            {
                track.Add(1);
            }

            for (int i = 0; i < x + y; i++)
            {
                track.Add(0);
            }

            int currentState = 0;
            int currentRead = 0;
            int index = 0;

            while (currentState != -1)
            {
                currentRead = track[index];
                TuringMachineNode temp = PlusLogic.Find(t => t.currentState == currentState && t.read == currentRead);
                track[index] = temp.write;
                currentState = temp.nextState;
                index += (int)temp.headDirection;
            }

            for (int i = 0; i < track.Count; i++)
            {
                result += track[i];
            }
            // var temp = PlusLogic.Find(t => t.currentState == currentState && t.read == currentRead);
            //currentState = temp.nextState;
            //index += (int)temp.headDirection;

            return result;
        }

       static int TuringMachineMinusNew(int x, int y)
        {
            List<TuringMachineNode> MinusLogic = new List<TuringMachineNode>();

            MinusLogic.Add(new TuringMachineNode(0, 0, 0, HeadDirection.Right, 0));
            MinusLogic.Add(new TuringMachineNode(0, 1, 0, HeadDirection.Right, 1));

            MinusLogic.Add(new TuringMachineNode(1, 0, 0, HeadDirection.Right, 2));
            MinusLogic.Add(new TuringMachineNode(1, 1, 1, HeadDirection.Right, 1));

            MinusLogic.Add(new TuringMachineNode(2, 0, 0, HeadDirection.Left, 3));
            MinusLogic.Add(new TuringMachineNode(2, 1, 1, HeadDirection.Right, 2));

            MinusLogic.Add(new TuringMachineNode(3, 0, 1, HeadDirection.Stay, -1));
            MinusLogic.Add(new TuringMachineNode(3, 1, 0, HeadDirection.Left, 4));

            MinusLogic.Add(new TuringMachineNode(4, 0, 0, HeadDirection.Stay, -1));
            MinusLogic.Add(new TuringMachineNode(4, 1, 1, HeadDirection.Left, 5));

            MinusLogic.Add(new TuringMachineNode(5, 0, 0, HeadDirection.Left, 6));
            MinusLogic.Add(new TuringMachineNode(5, 1, 1, HeadDirection.Left, 5));

            MinusLogic.Add(new TuringMachineNode(6, 0, 1, HeadDirection.Stay, -1));
            MinusLogic.Add(new TuringMachineNode(6, 1, 1, HeadDirection.Left, 7));

            MinusLogic.Add(new TuringMachineNode(7, 0, 0, HeadDirection.Right, 0));
            MinusLogic.Add(new TuringMachineNode(7, 1, 1, HeadDirection.Left, 7));



            //////////////////////////////////////////////////////////////////////////////////////

            int result = 0;

            List<int> track = new List<int>();

            track.Add(0);

            for (int i = 0; i < x; i++)
            {
                track.Add(1);
            }

            track.Add(0);

            for (int i = 0; i < y; i++)
            {
                track.Add(1);
            }

            for (int i = 0; i < x + y; i++)
            {
                track.Add(0);
            }

            int currentState = 0;
            int currentRead = 0;
            int index = 0;

            while (currentState != -1)
            {
                currentRead = track[index];
                TuringMachineNode temp = MinusLogic.Find(t => t.currentState == currentState && t.read == currentRead);
                track[index] = temp.write;
                currentState = temp.nextState;
                index += (int)temp.headDirection;
            }

            for (int i = 0; i < track.Count; i++)
            {
                result += track[i];
            }

            return result;
        }

       static int TuringMachineMultiplyNew(int x, int y)
        {
            List<TuringMachineNode> MultiplyLogic = new List<TuringMachineNode>();

            MultiplyLogic.Add(new TuringMachineNode(0, 0, 0, HeadDirection.Right, 1));
            MultiplyLogic.Add(new TuringMachineNode(0, 1, 0, HeadDirection.Right, 2));

            MultiplyLogic.Add(new TuringMachineNode(1, 0, 0, HeadDirection.Right, 14));
            MultiplyLogic.Add(new TuringMachineNode(1, 1, 0, HeadDirection.Right, 2));

            MultiplyLogic.Add(new TuringMachineNode(2, 0, 0, HeadDirection.Right, 3));
            MultiplyLogic.Add(new TuringMachineNode(2, 1, 1, HeadDirection.Right, 2));

            MultiplyLogic.Add(new TuringMachineNode(3, 0, 0, HeadDirection.Left, 15));
            MultiplyLogic.Add(new TuringMachineNode(3, 1, 0, HeadDirection.Right, 4));

            MultiplyLogic.Add(new TuringMachineNode(4, 0, 0, HeadDirection.Right, 5));
            MultiplyLogic.Add(new TuringMachineNode(4, 1, 1, HeadDirection.Right, 4));

            MultiplyLogic.Add(new TuringMachineNode(5, 0, 1, HeadDirection.Left, 6));
            MultiplyLogic.Add(new TuringMachineNode(5, 1, 1, HeadDirection.Right, 5));

            MultiplyLogic.Add(new TuringMachineNode(6, 0, 0, HeadDirection.Left, 7));
            MultiplyLogic.Add(new TuringMachineNode(6, 1, 1, HeadDirection.Left, 6));

            MultiplyLogic.Add(new TuringMachineNode(7, 0, 1, HeadDirection.Left, 9));
            MultiplyLogic.Add(new TuringMachineNode(7, 1, 1, HeadDirection.Left, 8));

            MultiplyLogic.Add(new TuringMachineNode(8, 0, 1, HeadDirection.Right, 3));
            MultiplyLogic.Add(new TuringMachineNode(8, 1, 1, HeadDirection.Left, 8));

            MultiplyLogic.Add(new TuringMachineNode(9, 0, 0, HeadDirection.Left, 10));
            MultiplyLogic.Add(new TuringMachineNode(9, 1, 1, HeadDirection.Left, 9));

            MultiplyLogic.Add(new TuringMachineNode(10, 0, 0, HeadDirection.Right, 12));
            MultiplyLogic.Add(new TuringMachineNode(10, 1, 1, HeadDirection.Left, 11));

            MultiplyLogic.Add(new TuringMachineNode(11, 0, 0, HeadDirection.Right, 0));
            MultiplyLogic.Add(new TuringMachineNode(11, 1, 1, HeadDirection.Left, 11));

            MultiplyLogic.Add(new TuringMachineNode(12, 0, 0, HeadDirection.Right, 12));
            MultiplyLogic.Add(new TuringMachineNode(12, 1, 0, HeadDirection.Right, 13));

            MultiplyLogic.Add(new TuringMachineNode(13, 0, 0, HeadDirection.Stay, -1));
            MultiplyLogic.Add(new TuringMachineNode(13, 1, 0, HeadDirection.Right, 13));

            MultiplyLogic.Add(new TuringMachineNode(14, 0, 0, HeadDirection.Stay, -1));
            MultiplyLogic.Add(new TuringMachineNode(14, 1, 0, HeadDirection.Right, 14));

            MultiplyLogic.Add(new TuringMachineNode(15, 0, 0, HeadDirection.Left, 16));
            MultiplyLogic.Add(new TuringMachineNode(15, 1, 0, HeadDirection.Left, 15));

            MultiplyLogic.Add(new TuringMachineNode(16, 0, 0, HeadDirection.Stay, -1));
            MultiplyLogic.Add(new TuringMachineNode(16, 1, 0, HeadDirection.Left, 16));


            //////////////////////////////////////////////////////////////////////////////////////

            int result = 0;

            List<int> track = new List<int>();

            track.Add(0);

            for (int i = 0; i < x; i++)
            {
                track.Add(1);
            }

            track.Add(0);

            for (int i = 0; i < y; i++)
            {
                track.Add(1);
            }

            for (int i = 0; i < 5 * x * y; i++)
            {
                track.Add(0);
            }

            int currentState = 0;
            int currentRead = 0;
            int index = 0;

            while (currentState != -1)
            {
                currentRead = track[index];
                TuringMachineNode temp = MultiplyLogic.Find(t => t.currentState == currentState && t.read == currentRead);
                track[index] = temp.write;
                currentState = temp.nextState;
                index += (int)temp.headDirection;
            }

            for (int i = 0; i < track.Count; i++)
            {
                result += track[i];
            }

            return result;
        }

       static int TuringMachineDivideNew(int x, int y)
        {
            List<TuringMachineNode> DivideLogic = new List<TuringMachineNode>();

            DivideLogic.Add(new TuringMachineNode(0, 0, 0, HeadDirection.Right, 0));
            DivideLogic.Add(new TuringMachineNode(0, 1, 1, HeadDirection.Left, 1));

            DivideLogic.Add(new TuringMachineNode(1, 0, 0, HeadDirection.Left, 2));
            DivideLogic.Add(new TuringMachineNode(1, 1, 0, HeadDirection.Left, 2));

            DivideLogic.Add(new TuringMachineNode(2, 0, 1, HeadDirection.Left, 3));
            DivideLogic.Add(new TuringMachineNode(2, 1, 1, HeadDirection.Left, 3));

            DivideLogic.Add(new TuringMachineNode(3, 0, 0, HeadDirection.Right, 4));
            DivideLogic.Add(new TuringMachineNode(3, 1, 0, HeadDirection.Right, 4));

            DivideLogic.Add(new TuringMachineNode(4, 0, 1, HeadDirection.Right, 5));
            DivideLogic.Add(new TuringMachineNode(4, 1, 1, HeadDirection.Right, 5));

            DivideLogic.Add(new TuringMachineNode(5, 0, 0, HeadDirection.Right, 6));
            DivideLogic.Add(new TuringMachineNode(5, 1, 1, HeadDirection.Stay, -1));

            DivideLogic.Add(new TuringMachineNode(6, 0, 0, HeadDirection.Right, 7));
            DivideLogic.Add(new TuringMachineNode(6, 1, 1, HeadDirection.Right, 6));

            DivideLogic.Add(new TuringMachineNode(7, 0, 0, HeadDirection.Right, 7));
            DivideLogic.Add(new TuringMachineNode(7, 1, 0, HeadDirection.Right, 8));

            DivideLogic.Add(new TuringMachineNode(8, 0, 0, HeadDirection.Left, 23));
            DivideLogic.Add(new TuringMachineNode(8, 1, 1, HeadDirection.Left, 9));

            DivideLogic.Add(new TuringMachineNode(9, 0, 0, HeadDirection.Left, 9));
            DivideLogic.Add(new TuringMachineNode(9, 1, 1, HeadDirection.Left, 10));

            DivideLogic.Add(new TuringMachineNode(10, 0, 0, HeadDirection.Right, 11));
            DivideLogic.Add(new TuringMachineNode(10, 1, 1, HeadDirection.Left, 37));

            DivideLogic.Add(new TuringMachineNode(11, 0, 0, HeadDirection.Stay, -1));
            DivideLogic.Add(new TuringMachineNode(11, 1, 1, HeadDirection.Right, 12));

            DivideLogic.Add(new TuringMachineNode(12, 0, 0, HeadDirection.Right, 12));
            DivideLogic.Add(new TuringMachineNode(12, 1, 1, HeadDirection.Left, 13));

            DivideLogic.Add(new TuringMachineNode(13, 0, 1, HeadDirection.Left, 14));
            DivideLogic.Add(new TuringMachineNode(13, 1, 1, HeadDirection.Stay, -1));

            DivideLogic.Add(new TuringMachineNode(14, 0, 0, HeadDirection.Left, 15));
            DivideLogic.Add(new TuringMachineNode(14, 1, 1, HeadDirection.Stay, -1));

            DivideLogic.Add(new TuringMachineNode(15, 0, 0, HeadDirection.Left, 16));
            DivideLogic.Add(new TuringMachineNode(15, 1, 1, HeadDirection.Left, 19));

            DivideLogic.Add(new TuringMachineNode(16, 0, 0, HeadDirection.Left, 16));
            DivideLogic.Add(new TuringMachineNode(16, 1, 1, HeadDirection.Left, 17));

            DivideLogic.Add(new TuringMachineNode(17, 0, 1, HeadDirection.Right, 18));
            DivideLogic.Add(new TuringMachineNode(17, 1, 1, HeadDirection.Left, 17));

            DivideLogic.Add(new TuringMachineNode(18, 0, 0, HeadDirection.Right, 12));
            DivideLogic.Add(new TuringMachineNode(18, 1, 1, HeadDirection.Right, 18));

            DivideLogic.Add(new TuringMachineNode(19, 0, 0, HeadDirection.Left, 20));
            DivideLogic.Add(new TuringMachineNode(19, 1, 1, HeadDirection.Left, 19));

            DivideLogic.Add(new TuringMachineNode(20, 0, 0, HeadDirection.Left, 20));
            DivideLogic.Add(new TuringMachineNode(20, 1, 1, HeadDirection.Left, 21));

            DivideLogic.Add(new TuringMachineNode(21, 0, 0, HeadDirection.Right, 22));
            DivideLogic.Add(new TuringMachineNode(21, 1, 1, HeadDirection.Left, 21));

            DivideLogic.Add(new TuringMachineNode(22, 0, 0, HeadDirection.Stay, -1));
            DivideLogic.Add(new TuringMachineNode(22, 1, 0, HeadDirection.Stay, -1));

            DivideLogic.Add(new TuringMachineNode(23, 0, 1, HeadDirection.Left, 23));
            DivideLogic.Add(new TuringMachineNode(23, 1, 1, HeadDirection.Right, 24));

            DivideLogic.Add(new TuringMachineNode(24, 0, 0, HeadDirection.Stay, -1));
            DivideLogic.Add(new TuringMachineNode(24, 1, 0, HeadDirection.Left, 25));

            DivideLogic.Add(new TuringMachineNode(25, 0, 0, HeadDirection.Stay, -1));
            DivideLogic.Add(new TuringMachineNode(25, 1, 1, HeadDirection.Left, 26));

            DivideLogic.Add(new TuringMachineNode(26, 0, 0, HeadDirection.Right, 32));
            DivideLogic.Add(new TuringMachineNode(26, 1, 1, HeadDirection.Left, 27));

            DivideLogic.Add(new TuringMachineNode(27, 0, 0, HeadDirection.Left, 39));
            DivideLogic.Add(new TuringMachineNode(27, 1, 1, HeadDirection.Left, 27));

            DivideLogic.Add(new TuringMachineNode(28, 0, 1, HeadDirection.Left, 29));
            DivideLogic.Add(new TuringMachineNode(28, 1, 1, HeadDirection.Left, 28));

            DivideLogic.Add(new TuringMachineNode(29, 0, 0, HeadDirection.Right, 30));
            DivideLogic.Add(new TuringMachineNode(29, 1, 0, HeadDirection.Right, 30));

            DivideLogic.Add(new TuringMachineNode(30, 0, 0, HeadDirection.Right, 31));
            DivideLogic.Add(new TuringMachineNode(30, 1, 1, HeadDirection.Right, 30));

            DivideLogic.Add(new TuringMachineNode(31, 0, 0, HeadDirection.Right, 31));
            DivideLogic.Add(new TuringMachineNode(31, 1, 0, HeadDirection.Right, 6));

            DivideLogic.Add(new TuringMachineNode(32, 0, 0, HeadDirection.Stay, -1));
            DivideLogic.Add(new TuringMachineNode(32, 1, 0, HeadDirection.Right, 33));

            DivideLogic.Add(new TuringMachineNode(33, 0, 0, HeadDirection.Right, 34));
            DivideLogic.Add(new TuringMachineNode(33, 1, 1, HeadDirection.Stay, -1));

            DivideLogic.Add(new TuringMachineNode(34, 0, 0, HeadDirection.Left, 35));
            DivideLogic.Add(new TuringMachineNode(34, 1, 0, HeadDirection.Right, 34));

            DivideLogic.Add(new TuringMachineNode(35, 0, 0, HeadDirection.Left, 35));
            DivideLogic.Add(new TuringMachineNode(35, 1, 1, HeadDirection.Left, 36));

            DivideLogic.Add(new TuringMachineNode(36, 0, 0, HeadDirection.Stay, -1));
            DivideLogic.Add(new TuringMachineNode(36, 1, 1, HeadDirection.Left, 36));

            DivideLogic.Add(new TuringMachineNode(37, 0, 0, HeadDirection.Right, 38));
            DivideLogic.Add(new TuringMachineNode(37, 1, 1, HeadDirection.Left, 37));

            DivideLogic.Add(new TuringMachineNode(38, 0, 0, HeadDirection.Stay, -1));
            DivideLogic.Add(new TuringMachineNode(38, 1, 0, HeadDirection.Right, 6));

            DivideLogic.Add(new TuringMachineNode(39, 0, 0, HeadDirection.Left, 39));
            DivideLogic.Add(new TuringMachineNode(39, 1, 1, HeadDirection.Left, 28));



            //////////////////////////////////////////////////////////////////////////////////////

            int result = 0;

            List<int> track = new List<int>();

            track.Add(0);
            for (int i = 0; i < x * y; i++)
            {
                track.Add(0);
            }

            for (int i = 0; i < x; i++)
            {
                track.Add(1);
            }

            track.Add(0);

            for (int i = 0; i < y; i++)
            {
                track.Add(1);
            }

            for (int i = 0; i < x * y; i++)
            {
                track.Add(0);
            }

            int currentState = 0;
            int currentRead = 0;
            int index = 0;

            try
            {

                while (currentState != -1)
                {
                    if (index >= track.Count)
                    {
                        for (int i = 0; i < x * y; i++)
                        {
                            track.Add(0);
                        }
                    }
                    currentRead = track[index];
                    TuringMachineNode temp = DivideLogic.Find(t => t.currentState == currentState && t.read == currentRead);
                    track[index] = temp.write;
                    currentState = temp.nextState;
                    index += (int)temp.headDirection;
                }
            }
            catch (System.Exception e)
            {
                var t = e.Message;
            }

            bool isZero = false;

            for (int i = 0; i < track.Count; i++)
            {
                if (track[i] == 1)
                {
                    isZero = true;
                }
                if (isZero == true)
                {
                    if (track[i] == 0)
                    {
                        break;
                    }
                }
                result += track[i];
            }

            return result;
        }
    }

    public class TuringMachineNode
    {
        public readonly int currentState;
        public readonly int read;
        public readonly int write;
        public readonly HeadDirection headDirection;
        public readonly int nextState;

        public TuringMachineNode(int _currentState, int _read, int _write, HeadDirection _headDirection, int _nextState)
        {
            currentState = _currentState;
            read = _read;
            write = _write;
            headDirection = _headDirection;
            nextState = _nextState;
        }
    }

    public enum HeadDirection
    {
        Stay = 0,
        Right = 1,
        Left = -1
    }

    public class InfixToPostfix
    {

        public static bool Check(string _infix)
        {
            int countOfBrackets = 0;
            bool isPreviousMathSign = false;
            bool isPreviousDigit = false;
            bool isPreviousLetter = false;
            bool isPreviousBracket = false;

            foreach (var t in _infix)
            {
                if (char.IsNumber(t))
                {
                    if (isPreviousLetter == true)
                    {
                        return false;
                    }
                    isPreviousDigit = true;
                    isPreviousMathSign = false;
                    isPreviousLetter = false;
                    isPreviousBracket = false;

                    continue;
                }
                else
                if (t >= 'a' && t <= 'z')
                {
                    if (isPreviousLetter == true || isPreviousDigit == true)
                    {
                        return false;
                    }
                    isPreviousLetter = true;
                    isPreviousMathSign = false;
                    isPreviousDigit = false;
                    isPreviousBracket = false;

                    continue;
                }
                else
                    if (t >= 'A' && t <= 'Z')
                {
                    if (isPreviousLetter == true || isPreviousDigit == true)
                    {
                        return false;
                    }
                    isPreviousLetter = true;
                    isPreviousMathSign = false;
                    isPreviousDigit = false;
                    isPreviousBracket = false;

                    continue;
                }
                else
                    switch (t)
                    {
                        case '+':
                            if (isPreviousMathSign == true || isPreviousBracket == true)
                                return false;
                            isPreviousLetter = false;
                            isPreviousMathSign = true;
                            isPreviousBracket = false;
                            isPreviousDigit = false;

                            break;
                        case '-':
                            if (isPreviousMathSign == true || isPreviousBracket == true)
                                return false;
                            isPreviousMathSign = true;
                            isPreviousLetter = false;
                            isPreviousDigit = false;
                            isPreviousBracket = false;

                            break;
                        case '*':
                            if (isPreviousMathSign == true || isPreviousBracket == true)
                                return false;
                            isPreviousMathSign = true;
                            isPreviousLetter = false;
                            isPreviousDigit = false;
                            isPreviousBracket = false;

                            break;
                        case '/':
                            if (isPreviousMathSign == true || isPreviousBracket == true)
                                return false;
                            isPreviousMathSign = true;
                            isPreviousLetter = false;
                            isPreviousDigit = false;
                            isPreviousBracket = false;

                            break;
                        //case '^':
                        //    if (isPreviousMathSign == true)
                        //        return false;
                        //    isPreviousMathSign = true;
                        //    break;
                        case '(':
                            isPreviousMathSign = false;
                            isPreviousLetter = false;
                            isPreviousDigit = false;
                            isPreviousBracket = true;

                            countOfBrackets++;

                            break;
                        case ')':
                            isPreviousMathSign = false;
                            isPreviousLetter = false;
                            isPreviousDigit = false;
                            isPreviousBracket = false;// true;

                            countOfBrackets--;

                            break;
                        case ' ':
                            isPreviousMathSign = false;
                            isPreviousLetter = false;
                            isPreviousDigit = false;
                            isPreviousBracket = false;

                            break;
                        default:
                            return false;
                    }
                if (countOfBrackets < 0)
                {
                    return false;
                }
            }

            if (countOfBrackets != 0)
            {
                return false;
            }
            return true;
        }

        public static List<string> Convert(string _infix)
        {
            bool lastWasNumber = false;
            string lastNumber = string.Empty;
            if (!Check(_infix))
            {
                return null;
            }

            Stack<char> Variables = new Stack<char>();
            Stack<char> MathSigns = new Stack<char>();
            Stack<char> Numbers = new Stack<char>();
            Stack<char> Bracket = new Stack<char>();


            List<string> Postfix = new List<string>();
            //Stack<string> Postfix = new Stack<string>();


            foreach (var t in _infix)
            {
                if (char.IsNumber(t))
                {
                    if (lastWasNumber == true)
                    {
                        lastWasNumber = true;
                    }
                    //Numbers.Push(t);
                    lastNumber += t;
                    continue;
                }
                else
                {
                    if (lastNumber != string.Empty)
                    {
                        Postfix.Add(lastNumber);
                        lastNumber = string.Empty;
                    }
                    if (t >= 'a' && t <= 'z')
                    {
                        //Variables.Push(t);
                        Postfix.Add(t.ToString());
                        continue;
                    }
                    else
                        if (t >= 'A' && t <= 'Z')
                    {
                        //Variables.Push(t);
                        Postfix.Add(t.ToString());

                        continue;
                    }
                    else
                        switch (t)
                        {
                            case '+':
                            case '-':
                                if (MathSigns.Count == 0)
                                {
                                    MathSigns.Push(t);
                                }
                                var tempSign = MathSigns.Peek();
                                while (tempSign != '(' && MathSigns.Count != 1)
                                {
                                    MathSigns.Pop();
                                    Postfix.Add(tempSign.ToString());
                                    tempSign = MathSigns.Peek();
                                }
                                MathSigns.Push(t);


                                break;

                            case '*':
                            case '/':
                                if (MathSigns.Count == 0)
                                {
                                    MathSigns.Push(t);
                                }
                                var tempSign1 = MathSigns.Peek();
                                while ((tempSign1 == '*' || tempSign1 == '/') && MathSigns.Count != 1)
                                {
                                    MathSigns.Pop();
                                    Postfix.Add(tempSign1.ToString());
                                    tempSign1 = MathSigns.Peek();
                                }
                                MathSigns.Push(t);

                                break;

                            case '(':
                                MathSigns.Push(t);
                                break;
                            case ')':

                                var tempSign2 = MathSigns.Peek();
                                while (tempSign2 != '(' && MathSigns.Count != 1)
                                {
                                    MathSigns.Pop();
                                    Postfix.Add(tempSign2.ToString());
                                    tempSign2 = MathSigns.Peek();
                                }
                                MathSigns.Pop();

                                break;
                            case ' ':

                                break;
                            default:
                                return null;
                        }
                }
            }

            return Postfix;

        }

    }
}