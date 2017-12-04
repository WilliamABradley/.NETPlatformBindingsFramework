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
using Java.Lang;
using Android.App;
using Android.Content;
using PlatformBindings.Enums;
using Android.Views;

namespace PlatformBindings.Models.DialogHandling
{
    public class AlertDialogBuilder : AlertDialogBuilderBase
    {
        public AlertDialogBuilder(Context Context) : base(Context)
        {
            Builder = new AlertDialog.Builder(Context);
        }

        public override void SetMessage(ICharSequence text)
        {
            Builder.SetMessage(text);
        }

        public override void SetPrimaryButton(ICharSequence text)
        {
            Builder.SetPositiveButton(text, new EventHandler<DialogClickEventArgs>((s, e) => Waiter.TrySetResult(DialogResult.Primary)));
        }

        public override void SetSecondaryButton(ICharSequence text)
        {
            Builder.SetNegativeButton(text, new EventHandler<DialogClickEventArgs>((s, e) => Waiter.TrySetResult(DialogResult.Secondary)));
        }

        public override void SetTitle(ICharSequence text)
        {
            Builder.SetTitle(text);
        }

        public override void SetView(View view)
        {
            Builder.SetView(view);
        }

        public override void SetView(int LayoutResId)
        {
            Builder.SetView(LayoutResId);
        }

        public override void Show()
        {
            Builder.Show();
        }

        private AlertDialog.Builder Builder { get; }
    }
}