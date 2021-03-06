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

using System.Threading.Tasks;

namespace PlatformBindings.Models.FileSystem
{
    /// <summary>
    /// The File System wrapper for each File/Folder. This Class holds important functions for manipulating File System Items
    /// </summary>
    public abstract class StorageContainer
    {
        /// <summary>
        /// Renames the Specifed Item
        /// </summary>
        /// <param name="NewName">New Name for this Item</param>
        /// <returns>Operation Success</returns>
        public abstract Task<bool> RenameAsync(string NewName);

        /// <summary>
        /// Asyncronously attempts to delete this Item
        /// </summary>
        /// <returns>Operation Success</returns>
        public abstract Task<bool> DeleteAsync();

        /// <summary>
        /// Presents the Path Location of the File System Item.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Path;
        }

        /// <summary>
        /// Gets the Name as represented via the File System of this Item
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the Full Path of this Item in the File System
        /// </summary>
        public abstract string Path { get; }

        /// <summary>
        /// Determines if the Current File can be Written to.
        /// </summary>
        public abstract bool CanWrite { get; }
    }
}