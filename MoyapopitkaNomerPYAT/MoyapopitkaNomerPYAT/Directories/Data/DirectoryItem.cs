using MoyapopitkaNomerPYAT.Directories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoyapopitkaNomerPYAT
{
    /// <summary>
    /// information about a directory item such as drive, a file or a folder
    /// 
    /// </summary>
    public class DirectoryItem
    {
        public string fullPath { get; set; }

        /// <summary>
        /// the name of this directory item
        /// </summary>
        public string name { get { return DirectoryStructure.getFileFolderName(fullPath); } }

    }
}
