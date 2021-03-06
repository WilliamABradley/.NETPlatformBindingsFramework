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

using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace PlatformBindings.Models.FileSystem
{
    /// <summary>
    /// The File Wrapper for PlatformBindings, use this for File handling on each platform
    /// </summary>
    public abstract class FileContainer : StorageContainer
    {
        /// <summary>
        /// Opens the File as a Stream for Reading or Writing.
        /// </summary>
        /// <param name="CanWrite"></param>
        /// <returns>A Filestream for Manipulation.</returns>
        public abstract Task<Stream> OpenAsStream(bool CanWrite);

        /// <summary>
        /// Returns the contents of the file as a String.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<string> ReadFileAsText()
        {
            using (var stream = await OpenAsStream(false))
            {
                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        /// <summary>
        /// Deserialises an object from the file text, via JSON Parsing.
        /// </summary>
        /// <typeparam name="T">Type of object to Deserialise.</typeparam>
        /// <returns>Operation Success</returns>
        public async Task<T> ReadAsJson<T>()
        {
            var content = await ReadFileAsText();
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Saves the contents of the file to a string.
        /// </summary>
        /// <param name="Text">Content to Save.</param>
        /// <returns></returns>
        public virtual async Task<bool> SaveText(string Text)
        {
            using (var stream = await OpenAsStream(true))
            {
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(Text);
                    return true;
                }
            }
        }

        /// <summary>
        /// Serialises the object as Json, then saves it to the file.
        /// </summary>
        /// <typeparam name="T">Type of object for serialisation.</typeparam>
        /// <param name="Data">Object for serialisation.</param>
        /// <returns>Operation Success</returns>
        public Task<bool> SaveJson<T>(T Data)
        {
            var content = JsonConvert.SerializeObject(Data);
            return SaveText(content);
        }
    }
}