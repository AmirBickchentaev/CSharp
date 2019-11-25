using MoyapopitkaNomerPYAT.Directories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MoyapopitkaNomerPYAT
{
    /// <summary>
    /// 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            
        }
        #endregion

       
        #region onLoaded    
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
       
            foreach (var drive in Directory.GetLogicalDrives())
            {
              
                //CREATE A NEW ITEM
                var item = new TreeViewItem()
                {
                    Header = drive,
                    //get fullpath
                    Tag = drive

                };
            // set the path and name to the drive 

                item.Items.Add(null);
                item.Expanded += folder_expanded;

                // adding it to the current system
                Folder_View.Items.Add(item);


            }
        }
        #endregion


        #region folder_expanded
        /// <summary>
        /// When a folder is expanded, find the subfolder files 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folder_expanded(object sender, RoutedEventArgs e)
        {
            #region initial Checks
            var item = (TreeViewItem)sender;

            //if the item contains only dummy data;
            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;

            }
            //clear dummy data
            item.Items.Clear();
             
            var fullpath = (string)item.Tag;
            #endregion
            //create a blank list
            //ignoring every issues
            #region getDirectories
            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullpath);


                    if (dirs.Length > 0)
                    {
                        directories.AddRange(dirs);
                    }
            }
            catch
            {

            }
            //for each directory
            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    //folderName
                    Header = DirectoryStructure.getFileFolderName(directoryPath),
                    //fullpath  
                    Tag = directoryPath
                };
                
                //add dummy item so we can expand the folder
                subItem.Items.Add(null);

                subItem.Expanded += folder_expanded;

                item.Items.Add(subItem);
                
            });
            #endregion
            #region getNewFiles
            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(fullpath);


                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }
            }
            catch
            {

            }
            //for each directory
            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    //folderName
                    Header = DirectoryStructure.getFileFolderName(filePath),
                    //fullpath  
                    Tag = filePath
                };

                //add dummy item so we can expand the folder
                subItem.Items.Add(null);

                subItem.Expanded += folder_expanded;

                item.Items.Add(subItem);

            });
            #endregion
        }
        #endregion

    }
}
