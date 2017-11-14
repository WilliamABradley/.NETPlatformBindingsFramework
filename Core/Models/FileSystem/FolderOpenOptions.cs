﻿using System.Collections.Generic;

namespace PlatformBindings.Models.FileSystem
{
    public class FolderOpenOptions
    {
        public IList<FileSystemContainer> ItemsToSelect { get; } = new List<FileSystemContainer>();
    }
}