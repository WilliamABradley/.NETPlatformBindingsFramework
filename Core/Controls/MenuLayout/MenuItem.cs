// ******************************************************************
// Copyright (c) William Bradley
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using PlatformBindings.Models;

namespace PlatformBindings.Controls.MenuLayout
{
    public class MenuItem : IMenuItem
    {
        public string Label { get; set; }
        public Action<MenuItem> Action { get; set; }
        public object DataContext { get; set; }
        public object Data { get; set; }
        public bool IsEnabled { get; set; } = true;
        public Shortcut Shortcut { get; set; }
        public string Text { get { return Label + Shortcut ?? ""; } }
    }
}