using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Collections.Generic;
using System;
using PrismAPI.Graphics;

namespace Epsilon.Applications.Base
{
    public class Calculator : Process
    {
        public string Content = "";
        Window w;
        Block tb;

        Button one, two, three,
            four, five, six,
            seven, eight, nine,
            zero,
            plus, minus, times, div,
            bksp, clear, enter;

        string c = "";
        bool clicked = false;

        int bh = 32, sbh = 24;

        public override void Start()
        {
            base.Start();
            this.w = new();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;

            this.w.StartAPI(this);
            tb = new(x, y + this.w.tSize, w, 20, "Calculator", Color.White, Color.Black, Color.Black);

            int div3 = w / 3;
            one = new(x, y + this.w.tSize + 20 + (bh * 0), div3, bh, GUI.colors.btColor, GUI.colors.bthColor, "1");
            two = new(x + div3, y + this.w.tSize + 20 + (bh * 0), div3 + 1, bh, GUI.colors.btColor, GUI.colors.bthColor, "2");
            three = new(x + div3 * 2, y + this.w.tSize + 20 + (bh * 0), div3 + 1, bh, GUI.colors.btColor, GUI.colors.bthColor, "3");

            four = new(x, y + this.w.tSize + 20 + (bh * 1), div3, bh, GUI.colors.btColor, GUI.colors.bthColor, "4");
            five = new(x + div3, y + this.w.tSize + 20 + (bh * 1), div3 + 1, bh, GUI.colors.btColor, GUI.colors.bthColor, "5");
            six = new(x + div3 * 2, y + this.w.tSize + 20 + (bh * 1), div3 + 1, bh, GUI.colors.btColor, GUI.colors.bthColor, "6");

            seven = new(x, y + this.w.tSize + 20 + (bh * 2), div3, bh, GUI.colors.btColor, GUI.colors.bthColor, "7");
            eight = new(x + div3, y + this.w.tSize + 20 + (bh * 2), div3 + 1, bh, GUI.colors.btColor, GUI.colors.bthColor, "8");
            nine = new(x + div3 * 2, y + this.w.tSize + 20 + (bh * 2), div3 + 1, bh, GUI.colors.btColor, GUI.colors.bthColor, "9");
            zero = new(x + div3, y + this.w.tSize + 20 + (bh * 3), div3 + 1, bh, GUI.colors.btColor, GUI.colors.bthColor, "0");

            int div4 = w / 4;
            plus = new(x, y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, "+");
            minus = new(x + div4, y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, "-");
            times = new(x + (div4 * 2), y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, "*");
            div = new(x + (div4 * 3), y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, "/");

            bksp = new(x, y + this.w.tSize + 20 + (bh * 4) + sbh, div3, sbh, GUI.colors.btColor, GUI.colors.bthColor, "<");
            clear = new(x + div3, y + this.w.tSize + 20 + (bh * 4) + sbh, div3 + 1, sbh, GUI.colors.btColor, GUI.colors.bthColor, "x");
            enter = new(x + (div3 * 2), y + this.w.tSize + 20 + (bh * 4) + sbh, div3 + 1, sbh, GUI.colors.btColor, GUI.colors.bthColor, "=");
        }

