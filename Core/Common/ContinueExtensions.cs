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
using System.Threading.Tasks;
using PlatformBindings.Models;

namespace PlatformBindings.Common
{
    public static partial class CoreExtensions
    {
        /// <summary>
        /// Creates a continuation that executes asynchronously when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="Task">Task to Continue from, before perfoming this Action</param>
        /// <param name="action">An action to run when the <see cref="Task"/> completes. </param>
        public static async void ContinueWith(this Task Task, Action action)
        {
            await Task.ContinueWith(task => action());
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="Task">Task to Continue from, before perfoming this Action</param>
        /// <param name="UIBinding">The UI Context for Dispatching</param>
        /// <param name="action">An action to run when the <see cref="Task"/> completes. </param>
        public static async void ContinueOnUIThread(this Task Task, IUIBindingInfo UIBinding, Action action)
        {
            await ContinueOnUIThreadAsync(Task, UIBinding, action);
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="Task">Task to Continue from, before perfoming this Action</param>
        /// <param name="action">An action to run when the <see cref="Task"/> completes. </param>
        public static async void ContinueOnUIThread(this Task Task, Action action)
        {
            await ContinueOnUIThreadAsync(Task, AppServices.Current.UI.DefaultUIBinding, action);
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="Task">Task to Continue from, before perfoming this Task</param>
        /// <param name="action">An Task to run when the <see cref="Task"/> completes. </param>
        public static async void ContinueOnUIThread(this Task Task, IUIBindingInfo UIBinding, Action<Task> action)
        {
            await ContinueOnUIThreadAsync(Task, UIBinding, action);
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="Task">Task to Continue from, before perfoming this Task</param>
        /// <param name="action">An Task to run when the <see cref="Task"/> completes. </param>
        public static async void ContinueOnUIThread<T>(this Task<T> Task, Action<Task<T>> action)
        {
            await ContinueOnUIThreadAsync(Task, AppServices.Current.UI.DefaultUIBinding, action);
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <typeparam name="T">Task Variable</typeparam>
        /// <param name="Task">Task to Continue from, before perfoming this Task</param>
        /// <param name="UIBinding">The UI Context for Dispatching</param>
        /// <param name="action">An Task to run when the <see cref="Task"/> completes. </param>
        public static async void ContinueOnUIThread<T>(this Task<T> Task, IUIBindingInfo UIBinding, Action<Task<T>> action)
        {
            await ContinueOnUIThreadAsync(Task, UIBinding, action);
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="Task">Task to Continue from, before perfoming this Task</param>
        /// <param name="action">An Task to run when the <see cref="Task"/> completes. </param>
        /// <returns>The Continuation Task</returns>
        public static async Task ContinueOnUIThreadAsync(this Task Task, Action<Task> action)
        {
            await ContinueOnUIThreadAsync(Task, AppServices.Current.UI.DefaultUIBinding, () => action(Task));
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="Task">Task to Continue from, before perfoming this Task</param>
        /// <param name="UIBinding">The UI Context for Dispatching</param>
        /// <param name="action">An Task to run when the <see cref="Task"/> completes. </param>
        /// <returns>The Continuation Task</returns>
        public static async Task ContinueOnUIThreadAsync(this Task Task, IUIBindingInfo UIBinding, Action<Task> action)
        {
            await ContinueOnUIThreadAsync(Task, UIBinding, () => action(Task));
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <typeparam name="T">Task Variable</typeparam>
        /// <param name="Task">Task to Continue from, before perfoming this Task</param>
        /// <param name="action">An Task to run when the <see cref="Task"/> completes. </param>
        /// <returns>The Continuation Task</returns>
        public static async Task ContinueOnUIThreadAsync<T>(this Task<T> Task, Action<Task<T>> action)
        {
            await ContinueOnUIThreadAsync(Task, AppServices.Current.UI.DefaultUIBinding, () => action(Task));
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <typeparam name="T">Task Variable</typeparam>
        /// <param name="Task">Task to Continue from, before perfoming this Task</param>
        /// <param name="UIBinding">The UI Context for Dispatching</param>
        /// <param name="action">An Task to run when the <see cref="Task"/> completes. </param>
        /// <returns>The Continuation Task</returns>
        public static async Task ContinueOnUIThreadAsync<T>(this Task<T> Task, IUIBindingInfo UIBinding, Action<Task<T>> action)
        {
            await ContinueOnUIThreadAsync(Task, UIBinding, () => action(Task));
        }

        /// <summary>
        /// Creates a continuation that executes on the UI Thread when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="Task">Task to Continue from, before perfoming this Task</param>
        /// <param name="UIBinding">The UI Context for Dispatching</param>
        /// <param name="action">An Action to run when the <see cref="Task"/> completes. </param>
        /// <returns>The Continuation Task</returns>
        public static async Task ContinueOnUIThreadAsync(this Task Task, IUIBindingInfo UIBinding, Action action)
        {
            await Task.ContinueWith(async task => await UIBinding.ExecuteAsync(action));
        }
    }
}