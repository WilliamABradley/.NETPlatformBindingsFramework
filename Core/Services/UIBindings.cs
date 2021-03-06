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
using PlatformBindings.Enums;
using PlatformBindings.Controls.MenuLayout;
using PlatformBindings.Models;

namespace PlatformBindings.Services
{
    /// <summary>
    /// Functions for Platform Independent UI Functions.
    /// </summary>
    public abstract class UIBindings
    {
        public UIBindings(Platform Platform)
        {
            UIPlatform = Platform;
        }

        /// <summary>
        /// The Interaction Manager for the Current Session, this is used to react to User Input, such as Keyboard combinations, Controller Input, etc.
        /// </summary>
        public abstract InteractionManager InteractionManager { get; }

        /// <summary>
        /// The Navigation Manager for the Current Session, this usually has to be instantiated in the Shell Page. This is used to handle Page Navigation.
        /// </summary>
        public abstract NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// The Navigation Manager for the Current Session, this manages the Title of the Page and Window.
        /// </summary>
        public abstract ITitleManager TitleManager { get; set; }

        /// <summary>
        /// The Default UI Context for the Current Session State. Contains Dispatcher Information for Handling UI.
        /// </summary>
        public abstract IUIBindingInfo DefaultUIBinding { get; }

        /// <summary>
        /// Prompts the User with a Message, and two Selectable Buttons.
        /// </summary>
        /// <param name="Title">Title for the Dialog</param>
        /// <param name="Message">Message for the Dialog</param>
        /// <param name="PrimaryButtonText">Primary Button Text, e.g. Accept</param>
        /// <param name="SecondaryButtonText">Secondary Button Text, e.g. Cancel</param>
        /// <param name="UIBinding">Additional UI Context for Handling the Dialog</param>
        /// <returns>What the User Picked from the Dialog</returns>
        public abstract Task<DialogResult> PromptUserAsync(string Title, string Message, string PrimaryButtonText, string SecondaryButtonText, IUIBindingInfo UIBinding);

        /// <summary>
        /// Prompts the User with a Message, and two Selectable Buttons.
        /// </summary>
        /// <param name="Title">Title for the Dialog</param>
        /// <param name="Message">Message for the Dialog</param>
        /// <param name="PrimaryButtonText">Primary Button Text, e.g. Accept</param>
        /// <param name="SecondaryButtonText">Secondary Button Text, e.g. Cancel</param>
        /// <returns>What the User Picked from the Dialog</returns>
        public Task<DialogResult> PromptUserAsync(string Title, string Message, string PrimaryButtonText, string SecondaryButtonText)
        {
            return PromptUserAsync(Title, Message, PrimaryButtonText, SecondaryButtonText, null);
        }

        /// <summary>
        /// Prompts the User with a Message, and a Primary Button.
        /// </summary>
        /// <param name="Title">Title for the Dialog</param>
        /// <param name="Message">Message for the Dialog</param>
        /// <param name="PrimaryButtonText">Primary Button Text, e.g. OK</param>
        /// <returns>Completion Task</returns>
        public Task PromptUserAsync(string Title, string Message, string PrimaryButtonText)
        {
            return PromptUserAsync(Title, Message, PrimaryButtonText, null, null);
        }

        /// <summary>
        /// Prompts the User with a Message, and a Primary Button.
        /// </summary>
        /// <param name="Title">Title for the Dialog</param>
        /// <param name="Message">Message for the Dialog</param>
        /// <param name="PrimaryButtonText">Primary Button Text, e.g. OK</param>
        /// <param name="UIBinding">Additional UI Context for Handling the Dialog</param>
        public async void PromptUser(string Title, string Message, string PrimaryButtonText, IUIBindingInfo UIBinding)
        {
            await PromptUserAsync(Title, Message, PrimaryButtonText, null, UIBinding);
        }

        /// <summary>
        /// Prompts the User with a Message, and a Primary Button.
        /// </summary>
        /// <param name="Title">Title for the Dialog</param>
        /// <param name="Message">Message for the Dialog</param>
        /// <param name="PrimaryButtonText">Primary Button Text, e.g. OK</param>
        public async void PromptUser(string Title, string Message, string PrimaryButtonText)
        {
            await PromptUserAsync(Title, Message, PrimaryButtonText, null, null);
        }

        /// <summary>
        /// Requests text from the User using a modal dialog.
        /// </summary>
        /// <param name="Title">Title for the Dialog</param>
        /// <param name="TextHeader">Header/Placeholder for the Text Request</param>
        /// <param name="OKButtonText">Text of Button that Validates the Result</param>
        /// <param name="CancelButtonText">Text of Button that Cancels the Request</param>
        /// <returns>Requested Text</returns>
        public Task<string> RequestTextFromUserAsync(string Title, string Message, string OKButtonText, string CancelButtonText)
        {
            return RequestTextFromUserAsync(Title, Message, OKButtonText, CancelButtonText, null);
        }

        /// <summary>
        /// Requests text from the User using a modal dialog.
        /// </summary>
        /// <param name="Title">Title for the Dialog</param>
        /// <param name="TextHeader">Header/Placeholder for the Text Request</param>
        /// <param name="OKButtonText">Text of Button that Validates the Result</param>
        /// <param name="CancelButtonText">Text of Button that Cancels the Request</param>
        /// <param name="UIBinding">Additional UI Context for Handling the Dialog</param>
        /// <returns>Requested Text</returns>
        public abstract Task<string> RequestTextFromUserAsync(string Title, string Message, string OKButtonText, string CancelButtonText, IUIBindingInfo UIBinding);

        /// <summary>
        /// Shows a Context Menu to the User.
        /// </summary>
        /// <param name="Menu">The Platform Independent Menu Layout to Display</param>
        /// <param name="Binding">UI Context for Handling the Menu, this is used to Display the Menu in different ways, using different UI Layouts</param>
        public abstract void ShowMenu(Menu Menu, IMenuBinding Binding);

        /// <summary>
        /// Shows a Context Menu to the User, using a UI Element for Reference.
        /// </summary>
        /// <param name="Menu">The Platform Independent Menu Layout to Display</param>
        /// <param name="Binding">UI Context for Handling the Menu, this is used to Display the Menu in reference to the Element, e.g. Where the Menu Opens, etc.</param>
        public abstract void ShowMenu(Menu Menu, object UIElement);

        /// <summary>
        /// Registers a Context Menu with a UI Element to Activate with the Element.
        /// </summary>
        /// <param name="Menu">The Platform Independent Menu Layout to Display</param>
        /// <param name="UIElement">UI Element to Register with. This allows for Right Click on UWP, Hold Press on Android, etc.</param>
        public abstract void RegisterMenu(Menu Menu, object UIElement);

        /// <summary>
        /// Opens a Uri Link, using the default Platform Browser, or does nothing if unsupported.
        /// </summary>
        /// <param name="Uri">Uri to Open</param>
        public abstract void OpenLink(Uri Uri);

        /// <summary>
        /// The Current UI Platform the Framework is running on.
        /// </summary>
        public Platform UIPlatform { get; }
    }
}