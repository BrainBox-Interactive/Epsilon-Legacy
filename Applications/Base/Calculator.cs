using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Collections.Generic;
using System;
using System.Drawing;

namespace Epsilon.Applications.Base
{
    public class Calculator : Process
    {
        public string Content = string.Empty;
        Window w;
        Box tb;

        Button[] numberButtons;
        Button plus, minus, times, div, bksp, clear, enter;

        string c = "";
        bool clicked = false;

        int bh = 32, sbh = 24;
        string AcceptedCharacters = "0123456789+-*/. ";

        public override void Start()
        {
            base.Start();
            this.w = new();

            wData.Position.Width = 151;
            wData.Position.Height = 220;

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;

            this.w.StartAPI(this);
            tb = new(x, y + this.w.tSize, w, 20,
                Color.White, Color.Black, "Calculator", this);

            int div3 = w / 3;
            numberButtons = new Button[10];
            for (int i = 0; i < 10; i++)
            {
                int row = i == 0 ? 3 : (i - 1) / 3;
                int col = i == 0 ? 1 : (i - 1) % 3;
                numberButtons[i] = new Button(
                    x - (col == 0 ? 1 : 0) + col * div3,
                    y + this.w.tSize + 20 + (bh * row),
                    div3 + (col == 2 || col == 0 ? 1 : 0),
                    bh,
                    GUI.colors.btColor,
                    GUI.colors.bthColor,
                    GUI.colors.btcColor,
                    i.ToString(), this
                );
            }

            int div4 = w / 4 + 1;
            plus = new(x - 1, y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "+", this);
            minus = new(x - 1 + div4, y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "-", this);
            times = new(x - 1 + (div4 * 2), y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "*", this);
            div = new(x - 1 + (div4 * 3), y + this.w.tSize + 20 + (bh * 4), div4, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "/", this);

            bksp = new(x - 1, y + this.w.tSize + 20 + (bh * 4) + sbh, div3 + 1, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "<", this);
            clear = new(x + div3, y + this.w.tSize + 20 + (bh * 4) + sbh, div3 + 1, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "x", this);
            enter = new(x + (div3 * 2), y + this.w.tSize + 20 + (bh * 4) + sbh, div3 + 1, sbh, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor, "=", this);
        }

        public override void Run()
        {
            w.DrawB(this); w.DrawT(this);
            tb.X = wData.Position.X; tb.Y = wData.Position.Y + w.tSize; tb.Content = c;
            tb.Update();

            int div3 = wData.Position.Width / 3;
            for (int i = 0; i < 10; i++)
            {
                int row = i == 0 ? 3 : (i - 1) / 3;
                int col = i == 0 ? 1 : (i - 1) % 3;
                numberButtons[i].X = wData.Position.X - (col == 0 ? 1 : 0) + col * div3;
                numberButtons[i].Y = wData.Position.Y + w.tSize + 20 + (bh * row);
                numberButtons[i].Update();
            }

            int div4 = wData.Position.Width / 4 + 1;
            plus.X = wData.Position.X - 1; plus.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); plus.Update();
            minus.X = wData.Position.X - 1 + div4; minus.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); minus.Update();
            times.X = wData.Position.X - 1 + (div4 * 2); times.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); times.Update();
            div.X = wData.Position.X - 1 + (div4 * 3); div.Y = wData.Position.Y + w.tSize + 20 + (bh * 4); div.Update();

            bksp.X = wData.Position.X - 1; bksp.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; bksp.Update();
            clear.X = wData.Position.X - 1 + div3; clear.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; clear.Update();
            enter.X = wData.Position.X - 1 + (div3 * 2) + 1; enter.Y = wData.Position.Y + w.tSize + 20 + (bh * 4) + sbh; enter.Update();

            if (Kernel.IsKeyPressed && !tb.isPressed
                && tb.isFocused
                && AcceptedCharacters.Contains(Kernel.k.KeyChar))
                c += Kernel.k.KeyChar;

            if (MouseManager.MouseState == MouseState.Left
                && !GUI.clicked && !clicked)
                OnClick();

            if (MouseManager.MouseState == MouseState.None
                && clicked)
                clicked = false;
        }

        bool _result = false;
        public void OnClick()
        {
            if (Manager.IsFrontTU(this)) return;

            if (_result
                && !enter.CheckHover()
                && GUI.mx >= wData.Position.X
                && GUI.mx <= wData.Position.X + wData.Position.Width
                && GUI.my >= wData.Position.Y + w.tSize + 20
                && GUI.my <= wData.Position.Y + wData.Position.Height - w.tSize - 20)
            {
                c = string.Empty;
                _result = false;
            } clicked = true;

            if (enter.CheckHover())
            {
                try
                {
                    var result = Evaluator.Evaluate(c);
                    c = result.ToString();
                    _result = true;
                }
                catch (Exception)
                {
                    c = "Invalid result ";
                    _result = true;
                }
            }
            else if (clear.CheckHover()) c = "";
            else if (bksp.CheckHover() && c.Length > 0) c = c.Remove(c.Length - 1);

            if (c.Length > (wData.Position.Width - 1) / GUI.dFont.Width) return;

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
