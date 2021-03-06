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
using System.Threading.Tasks;
using PlatformBindings.Common;
using Java.Net;
using Android.Support.V4.Provider;
using System.Linq;
using Android.Net;

namespace PlatformBindings.Models.FileSystem
{
    public class AndroidSAFFolderContainer : FolderContainer, IAndroidSAFContainer
    {
        public AndroidSAFFolderContainer(Android.Net.Uri Uri)
        {
            Folder = DocumentFile.FromTreeUri(AndroidHelpers.GetCurrentActivity(), Uri);
            CheckFolder();
        }

        public AndroidSAFFolderContainer(DocumentFile Folder)
        {
            this.Folder = Folder;
            CheckFolder();
        }

        private void CheckFolder()
        {
            if (!Folder.IsDirectory)
            {
                throw new FormatException("DocumentFile is not a Directory");
            }
        }

        public override Task<IReadOnlyList<StorageContainer>> GetItemsAsync()
        {
            return Task.Run(() =>
            {
                var Items = new List<StorageContainer>();
                foreach (var item in Folder.ListFiles())
                {
                    if (item.IsDirectory)
                    {
                        Items.Add(new AndroidSAFFolderContainer(item));
                    }
                    else
                    {
                        Items.Add(new AndroidSAFFileContainer(item));
                    }
                }
                return (IReadOnlyList<StorageContainer>)Items;
            });
        }

        public override async Task<IReadOnlyList<FolderContainer>> GetFoldersAsync()
        {
            var items = await GetItemsAsync();
            return items.OfType<FolderContainer>().ToList();
        }

        public override async Task<IReadOnlyList<FileContainer>> GetFilesAsync()
        {
            var items = await GetItemsAsync();
            return items.OfType<FileContainer>().ToList();
        }

        public override Task<FileContainer> GetFileAsync(string FileName)
        {
            return Task.Run(() =>
            {
                try
                {
                    var result = Folder.FindFile(FileName);
                    if (result != null)
                    {
                        return (FileContainer)new AndroidSAFFileContainer(result);
                    }
                }
                catch { }
                return null;
            });
        }

        protected override Task<FolderContainer> InternalCreateFolderAsync(string FolderName)
        {
            return Task.Run(() =>
            {
                var result = Folder.CreateDirectory(FolderName);
                if (result != null)
                {
                    return (FolderContainer)new AndroidSAFFolderContainer(result);
                }
                else return null;
            });
        }

        protected override Task<FileContainer> InternalCreateFileAsync(string FileName)
        {
            return Task.Run(() =>
            {
                var mimeType = URLConnection.GuessContentTypeFromName(FileName);
                var result = Folder.CreateFile(mimeType, FileName);
                if (result != null)
                {
                    return (FileContainer)new AndroidSAFFileContainer(result);
                }
                else return null;
            });
        }

        public override Task<bool> RenameAsync(string NewName)
        {
            return Task.Run(() =>
            {
                return Folder.RenameTo(NewName);
            });
        }

        public override Task<bool> DeleteAsync()
        {
            return Task.Run(() =>
            {
                return Folder.Delete();
            });
        }

        public override Task<bool> FileExists(string FileName)
        {
            return Task.Run(() => Folder.FindFile(FileName) != null);
        }

        ~AndroidSAFFolderContainer()
        {
            Folder?.Dispose();
        }

        public override bool CanWrite => Folder.CanWrite();

        public override string Name => Folder.Name;
        public override string Path => Uri.ToString();
        public DocumentFile Folder { get; }

        public Android.Net.Uri Uri => Folder.Uri;
    }
}