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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace PlatformBindings.Models.FileSystem
{
    public class UWPFolderContainer : FolderContainer, IUWPStorageContainer
    {
        public UWPFolderContainer(StorageFolder Folder)
        {
            this.Folder = Folder;
        }

        public override async Task<FileContainer> GetFileAsync(string FileName)
        {
            var file = await Folder.GetFileAsync(FileName);
            return new UWPFileContainer(file);
        }

        public override async Task<FileContainer> CreateFileAsync(string FileName, Enums.CreationCollisionOption options)
        {
            var file = await Folder.CreateFileAsync(FileName, (CreationCollisionOption)options);
            return new UWPFileContainer(file);
        }

        public override async Task<FolderContainer> GetFolderAsync(string FolderName)
        {
            var subFolder = await Folder.GetFolderAsync(FolderName);
            return new UWPFolderContainer(subFolder);
        }

        public override async Task<FolderContainer> CreateFolderAsync(string FolderName, Enums.CreationCollisionOption options)
        {
            var subFolder = await Folder.CreateFolderAsync(FolderName, (CreationCollisionOption)options);
            return new UWPFolderContainer(subFolder);
        }

        public override async Task<IReadOnlyList<FolderContainer>> GetFoldersAsync()
        {
            var folders = await Folder.GetFoldersAsync();
            return folders.Select(item => new UWPFolderContainer(item)).ToList();
        }

        public override async Task<IReadOnlyList<FileContainer>> GetFilesAsync()
        {
            var files = await Folder.GetFilesAsync();
            return files.Select(item => new UWPFileContainer(item)).ToList();
        }

        public override async Task<bool> DeleteAsync()
        {
            try
            {
                await Folder.DeleteAsync();
                return true;
            }
            catch { return false; }
        }

        public override async Task<bool> RenameAsync(string NewName)
        {
            try
            {
                await Folder.RenameAsync(NewName);
                return true;
            }
            catch { return false; }
        }

        // UNUSED //

        protected override Task<FileContainer> InternalCreateFileAsync(string FileName)
        {
            throw new NotSupportedException();
        }

        protected override Task<FolderContainer> InternalCreateFolderAsync(string FolderName)
        {
            throw new NotSupportedException();
        }

        // UNUSED //

        public StorageFolder Folder { get; }

        public override string Name => Folder.Name;

        public override string Path => Folder.Path;

        public override bool CanWrite => !((Folder.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);

        public IStorageItem Item => Folder;
    }
}