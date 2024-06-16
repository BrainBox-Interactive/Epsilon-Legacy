using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Collections.Generic;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Epsilon.Applications.Base
{
    public class Calculator : Process
    {
        public string Content = "";
        Window w;
        Block tb;

        Button[] numberButtons;
        Button plus, minus, times, div, bksp, clear, enter;

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
            numberButtons = new Button[10];
            for (int i = 0; i < 10; i++)
            {
                int row = i == 0 ? 3 : (i - 1) / 3;
                int col = i == 0 ? 1 : (i - 1) % 3;
                numberButtons[i] = new Button(
                    x + col * div3,
                    y + this.w.tSize + 20 + (bh * row),
                    div3 + (col == 2 ? 1 : 0),
                    bh,
                    GUI.colors.btColor,
                    GUI.colors.bthColor,
                    GUI.colors.btcColor,
                    i.ToString()
                );
            }

            int div4 = w / 4;
            plus = new(x, y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "+");
            minus = new(x + div4, y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "-");
            times = new(x + (div4 * 2), y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "*");
            div = new(x + (div4 * 3), y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "/");

            bksp = new(x, y + this.w.tSize + 20 + (bh * 4) + sbh, div3, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "<");
            clear = new(x + div3, y + this.w.tSize + 20 + (bh * 4) + sbh, div3 + 1, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "x");
            enter = new(x + (div3 * 2), y + this.w.tSize + 20 + (bh * 4) + sbh, div3 + 1, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "=");
        }

        public override void Run()
        {
            w.DrawT(this); w.DrawB(this);
            tb.X = wData.Position.X; tb.Y = wData.Position.Y + w.tSize; tb.Text = c;
            tb.Update();

            int div3 = wData.Position.Width / 3;
            for (int i = 0; i < 10; i++)
            {
                int row = i == 0 ? 3 : (i - 1) / 3;
                int col = i == 0 ? 1 : (i - 1) % 3;
                numberButtons[i].X = wData.Position.X + col * div3;
                numberButtons[i].Y = wData.Position.Y + w.tSize + 20 + (bh * row);
                numberButtons[i].Update();
            }

            int div4 = wData.Position.Width / 4;
            plus.X = wData.Position.X; plus.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); plus.Update();
            minus.X = wData.Position.X + div4; minus.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); minus.Update();
            times.X = wData.Position.X + (div4 * 2); times.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); times.Update();
            div.X = wData.Position.X + (div4 * 3); div.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); div.Update();

            bksp.X = wData.Position.X; bksp.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; bksp.Update();
            clear.X = wData.Position.X + div3; clear.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; clear.Update();
            enter.X = wData.Position.X + (div3 * 2) + 1; enter.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; enter.Update();

            if (MouseManager.MouseState == MouseState.Left
                && !GUI.clicked)
                OnClick();
        }

        bool _result = false;
        public void OnClick()
        {
            if (_result
                && !enter.CheckHover())
            {
                c = string.Empty;
                _result = false;
            }

            for (int i = 0; i < 10; i++)
                if (numberButtons[i].CheckHover())
                {
                    c += i.ToString();
                    return;
                }

            if (plus.CheckHover()) c += '+';
            else if (minus.CheckHover()) c += '-';
            else if (times.CheckHover()) c += '*';
            else if (div.CheckHover()) c += '/';

            else if (enter.CheckHover())
            {
                try
                {
                    var result = Evaluator.Evaluate(c);
                    c = result.ToString();
                    _result = true;
                }
                catch (Exception ex)
                {
                    c = ex.ToString();
                    _result = true;
                }
            }
            else if (clear.CheckHover()) c = "";
            else if (bksp.CheckHover() && c.Length > 0) c = c.Remove(c.Length - 1);
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
