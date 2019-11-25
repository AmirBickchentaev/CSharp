

namespace MoyapopitkaNomerPYAT.Directories
{       
    /// <summary>
    /// A helper class to query all info about directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// find a filename from fullpath
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string getFileFolderName(string Path)
        {
            //C://smth;
            //C://png.jpeg;

            //if we have no path return empty
            if (string.IsNullOrEmpty(Path))
            {
                return string.Empty;
            }

            var normalizePath = Path.Replace('/', '\\');

            var lastIndex = normalizePath.LastIndexOf('\\');
            //make all the slashes into backslash

            if (lastIndex <= 0)
            {
                return Path;
            }
            return Path.Substring(lastIndex + 1);
        }
    }
}
