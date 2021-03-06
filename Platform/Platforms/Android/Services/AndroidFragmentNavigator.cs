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

using Android.App;
using PlatformBindings.Common;
using PlatformBindings.Models;

namespace PlatformBindings.Services
{
    public abstract class AndroidFragmentNavigator<T> : Navigator<T>
    {
        /// <summary>
        /// Replaces the PrimaryNavigationFragment with a new Activity.
        /// </summary>
        /// <param name="Fragment">New Navigation Fragment.</param>
        /// <param name="Parameter">Parameter to remember after the Navigation.</param>
        /// <param name="ClearBackStack">Removes the ability to go to the previous Fragment.</param>
        /// <returns>Navigation Handled</returns>
        protected bool NavigatePrimaryFragment(Fragment Fragment, NavigationParameters Parameters, bool ClearBackStack)
        {
            var currentNavigationActivity = Manager.PrimaryNavigationFragment;
            _Parameters = Parameters;

            var transaction = Manager.BeginTransaction();
            transaction.Replace(currentNavigationActivity.Id, Fragment);
            transaction.SetPrimaryNavigationFragment(Fragment);
            if (!ClearBackStack)
            {
                transaction.AddToBackStack(null);
            }
            transaction.Commit();
            return true;
        }

        public override NavigationParameters Parameters => _Parameters;

        /// <summary>
        /// Parameter Storage, as Fragment Navigation doesn't natively support Parameters that I'm aware of.
        /// </summary>
        protected NavigationParameters _Parameters { get; set; }

        public FragmentManager Manager => AndroidHelpers.GetCurrentActivity().FragmentManager;
    }
}