        public override void Run()      
        {
            w.DrawT(this); w.DrawB(this);
            tb.X = wData.Position.X; tb.Y = wData.Position.Y + w.tSize; tb.Text = c;
            tb.Update();

            int div3 = wData.Position.Width / 3;
            one.X = wData.Position.X; one.Y = wData.Position.Y + w.tSize + 20; one.Update();
            two.X = wData.Position.X + div3; two.Y = wData.Position.Y + w.tSize + 20; two.Update();
            three.X = wData.Position.X + div3 * 2 + 1; three.Y = wData.Position.Y + w.tSize + 20; three.Update();

            four.X = wData.Position.X; four.Y = wData.Position.Y + w.tSize + 20 + (bh * 1); four.Update();
            five.X = wData.Position.X + div3; five.Y = wData.Position.Y + w.tSize + 20 + (bh * 1); five.Update();
            six.X = wData.Position.X + div3 * 2 + 1; six.Y = wData.Position.Y + w.tSize + 20 + (bh * 1); six.Update();

            seven.X = wData.Position.X; seven.Y = wData.Position.Y + w.tSize + 20 + (bh * 2); seven.Update();
            eight.X = wData.Position.X + div3; eight.Y = wData.Position.Y + w.tSize + 20 + (bh * 2); eight.Update();
            nine.X = wData.Position.X + div3 * 2 + 1; nine.Y = wData.Position.Y + w.tSize + 20 + (bh * 2); nine.Update();
            zero.X = wData.Position.X + div3; zero.Y = wData.Position.Y + w.tSize + 20 + (bh * 3); zero.Update();

            int div4 = wData.Position.Width / 4;
            plus.X = wData.Position.X; plus.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); plus.Update();
            minus.X = wData.Position.X + div4; minus.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); minus.Update();
            times.X = wData.Position.X + (div4 * 2); times.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); times.Update();
            div.X = wData.Position.X + (div4 * 3); div.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); div.Update();

            bksp.X = wData.Position.X; bksp.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; bksp.Update();
            clear.X = wData.Position.X + div3; clear.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; clear.Update();
            enter.X = wData.Position.X + (div3 * 2) + 1; enter.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; enter.Update();

            if (MouseManager.MouseState == MouseState.Left
                && Manager.toUpdate == this
                && !GUI.clicked
                && !clicked)
                OnClick();

            if (MouseManager.MouseState == MouseState.None
                && clicked)
                clicked = false;
        }

        bool _result = false;
        public void OnClick()
        {
            clicked = true;
            if (_result)
            {
                c = "";
                _result = false;
            }

            if (one.CheckHover()) c += '1';
            else if (two.CheckHover()) c += '2';
            else if (three.CheckHover()) c += '3';

            else if (four.CheckHover()) c += '4';
            else if (five.CheckHover()) c += '5';
            else if (six.CheckHover()) c += '6';

            else if (seven.CheckHover()) c += '7';
            else if (eight.CheckHover()) c += '8';
            else if (nine.CheckHover()) c += '9';
            else if (zero.CheckHover()) c += '0';

            else if (plus.CheckHover()) c += '+';
            else if (minus.CheckHover()) c += '-';
            else if (times.CheckHover()) c += '*';
            else if (div.CheckHover()) c += '/';

            else if (enter.CheckHover())
            {
                try
                {
                    // Evaluate the expression in c using the SimpleEvaluator
                    var result = Evaluator.Evaluate(c);
                    c = result.ToString();
                    _result = true;
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the computation
                    c = ex.ToString();
                    _result = true;
                }
            }
            else if (clear.CheckHover()) c = "";
            else if (bksp.CheckHover()
                && c.Length > 0) c = c.Remove(c.Length - 1);
        }
    }

    public static class Evaluator
    {
        public static double Evaluate(string expression)
        {
            var tokens = new Queue<string>(Tokenize(expression));
            var value = ParseExpression(tokens);
            if (tokens.Count > 0)
                throw new ArgumentException("Invalid expression");
            return value;
        }

        private static IEnumerable<string> Tokenize(string expression)
        {
            var number = "";
            foreach (var ch in expression)
            {
                if (char.IsDigit(ch) || ch == '.')
                    number += ch;
                else
                {
                    if (number.Length > 0)
                    {
                        yield return number;
                        number = "";
                    }

                    if ("+-*/()".Contains(ch))
                        yield return ch.ToString();
                }
            }

            if (number.Length > 0)
                yield return number;
        }

        private static double ParseExpression(Queue<string> tokens)
        {
            var left = ParseTerm(tokens);

            while (tokens.Count > 0)
            {
                var op = tokens.Peek();
                if (op != "+" && op != "-")
                    break;

                tokens.Dequeue();
                var right = ParseTerm(tokens);

                if (op == "+") left += right;
                else left -= right;
            }

            return left;
        }

        private static double ParseTerm(Queue<string> tokens)
        {
            var left = ParseFactor(tokens);

            while (tokens.Count > 0)
            {
                var op = tokens.Peek();
                if (op != "*" && op != "/")
                    break;

                tokens.Dequeue();
                var right = ParseFactor(tokens);

                if (op == "*") left *= right;
                else left /= right;
            }

            return left;
        }

        private static double ParseFactor(Queue<string> tokens)
        {
            var token = tokens.Dequeue();

            if (token == "(")
            {
                var value = ParseExpression(tokens);
                if (tokens.Dequeue() != ")")
                    throw new ArgumentException("Mismatched parentheses");
                return value;
            }

            return double.Parse(token);
        }
    }
}
